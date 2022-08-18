using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool isGrounded;            // Whether or not the player is grounded.
	private Rigidbody2D rb;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
    public Animator animator;
    public int targetFrameRate = 60;

    public float jumpTime;
	private float jumpTimeCounter;
	private bool stoppedJumping;

    public float runSpeed = 40f;
	float horizontalMove = 0f;

	private Vector3 respawnPoint;
	public GameObject fallDetector;

	private void Start()
	{
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
		respawnPoint = transform.position;
    }
	private void Update()
	{
        GroundCheck();

        if (isGrounded)
		{
			jumpTimeCounter = jumpTime;
			animator.SetBool("isJumping", false);
			animator.SetBool("Falling", false);

		}
        // If the player should jump...
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Add a vertical force to the player.
            rb.AddForce(new Vector2(0f, m_JumpForce));
			stoppedJumping = false;
            animator.SetBool("isJumping", true);
        }

		if (Input.GetButtonDown("Jump") && !stoppedJumping && (jumpTimeCounter > 0))
		{
            isGrounded = true;
            rb.AddForce(new Vector2(0f, m_JumpForce));
			jumpTimeCounter -= Time.deltaTime;
            animator.SetBool("isJumping", true);
        }

		if (Input.GetButtonUp("Jump"))
		{
			jumpTimeCounter = 0;
			stoppedJumping = true;
			animator.SetBool("Falling", true);
            animator.SetBool("isJumping", false);
        }

		if(rb.velocity.y < 0)
		{
			animator.SetBool("Falling", true);
        }
    }

	private void GroundCheck()
	{
        isGrounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
			}
		}
    }

	private void FixedUpdate()
	{
        Move(horizontalMove * Time.fixedDeltaTime);
		SwitchLayers();
    }

	private void Move(float move)
	{

		//only control the player if grounded or airControl is turned on
		if (isGrounded || m_AirControl)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
			// And then smoothing it out and applying it to the character
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void SwitchLayers()
	{
		if (!isGrounded)
		{
			animator.SetLayerWeight(1, 1);
		}
		else
		{
            animator.SetLayerWeight(1, 0);
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "FallDetector")
		{
			Debug.Log("Player respawned");
			transform.position = respawnPoint;
		}	
	}
}
