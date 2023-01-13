using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    int damageToPlayer = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            collision.gameObject.GetComponent<PlayerController>().LoseLife(damageToPlayer);
            //Come�ar uma courtina pra iniciar uma anima�ao
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
