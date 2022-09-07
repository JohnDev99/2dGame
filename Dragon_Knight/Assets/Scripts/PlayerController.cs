using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D myRb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce = 5f;

    float horizontalInput;
    float verticalInput;


    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput != 0)
        {
            MovePlayer();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }


    }

    private void FixedUpdate()
    {

        
    }

    //Player começa devagar e acelera e desacelera suavemente
    void MovePlayer()
    {
        Vector2 playerMovement = new Vector2(myRb.position.x + speed * horizontalInput * Time.deltaTime, myRb.position.y);
        myRb.MovePosition(playerMovement);
        //myRb.AddForce(playerMovement);
    }
    void Jump()
    {
        //Player abaixa-se e depois que passa ao salto(antecipaçao)
        //Dealay antes do salto

        myRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }
}
