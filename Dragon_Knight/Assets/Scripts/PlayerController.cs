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
    [SerializeField]bool isFlipped;

    Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        CheckFacingDirection();



    }

    private void FixedUpdate()
    {

        if (horizontalInput != 0)
        {
            MovePlayer();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }


    }

    private void LateUpdate()
    {
        playerAnim.SetFloat("MoveX", horizontalInput);
    }

    //Player começa devagar e acelera e desacelera suavemente
    void MovePlayer()
    {
        Vector2 playerMovement = new Vector2(myRb.position.x + speed * horizontalInput * Time.fixedDeltaTime, myRb.position.y);
        myRb.MovePosition(playerMovement);
        //myRb.AddForce(playerMovement);
    }
    void Jump()
    {
        //Player abaixa-se e depois que passa ao salto(antecipaçao)
        //Dealay antes do salto

        myRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }

    void CheckFacingDirection()
    {
        if (horizontalInput > 0)
        {
            isFlipped = false;
        }
            
        if(horizontalInput < 0)
        {
            isFlipped = true;
        }

        FlipCharacter();
    }

    void FlipCharacter()
    {
        //Vector3 currentScale = GetComponentInChildren<Transform>().transform.localScale;
        //currentScale.x *= -1;
        SpriteRenderer playerCharacter = gameObject.GetComponentInChildren<SpriteRenderer>();

        playerCharacter.flipX = isFlipped;

    }

    
}
