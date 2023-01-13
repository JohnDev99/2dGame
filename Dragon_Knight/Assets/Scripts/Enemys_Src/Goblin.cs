using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Goblin : Enemy
{
    [SerializeField] Transform groundPoint;
    [SerializeField] Vector2 boxSize;
    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] float rayDistance;
    Transform myPosition;
    [SerializeField] Transform rayPosition;
    bool isGrounded;
    


    // Start is called before the first frame update
    void Start()
    {
        myPosition = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGroundCheck())
        {
            Walk();
        }

        if (WallDetected())
        {
            speed = -speed;
            myPosition.localScale = EntityLocalScale(myPosition);
        }

        isGrounded = isGroundCheck();
        
    }

    private void Walk()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    bool isGroundCheck()
    {
        Collider2D hit = Physics2D.OverlapPoint(groundPoint.position, groundLayer);

        if(hit != null)
        {
            return true;
        }

        return false;

    }

    bool WallDetected()
    {
        Vector2 lineStart = new Vector2(myPosition.position.x, myPosition.position.y);
        Vector2 lineEnd = new Vector2(myPosition.position.x + rayDistance, myPosition.position.y);



        Debug.DrawLine(lineStart, lineEnd);

        RaycastHit2D hit = Physics2D.Linecast(lineStart, lineEnd, groundLayer);
        if(hit.collider != null)
        {
            rayDistance *= -1;
            return true;
        }

        return false;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundPoint.position, boxSize);
    }

}
