using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontalMovement;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private bool isGrounded = true;

    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private SpriteRenderer render;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalMovement * speed, rb.linearVelocity.y);
        if(horizontalInpunt!= 0)
        {
            render.flipX = horizontalMovement < 0;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Jumpable")) return;
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 n = collision.GetContact(i).normal;
            if (n.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumpable"))
        {
            isGrounded = false;
        }
    }

}