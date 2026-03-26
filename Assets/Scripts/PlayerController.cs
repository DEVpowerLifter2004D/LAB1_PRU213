using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce = 8f; 
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private bool canDie = false;
    private AudioManager audioManager;
    private GameManager gameManager;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
           Invoke(nameof(EnableDie), 0.2f); // đợi 0.2s sau khi spawn

        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();  
    }
    void EnableDie()
    {
        canDie = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager == null) return;

        if (gameManager.IsGameOver() || gameManager.IsGameWin()) return;

        handleMovement();
        handleJump();
        updateAnimation();

        if (!canDie) return;

        if (transform.position.y < -10f)
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }


    private void updateAnimation()
    {

        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
         animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);

    }

    private void handleJump()
    {

        if ( Input.GetButtonDown("Jump") && isGrounded)

        {
            audioManager.PlayJumpSound();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
         
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);


    }
    public void ResetPlayer()
    {
        canDie = false;
        Invoke(nameof(EnableDie), 0.2f);
    }

    private void handleMovement()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector3(moveHorizontal * speed, rb.linearVelocity.y);

        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
