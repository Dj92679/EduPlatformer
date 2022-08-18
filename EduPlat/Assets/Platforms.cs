using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    Collider2D platformCollider;
    private bool playerOnPlatform;
    // Start is called before the first frame update
    void Start()
    {
        platformCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
            {
                platformCollider.enabled = false;
                StartCoroutine(EnableCollider());
            }
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        platformCollider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         SetPlayerOnPlatform(collision, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SetPlayerOnPlatform(collision, false);
    }



    
}
