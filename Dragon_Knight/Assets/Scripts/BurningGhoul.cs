using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningGhoul : EnemyBase
{
    [SerializeField] float velocity;
    [SerializeField] float rayDistance = 2f;
    [SerializeField] LayerMask hitObjects;
    bool isHited;
    [SerializeField] Transform rayPos;

    //Chase Player
    public float rayToPlayerDistance;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float rayDistanceOfPlayerView = 10f;
    public float chaseSpeed = 2f;
    bool isChasing;

    //Ghoul Jump chase
    public Transform wallColisionPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isHited == false)
        {
            IsHittingWall();
        }

        //JumpToFollowPlayer();

        //Criar uma caixa na direçao que o enemigo esta a olhar e vereficar se encontra o player
        //Enquanto estiver nessa area vou correr ate ele na tentativa de explodir
        //Olhar no infinito
        RaycastHit2D playerFound = Physics2D.Raycast(transform.position, Vector2.left * velocity, rayDistanceOfPlayerView, playerLayer);
        Debug.DrawRay(transform.position, Vector2.left * velocity, Color.blue);
        if (playerFound.collider != null)
        {
            //Enquanto o player estiver na area de visao , vou de encontro a ele
            //Caso Contrario vou voltar a patrulha
            isChasing = true;
            Move(velocity * chaseSpeed);
            
            //JumpCheck
        }else
        {
            Move(velocity);
            isChasing = false;
        }

    }

    //Retorna um valor booleano 
    private bool IsHittingWall()
    {
        RaycastHit2D hitWall = Physics2D.Raycast(rayPos.position, Vector2.left * velocity, rayDistance, hitObjects);
        Debug.DrawRay(transform.position, Vector2.left * velocity, Color.red);
        if (hitWall.collider != null)
        {
            Flip();
            velocity *= -1f;
            return true;
        }
        else
            return false;
    }

    private void Move(float myVelocity)
    {
        transform.Translate(Vector2.left * myVelocity * Time.deltaTime);
    }


    //
    void JumpToFollowPlayer()
    {
        Collider2D pointCollider = Physics2D.OverlapPoint(wallColisionPoint.transform.position, hitObjects);
        if(IsHittingWall() == true && isChasing == true && pointCollider != null)
        {
            transform.Translate(Vector2.up * velocity * Time.deltaTime);
        }

    }
}
