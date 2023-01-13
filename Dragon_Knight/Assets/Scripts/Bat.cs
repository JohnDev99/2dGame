using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    [SerializeField] Transform[] positions;
    [SerializeField] float angleLineValue, depthLineValue;
    [SerializeField] float playerCenter;

    float idleTimer;
    Transform myPosition;

    bool playerSpoted;


    [SerializeField] LayerMask wallLayer;
    Transform playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        myPosition = GetComponent<Transform>();
        playerSpoted = false;
        playerPosition = FindObjectOfType<PlayerController>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerSpoted)
        {
            SleepState();
        }

        FollowPlayer(playerPosition);


    }


    void SleepState()
    {
        Vector2 leftEndLine = new Vector2(myPosition.position.x - angleLineValue, depthLineValue);
        Vector2 rightEndLine = new Vector2(myPosition.position.x + angleLineValue, depthLineValue);

        RaycastHit2D hitLeft = Physics2D.Raycast(myPosition.position, leftEndLine);
        RaycastHit2D hitRight = Physics2D.Raycast(myPosition.position, rightEndLine);
        //Debbuging
        Debug.DrawLine(myPosition.position, leftEndLine);
        Debug.DrawLine(myPosition.position, rightEndLine);

        if(hitLeft.collider.gameObject.CompareTag("Player") || hitRight.collider.gameObject.CompareTag("Player"))
        {
            //Start chasing Player
            //Move Towards player
            Debug.Log("Player");

            playerSpoted = true;

            
        }
    }

    void FollowPlayer(Transform player)
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y + playerCenter);
        myPosition.position = Vector2.Lerp(myPosition.position, playerPos, Time.deltaTime);

        if(Vector2.Distance(playerPos, myPosition.position) <= 2f)
        {
            Debug.Log("Attack");
        }

    }

}
