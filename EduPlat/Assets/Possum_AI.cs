using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possum_AI : MonoBehaviour
{
    //reference to waypoints
    public List<Transform> points;
    //Int Value for next point index
    public int nextID = 0;
    //The value of that applies to ID for changing
    int idChangeValue = 1;

    public float speed = 2;

    private void Reset()
    {
        Init();

    }

    //AI Script
    private void Init()
    {
        GetComponent<PolygonCollider2D>().isTrigger = true;
        //Create Root Object
        GameObject root = new GameObject(name + "Root");
        //reset Position of Root to object
        root.transform.position = transform.position;
        //Set enemy object child of root
        transform.SetParent(root.transform);
        //Create Waypoints object
        GameObject waypoints = new GameObject("Waypoints");
        //Reset waypoints position to Root
        //Make waypoints object child of root
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        //Create two points and reset their position to waypoint object
        //Make points children of waypoint object
        GameObject p1 = new GameObject("Point1");
        p1.transform.SetParent(waypoints.transform);
        p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2");
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position;

        //add points to point list
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);


    }


    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        //Flip Enemy Sprite
        if (goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
        //Move the enemy towards the zone
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);
        //Check the distance between the distance between enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, goalPoint.position) < 1f)
        {
            //Check if we are at the end of the line (make the change -1)
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            //Check if we are at the end of the line (make the change +1)
            if (nextID == 0)
                idChangeValue = 1;
            //Apply the change on the nextID
            nextID += idChangeValue;



        }

    }
}
