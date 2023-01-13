using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected int enemyCurrentlife = 5;
    protected bool isHit = false;
    Rigidbody rb;
    bool isFlipped;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LoseLife(int damage)
    {
        enemyCurrentlife -= damage;

        StartCoroutine(EnemyHit(2f));
        if (enemyCurrentlife <= 0)
        {
            EnemyDeath();
        }

        //KnockBack Effect - change color of the sprite 
        //KnockBackEffect();
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }

    IEnumerator EnemyHit(float hitTime)
    {
        StopAllCoroutines();
        isHit = true;
        //gameObject.GetComponentInChildren<Animator>().SetTrigger("Hit");
        isHit = false;
        yield return new WaitForSeconds(hitTime);

    }


    /// <summary>
    /// Flips Enemy localScale
    /// </summary>
    protected void Flip()
    {

        Vector2 enemyLocalScale = transform.localScale;
        enemyLocalScale.x *= -1f;
        transform.localScale = enemyLocalScale;
        isFlipped = !isFlipped;
    }
}
