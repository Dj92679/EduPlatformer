using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

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
	public GameObject equationText;
	public GameObject chest;
	public GameObject numPoint;
	public GameObject door;
	public GameObject spawnedNums;

    public float jumpTime;
	private float jumpTimeCounter;
	private bool stoppedJumping;
	private int keys = 3; 

    public float runSpeed = 40f;
	float horizontalMove = 0f;

	

	private Vector3 respawnPoint;
	public Vector3 spacing = new Vector3(-2f, 2f, 0f);
	public Vector3 spacing2 = new Vector3(2f, 2f, 0f);

    CapsuleCollider2D capsuleCollider;

	private void Start()
	{
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
		respawnPoint = transform.position;
		capsuleCollider = GetComponent<CapsuleCollider2D>();
		gameObject.tag = "Player";
		uiHud.GetComponent<TMP_Text>().text = "Keys: " + keys + "/5";
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
            SoundManager.PlaySound("Jump");
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

	// Makes the character fall by disabling then enabling the collider
	private IEnumerator Fall()
	{
		capsuleCollider.isTrigger = true;
		yield return new WaitForSeconds(0.3f);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Number" && Input.GetKeyUp("space"))
        {
            numbers.Add(collision.gameObject);
			collision.gameObject.transform.position = numPoint.transform.position;
			equationText.GetComponent<TMP_Text>().text = "Equation: " + numbers[0].GetComponent<NumScript>().value.ToString() + " " + chest.GetComponent<NumberSpawn>().operation + "  = " + chest.GetComponent<NumberSpawn>().solution;
            SoundManager.PlaySound("Pickup");
            /*if (numbers[0] == numbers[1])
            {
                numbers.RemoveAt(1);
				equationText.GetComponent<TMP_Text>().text = "Equation: " + numbers[0].GetComponent<NumScript>().value.ToString() + " " + chest.GetComponent<NumberSpawn>().operation + "  = " + chest.GetComponent<NumberSpawn>().solution;
    		}*/

            if (numbers.Count == 1) {
				equationText.GetComponent<TMP_Text>().text = "Equation: " + numbers[0].GetComponent<NumScript>().value.ToString() + " " + chest.GetComponent<NumberSpawn>().operation + "  = " + chest.GetComponent<NumberSpawn>().solution;
			}

			if(numbers.Count == 2) {
				if(numbers[1].GetComponent<NumScript>().value > numbers[0].GetComponent<NumScript>().value) {
					equationText.GetComponent<TMP_Text>().text = "Equation: " + numbers[1].GetComponent<NumScript>().value.ToString() + " " + chest.GetComponent<NumberSpawn>().operation +" " + numbers[0].GetComponent<NumScript>().value.ToString() + " = " + chest.GetComponent<NumberSpawn>().solution;
				}
				else {
					equationText.GetComponent<TMP_Text>().text = "Equation: " + numbers[0].GetComponent<NumScript>().value.ToString() + " " + chest.GetComponent<NumberSpawn>().operation +" " + numbers[1].GetComponent<NumScript>().value.ToString() + " = " + chest.GetComponent<NumberSpawn>().solution;
				}
			}
            
            if (numbers.Count > 2)
            {
				numbers[0].transform.position = transform.position;
                numbers.RemoveAt(0);
				
				if(numbers[1].GetComponent<NumScript>().value > numbers[0].GetComponent<NumScript>().value) {
					equationText.GetComponent<TMP_Text>().text = "Equation: " + numbers[1].GetComponent<NumScript>().value.ToString() + " " + chest.GetComponent<NumberSpawn>().operation +" " + numbers[0].GetComponent<NumScript>().value.ToString() + " = " + chest.GetComponent<NumberSpawn>().solution;
				}
				else {
					equationText.GetComponent<TMP_Text>().text = "Equation: " + numbers[0].GetComponent<NumScript>().value.ToString() + " " + chest.GetComponent<NumberSpawn>().operation +" " + numbers[1].GetComponent<NumScript>().value.ToString() + " = " + chest.GetComponent<NumberSpawn>().solution;
				}
            }
        }
		if(collision.gameObject.tag == "Door" && keys == 5 && Input.GetKeyUp(KeyCode.Return)) {
			int difficulty = chest.GetComponent<NumberSpawn>().difficulty;
			int level = door.GetComponent<DoorScript>().level;
			switch(difficulty) {
				case 1: 
					switch(level) {
						case 1:
							SceneManager.LoadScene("E2");
							break;
						case 2:
							SceneManager.LoadScene("E3");
							break;
						case 3:
							SceneManager.LoadScene("E4");
							break;
						case 4:
							SceneManager.LoadScene("E5");
							break;
						case 5:
							SceneManager.LoadScene("MainMenu");
							break;
					}
					break;
				case 2: 
					switch(level) {
						case 1:
							SceneManager.LoadScene("M2");
							break;
						case 2:
							SceneManager.LoadScene("M3");
							break;
						case 3:
							SceneManager.LoadScene("M4");
							break;
						case 4:
							SceneManager.LoadScene("M5");
							break;
						case 5:
							SceneManager.LoadScene("MainMenu");
							break;
					}
					break;
				case 3: 
					switch(level) {
						case 1:
							SceneManager.LoadScene("H2");
							break;
						case 2:
							SceneManager.LoadScene("H3");
							break;
						case 3:
							SceneManager.LoadScene("H4");
							break;
						case 4:
							SceneManager.LoadScene("H5");
							break;
						case 5:
							SceneManager.LoadScene("MainMenu");
							break;
					}
					break;
			}
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "FallDetector")
		{
			transform.position = respawnPoint;
		}

        if (collision.gameObject.tag == "Ground")
        {
            capsuleCollider.isTrigger = false;
        }

        if (collision.gameObject.tag == "Chest") {
			if(numbers.Count == 2 && keys < 5) {
				Debug.Log("chest has been triggered");
                if (numbers[0].GetComponent<NumScript>().value == collision.gameObject.GetComponent<NumberSpawn>().first && numbers[1].GetComponent<NumScript>().value == collision.gameObject.GetComponent<NumberSpawn>().second || numbers[1].GetComponent<NumScript>().value == collision.gameObject.GetComponent<NumberSpawn>().first && numbers[0].GetComponent<NumScript>().value == collision.gameObject.GetComponent<NumberSpawn>().second) {
					keys++;
                    SoundManager.PlaySound("Solve");
                    uiHud.GetComponent<TMP_Text>().text = "Keys: " + keys.ToString() + "/5";
					//Destroy(numbers[0]);
					//Destroy(numbers[1]);
					if(keys < 5) {
						collision.gameObject.transform.position = collision.gameObject.GetComponent<NumberSpawn>().chestLocations.transform.GetChild(keys - 1).transform.position;
						numbers = new List<GameObject>();
						collision.gameObject.GetComponent<NumberSpawn>().Spawn();
					}
					else {
						equationText.GetComponent<TMP_Text>().text = "Level Complete! Head towards the house and press the Enter key";
						spawnedNums.SetActive(false);
						chest.SetActive(false);
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
