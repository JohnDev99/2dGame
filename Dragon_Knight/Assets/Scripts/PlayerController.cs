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
    [SerializeField] Transform swordPoint;
    [SerializeField] LayerMask enemyLayer;
    public int damageToGive = 1;

    [SerializeField] float hitForce = 20f;

    Animator playerAnim;
    SpriteRenderer playerCharacter;

    private void Awake()
    {
        playerCharacter = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

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

    private void LateUpdate()
    {
        playerAnim.SetBool("isJumping", !isGrounded);
        playerAnim.SetFloat("jumpVelocity", myRb.velocity.y);
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
        Vector3 newScale = transform.localScale;

        if (isFlipped)
        {
            newScale.x = -1;
            transform.localScale = newScale;
        }
        else
        {
            newScale.x = 1f;
            transform.localScale = newScale;
        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundPoint.position, groundCheckRadius);
        Gizmos.DrawWireSphere(swordPoint.position, 0.5f);
    }

    void SwordAttack()
    {
        //Se premir o botao Espaço E se eu puder atacar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("SwordAttack");
            //Crear uma coutina para esperar 0.03f
            SwordHit();
        }
    }

    //Evento de ataque
    public void SwordHit()
    {
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(swordPoint.transform.position, 1f, enemyLayer);

        foreach(Collider2D enemyColl in hitEnemys)
        {
            //Debug.Log("Hit : " + enemyColl.name);
            Rigidbody2D enemyRb = enemyColl.GetComponent<Rigidbody2D>();

            Vector2 forceDirection = new Vector2(enemyColl.transform.position.x - transform.position.x, 0f).normalized;
            enemyRb.AddForce(forceDirection * hitForce, ForceMode2D.Impulse);

            //Perde vida
            enemyColl.GetComponent<EnemyBehaviour>().LoseLife(damageToGive);
        }
    }

}
