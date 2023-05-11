using UnityEngine;

public class PathFollow : MonoBehaviour
{
    public GameObject path;  // Path to follow
    public Transform[] waypoints;  // Waypoints to follow
    public float speed = 5.0f;  // Speed of movement along the path
    public bool loop = false;   // Whether to loop through the waypoints or stop at the last one

    private int currentWaypoint = 0;  // Index of the current waypoint
    private bool forward = true;  // Whether to move forward or backward along the path

    void Start()
    {
        // Get all waypoints from the children of path GameObject
        waypoints = new Transform[path.transform.childCount];
        for (int i = 0; i < path.transform.childCount; i++)
        {
            waypoints[i] = path.transform.GetChild(i);
        }

        // Set position to the first waypoint
    }

    void Update()
    {
        // Calculate the distance to the current waypoint
        float distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);

        // Check if we have reached the current waypoint
        if (distance < 0.1f)
        {
            // If we're moving forward and haven't reached the last waypoint, move to the next one
            if (forward && currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
            }
            // If we're moving backward and haven't reached the first waypoint, move to the previous one
            else if (!forward && currentWaypoint > 0)
            {
                currentWaypoint--;
            }
            // If we've reached the last waypoint and are looping, start over from the first one
            else if (loop && forward)
            {
                currentWaypoint = 0;
            }
            // If we've reached the first waypoint and are looping, start over from the last one
            else if (loop && !forward)
            {
                currentWaypoint = waypoints.Length - 1;
            }
            // Otherwise, stop moving
            else
            {
                return;
            }
        }

        // Calculate the direction to the current waypoint
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;

        // Normalize the direction vector and rotate it to point forward
        direction.Normalize();

        print(direction + " " + waypoints[currentWaypoint].position + " " + transform.position);

        // Move the object in the direction of the current waypoint
        transform.position += direction * speed * Time.deltaTime;
    }
}