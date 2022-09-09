using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D myRb;
    CapsuleCollider2D playerCollider;
    [SerializeField] float speed;
    [SerializeField] float jumpForce = 5f;

    float horizontalInput;
    float verticalInput;
    [SerializeField]bool isFlipped;

    //Collision Detection
    [SerializeField]Transform groundPoint;
    [SerializeField]LayerMask groundLayer;
    [SerializeField]bool isGrounded;
    [SerializeField] float groundCheckRadius = 1.5f;

    //SwordAttack
    //[SerializeField] float attackDelay = 1f;
    

    Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        CheckFacingDirection();

        SwordAttack();


    }

    private void FixedUpdate()
    {
        IsGroundedCheck();



        if (horizontalInput != 0 && isGrounded == true)
        {
            MovePlayer();
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            playerAnim.SetBool("isWalking", false);
        }

        if(verticalInput > 0 && isGrounded == true)
        {
            Jump();
        }
    }

    private void IsGroundedCheck()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundPoint.position, groundCheckRadius, groundLayer);
        if (hit != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


    //Player começa devagar e acelera e desacelera suavemente
    void MovePlayer()
    {
        myRb.velocity = Vector2.right * horizontalInput * speed;
    }
    void Jump()
    {
        //Player abaixa-se e depois que passa ao salto(antecipaçao)
        //Dealay antes do salto

        myRb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);


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
        SpriteRenderer playerCharacter = gameObject.GetComponentInChildren<SpriteRenderer>();

        playerCharacter.flipX = isFlipped;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundPoint.position, groundCheckRadius);
    }

    void SwordAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("SwordAttack");
        }
    }

    //Evento de ataque
    void SwordHit()
    {

    }

}
