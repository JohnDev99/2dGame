using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] int health = 100;

    //Enemy Ai
    public float velocity = 5f;
    Rigidbody2D rb;

    [SerializeField]LayerMask wallLayer;
    [SerializeField] float originOffset = 1f;

    Vector2 enemyLocalScale;

    //Life
    public int enemyLife = 5;
    int enemyCurrentLife;

    //Player Check
    [SerializeField] LayerMask playerLayer;
    public float overlapRadius = 1f;
    [SerializeField] float minDistance, maxDistance;

    /// <summary>
    /// Uso de enum para vereficar que tipo de enemigo é
    /// Ghoul, Morcego, Esqueleto
    /// </summary>
    ///


    //Vereficar se colidi com uma parede
    bool isHittingWall = false;
    bool isFlipped = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyLocalScale = transform.localScale;
        enemyCurrentLife = enemyLife;
    }

    // Update is called once per frame
    void Update()
    {
        WallHit();

        //Verefica se o personagem esta dentro da minha area de visao
        Collider2D checkPlayerPosition = Physics2D.OverlapCircle(transform.position, overlapRadius, playerLayer);

        if (checkPlayerPosition != null && isHittingWall == false)
        {
            #region
            /*Debug.Log("See " + checkPlayerPosition.gameObject.name);
            //Vereficar a minha distancia ao player
            float distanceToPlayer = Vector2.Distance(transform.position, checkPlayerPosition.gameObject.transform.position);
            if (distanceToPlayer <= minDistance)
            {
                state = StateChange.chasing;

            }
            else if (distanceToPlayer >= maxDistance)
            {
                state = StateChange.patroling;
            }

            //Vereficar de que lado o meu player esta
            float playerDiference = Mathf.Round(transform.position.x - checkPlayerPosition.gameObject.transform.position.x);
            //Mover para a posiçao que o player se encontra
            Vector2 playerPos = Vector2.MoveTowards(transform.position, checkPlayerPosition.gameObject.transform.position, distanceToPlayer);
            transform.Translate(playerPos * velocity * Time.fixedDeltaTime);

            //Se ele estiver a minha esquerda
            if (playerDiference < 0)
            {

                velocity *= -1f;
                Flip();
            }
            if(playerDiference > 0)
            {
                velocity *= -1f;
                Flip();
            }*/
            #endregion
            //Chase Player

            //transform.Translate(checkPlayerPosition.gameObject.transform.position * velocity * 2 * Time.deltaTime);

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(checkPlayerPosition.gameObject.transform.position.x, transform.position.y), velocity * 2 * Time.deltaTime);

            float distanceToPlayer = Mathf.Round(transform.position.x - checkPlayerPosition.gameObject.transform.position.x);
            if(distanceToPlayer <= 0 && isFlipped == false)
            {
                //transform.localScale = new Vector3(-1, 1, 1);
                Flip();
            }
            if(distanceToPlayer > 0 && isFlipped == true)
            {
                Flip();
                
            }
            else
            {
                Debug.Log("Explode");
            }
        }
        else
        {
            Debug.Log("Patrol");
            Move();
        }


    }

    private void Move()
    {
        //  Move o personagem
        transform.Translate(Vector2.left * velocity * Time.deltaTime);
    }

    private void WallHit()
    {
        Vector2 myPos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(myPos + new Vector2(-originOffset, 0f), Vector2.left * velocity, 1f, wallLayer);

        if (hit.collider != null)
        {
            //Inverter a posiçao do enemigo
            //mutiplicar a minha velocidade por -1f
            velocity *= -1f;

            Flip();
            isHittingWall = true;
            
        }
        else
        {
            isHittingWall = false;
        }
        Debug.DrawRay(myPos + new Vector2(-originOffset, 0f), Vector2.left * velocity, Color.red);
    }


    protected void Flip()
    {
        Vector2 enemyLocalScale = transform.localScale;
        enemyLocalScale.x *= -1f;
        transform.localScale = enemyLocalScale;
        isFlipped = !isFlipped;
    }

    //Metodo a ser invocado pelo player
    public void LoseLife(int damage)
    {
        enemyCurrentLife -= damage;
        if(enemyCurrentLife <= 0)
        {
            Die();
        }

        //KnockBack Effect - change color of the sprite 
        KnockBackEffect();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void KnockBackEffect()
    {
        Animator anim = GetComponentInChildren<Animator>();
        anim.SetTrigger("Hurt");

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, overlapRadius);
    }

}
