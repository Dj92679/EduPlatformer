using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float speed; // platform speed
    public int startingPoint; // starting position of platform
    public Transform[] points; // array of transform points platform moves to
    private int i; // index of array
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position; // Setting position of the platform with position from startingPoint
    }

    // Update is called once per frame
    void Update()
    {
        // Checks the distance between the platform and the point position
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++; // Increases index value
            if (i == points.Length) // Checks if the platform has reached the last point
            {
                i = 0; // Resets index value
            }
        }

        // Moves platform to the point position according to the index value
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.transform.position.y > transform.position.y)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
