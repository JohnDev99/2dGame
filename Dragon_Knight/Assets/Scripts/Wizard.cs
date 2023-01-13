using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Wizard : EnemyBase
{
    Rigidbody2D rb;

    [SerializeField] float radius = 1f;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;

    public float startShootingTime = 60f;
    float timerCount;
    public float fireBallSpeed = 20f;

    bool firstShot;
    [SerializeField] float rayDistance;
    public Transform[] wizardPoints;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timerCount = startShootingTime;
    }

    // Update is called once per frame
    void Update()
    {

        

        Collider2D player = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        if(player != null && isHit == false)
        {
            Vector2 playerCenter = player.GetComponent<Transform>().position;
            Transform centerPos = player.gameObject.transform.GetChild(0).GetComponent<Transform>();
            Vector2 distanceToPlayer = centerPos.position - transform.position;

            //Seguir a posiçao do player
            FlipAlongPlayerPos(playerCenter);

            Shoot(distanceToPlayer);

            //Se o player esta muito perto de mim (Raycast)
            #region Tranport
            //Tranportar-me para posiçao aleatoria(Magia)
            RaycastHit2D rayHitLeft = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, playerLayer);
            RaycastHit2D rayHitRight = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, playerLayer);
            Debug.DrawRay(transform.position, Vector2.left, Color.red, rayDistance);
            Debug.DrawRay(transform.position, Vector2.right, Color.red, rayDistance);
            if (rayHitLeft.collider != null || rayHitRight.collider != null)
            {
                int randomPos = RandomValue(0, wizardPoints.Length);
                if (wizardPoints[randomPos].position.x != transform.position.x)
                {
                    StartCoroutine(Teleport(randomPos));
                }
                else
                    return;


            }
            #endregion

        }
        StopAllCoroutines();

    }

    

    private void Shoot(Vector2 playerCenter)
    {
        //Atiro 1 vez bool = true
        //Enquanto na area de colisao esse valor vai ser verdadeiro
        //caso contrario volta a ser falso
        if(isHit == false)
        {
            if (firstShot == false)
            {
                ShootAtPlayer(playerCenter);
                firstShot = true;
            }
            else
            {
                timerCount -= Time.deltaTime;
                if (timerCount <= 0)
                {
                    ShootAtPlayer(playerCenter);
                }
            }
        }

    }

    private void ShootAtPlayer(Vector2 playerCenter)
    {
        //Disparar 
        GameObject fireBall = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity) as GameObject;
        fireBall.GetComponent<Rigidbody2D>().AddForce(playerCenter * fireBallSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        timerCount = startShootingTime;
    }


    //Metodo de Flip
    private void FlipAlongPlayerPos(Vector2 playerCenter)
    {
        //Flip According PlayerPos
        float distanceToPlayer = Mathf.Round(transform.position.x - playerCenter.x);
        Vector2 myScale = transform.localScale;
        if (distanceToPlayer <= 0)
        {
            //Local scale = -1
            //Metodo com retorno Vector3
            myScale.x = -1f;
            transform.localScale = myScale;
        }
        if (distanceToPlayer > 0)
        {
            myScale.x = 1f;
            transform.localScale = myScale;
        }
    }

    IEnumerator Teleport(int randomIndex)
    {
        yield return new WaitForSeconds(2f);
        transform.position = wizardPoints[randomIndex].position;
        //Passar Animaçao
        StopAllCoroutines();

    }


    //Sobrecarga do metodo
    int RandomValue(int min, int max)
    {
        return Random.Range(min, max);       
    }
    float RandomValue(float min, float max)
    {
        return Random.Range(min, max);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
