using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float powerJump;


    private Rigidbody2D body;
    private Animator anim;
    private float wallJumpCooldown;
    private BoxCollider2D boxCollider;
    private float horizontalInput;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        // move right - left
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // jump wall logic

        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);



            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime; 



    }
    private void Jump()
    {
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, powerJump);
                    anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10,5);
                transform.localScale = new Vector3(
                    -Mathf.Sign(transform.localScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                    );
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3,7);
            }
            wallJumpCooldown = 0;
        }
      
    }

    // is the player on ground or not
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            0.1f,
            groundLayer
            );
        return raycastHit.collider != null;
    }
    // check if the player on wall or not
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            new Vector2(transform.lossyScale.x, 0),
            0.1f,
            wallLayer
            );
        return raycastHit.collider != null;
    }

    //can player shooting
    public bool canAttack()
    {
        return isGrounded() && !onWall() && horizontalInput == 0;
    }
}
