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

    enum StateChange
    {
        patroling,
        chasing
    }

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
        transform.Translate(Vector2.left * velocity * Time.deltaTime);

        WallHit();

        //Verefica se o personagem esta dentro da minha area de visao
        Collider2D checkPlayerPosition = Physics2D.OverlapCircle(transform.position, overlapRadius, playerLayer);
        if(checkPlayerPosition != null)
        {
            Debug.Log("See " + checkPlayerPosition.gameObject.name);
            //Vereficar a minha distancia ao player
            float distanceToPlayer = Vector2.Distance(transform.position, checkPlayerPosition.gameObject.transform.position);
            if(distanceToPlayer <= minDistance)
            {
                Debug.Log("Chase");
                Vector2.MoveTowards(transform.position, checkPlayerPosition.gameObject.transform.position, distanceToPlayer);
            }
            if (distanceToPlayer >= maxDistance)
            {
                Debug.Log("Return to Patrol");
            }
            else
                return;
        }

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
            
        }
        Debug.DrawRay(myPos + new Vector2(-originOffset, 0f), Vector2.left, Color.red);
    }

    void PlayerFollow()
    {
        
    }

    protected void Flip()
    {
        Vector2 enemyLocalScale = transform.localScale;
        enemyLocalScale.x *= -1f;
        transform.localScale = enemyLocalScale;
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
