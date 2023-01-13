using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    EnemyObject enemy;

    protected string type;
    protected int health;
    protected float speed;
    protected float damageToPlayer;

    protected bool isDead;


    //Propriedade para obter vida do enemigo
    public int EnemyHealth { get { return this.health; } }


    //Construtor 
    /*protected Enemy(EnemyObject enemy)
    {
        this.enemy = enemy;
        this.health = enemy.health;
        this.speed = enemy.speed;
        this.damageToPlayer = enemy.damageToPlayer;
        this.isDead = false;
    }*/

    private void Awake()
    {
        this.health = enemy.health;
        this.type= enemy.type;
        this.damageToPlayer = enemy.damageToPlayer;
        this.speed = enemy.speed;
        this.isDead = false;
    }

    protected void LoseLife(int damageToGive)
    {
        this.health -= damageToGive;
    }

    public void EnemyLoseHealth()
    {

        if (health > 0)
        {
            health--;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    //Tipos de enemigos Aereos e terrestres

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().LoseLife(this.damageToPlayer);
        }
    }


}
