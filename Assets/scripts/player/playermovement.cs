using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Animator anim;
    private BoxCollider2D BoxCollider;
    public LayerMask groundlayer;
    public LayerMask wallLayer;
    private float walljumpCooldown;
   public float horizontalInput;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jump")]
    [SerializeField] private int extraJump;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [SerializeField] private Joystick joystick;



    [Header("SFX")]
     [SerializeField]  private AudioClip jumpSound;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        BoxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {


       // horizontalInput = Input.GetAxis("Horizontal");

        horizontalInput = joystick.Horizontal;


        //for flip player
        if (horizontalInput>0.01f)
        {
            transform.localScale = Vector3.one;
        }

       else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3 (-1,1,1);
        }

      

        anim.SetBool("grounded",isGrounded());  
        anim.SetBool("run",horizontalInput!=0);


        if (Input.GetKeyDown(KeyCode.Space))

        {
            Jump();
            Debug.Log("jumping");
        }


        if(Input.GetKeyUp(KeyCode.Space) && body .linearVelocity.y>0)
        {
            body.linearVelocity= new Vector2(body.linearVelocity.x,body.linearVelocity.y/2);
        }

        if (onWall())
        {
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.linearVelocity = new Vector2(horizontalInput* speed ,body.linearVelocity.y);

            if(isGrounded())
            {
                coyoteCounter = coyoteTime;
                jumpCounter = extraJump;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }
        }

     


    }

    public void Jump()
    {
        if(coyoteCounter <=0 && !onWall() && jumpCounter <= 0)
        {
            return;
        }

        AudioManager.instance.PlaySound(jumpSound);

        if(onWall())
        {
            WallJump();
        }
        else
        {
            if(isGrounded() )
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            }
            else
            {
                if (coyoteCounter > 0)
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                }
                else
                {
                    if (jumpCounter > 0)
                    {
                        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }
            coyoteCounter = 0;
        }



      /* if (isGrounded())
        {
            
            body.velocity = new Vector2(body.velocity.x, jumpPower);
           
            
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput==0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 20, 0);
                transform.localScale= new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y,transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            walljumpCooldown = 0;

           
        }*/
      
       
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        walljumpCooldown = 0;
    }


   
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size, 0, Vector2.down, 0.1f, groundlayer);
        return raycastHit.collider!=null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(BoxCollider.bounds.center, BoxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    
    public bool attack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

  

}

