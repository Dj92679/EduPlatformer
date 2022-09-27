using System.Collections;
using System.Collections.Generic;
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
	public List<GameObject> numbers = new List<GameObject>();
	public GameObject uiHud;

    public float jumpTime;
	private float jumpTimeCounter;
	private bool stoppedJumping;
	private int keys = 0; 

    public float runSpeed = 40f;
	float horizontalMove = 0f;

	private Vector3 respawnPoint;
	public GameObject fallDetector;
	public Vector3 spacing = new Vector3(-2f, 2f, 0f);
	public Vector3 spacing2 = new Vector3(2f, 2f, 0f);

	BoxCollider2D boxCollider;
	CircleCollider2D circleCollider;

	private void Start()
	{
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
		respawnPoint = transform.position;
		boxCollider = GetComponent<BoxCollider2D>();
		circleCollider = GetComponent<CircleCollider2D>();
		gameObject.tag = "Player";

		uiHud.GetChild(1).text = "Keys: " + keys;
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

		foreach(GameObject number in numbers) {
			if(numbers.Count == 1) {
				numbers[0].transform.position = Vector3.Lerp(number.transform.position, transform.position + spacing, 10f);
			}
			if(numbers.Count == 2) {
				numbers[0].transform.position = Vector3.Lerp(number.transform.position, transform.position + spacing, 10f);
				numbers[1].transform.position = Vector3.Lerp(number.transform.position, transform.position + spacing2, 10f);
			}
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

	private void PlaceNumber() 
	{
		
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

	// Makes the character fall by disabling then enabling the collider
	private IEnumerator Fall()
	{
		boxCollider.isTrigger = true;
		circleCollider.isTrigger = true;
		yield return new WaitForSeconds(0.3f);
		boxCollider.isTrigger = false;
		circleCollider.isTrigger = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "FallDetector")
		{
			transform.position = respawnPoint;
		}
		if(collision.gameObject.tag == "Number" && Input.GetKey("space")) {
			numbers.Add(collision.gameObject);
			if(numbers[0] == numbers[1]) {
				numbers.RemoveAt(1);
			}
			if(numbers.Count > 2) {
				numbers.RemoveAt(0);
			}
		}

		if(collision.gameObject.tag == "Chest") {
			if(numbers.Count == 2 && keys < 5) {
				Debug.Log("chest has been triggered");
				if(numbers[0].GetComponent<NumScript>().value == collision.gameObject.GetComponent<NumberSpawn>().first && numbers[1].GetComponent<NumScript>().value == collision.gameObject.GetComponent<NumberSpawn>().second || numbers[1].GetComponent<NumScript>().value == collision.gameObject.GetComponent<NumberSpawn>().first && numbers[0].GetComponent<NumScript>().value == collision.gameObject.GetComponent<NumberSpawn>().second) {
					keys++;
					//uiHud.GetChild(1).text = "Keys: " + keys;
					Destroy(numbers[0]);
					Destroy(numbers[1]);
					collision.gameObject.GetComponent<NumberSpawn>().Spawn();
					if(keys < 5) {
						collision.gameObject.transform.position = collision.gameObject.GetComponent<NumberSpawn>().chestLocations.transform.GetChild(keys).transform.position;
					}
				}
			}
		}
	}

	private void OnCollisionStay2D(Collision2D other)
    {
		if (other.gameObject.tag == "Platform")
		{
			if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			{
				// Disables the player collider temporarily
				StartCoroutine("Fall");
			}
		}
		}
	}
