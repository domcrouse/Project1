using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158 - James Quinney
// This script determines that an object can move, how fast it moves and the direction it moves
public class RailObject : MonoBehaviour
{
	[SerializeField]
	float searchDistance = 0.5f; // The distance the object will search for a waypoint within
	public GameObject currentWaypoint; // The waypoint the object is currently moving towards
	[SerializeField]
	Rigidbody2D rb; // The rigidbody attached to the object
	[SerializeField]
	[Range(0.1f, 30.0f)]
	float speed = 1.0f;
	public float speedMultiplier = 1.0f; // This is the speed of the tower when influenced by other scripts.
	[SerializeField]
	bool destroyOnReach; // Whether the object destroys itself when it reaches its final waypoint
	public bool reversed; // If reversed, the object will go backwards
	[SerializeField]
	Transform body; // The body that rotates in the direction of the track

    // Update is called once per frame
    void Update()
    {
		// Check to make sure there is a waypoint to move to
		if(currentWaypoint != null){
			// If we have not yet reached our waypoint
			if(Vector3.Distance(currentWaypoint.transform.position, transform.position) > searchDistance){
				rb.velocity = Vector3.Normalize(currentWaypoint.transform.position - transform.position) * speed * speedMultiplier; // Move towards the waypoint
				// Check if the object is moving at full speed
				if(speedMultiplier == 1.0f && body != null){
					Vector3 direction = currentWaypoint.transform.position - body.position; // Work out the direction
					float ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f; // Work out the angle
					Quaternion rot = Quaternion.AngleAxis(ang, Vector3.forward); // The angle in quaternion form
					body.rotation = rot; // Set the rotation
				}
			}
		}
    }

	// Choose a waypoint from a list
	void SelectWaypoint(){
		// Ensure there is a waypoint to move to
		if(currentWaypoint != null){
			List<GameObject> others; // This is where we will store possible waypoints
			// Check if the object is reversed
			if(reversed){
				others = currentWaypoint.GetComponent<Waypoint>().parentWaypoints; // We store all of the possible waypoints if the object is reversed
			}
			else{
				others = currentWaypoint.GetComponent<Waypoint>().other; // We store all of the possible waypoints if the object is not reversed
			}

			// Check to see if there is a new waypoint to move to
			if(others.Count > 0){
				currentWaypoint = others[Random.Range(0,others.Count)]; // We move to a new waypoint at random
			}
			// If there are no waypoints to move to
			else{
				transform.position = currentWaypoint.transform.position; // Ensure we stop exactly on our waypoint
				rb.velocity = Vector3.zero; // Stop our object
				GameObject savedWaypoint = currentWaypoint; // This allows us to tell the object which waypoint it reached after it has reached it
				currentWaypoint = null; // We tell the object that it no longer needs to move towards the waypoint
				OnReachEnd(savedWaypoint); // Tell the object it has reached its waypoint
			}
		}
	}

	// Ensure the object definitely changes irection when touching a waypoint
	void OnTriggerEnter2D(Collider2D collider){
		// Ensure we have reached a waypoint
		if(collider.gameObject.GetComponent<Waypoint>() != null){
			// Check to see if we have reached our waypoint
			if(currentWaypoint == null || currentWaypoint == collider.gameObject){
				currentWaypoint = collider.gameObject; // Set our next waypoint
				SelectWaypoint(); // Select one of our new waypoint's destinations
			}
		}
	}

	// When the object reaches its final waypoint
	void OnReachEnd(GameObject waypoint){
		// Check if the object should be destroyed at the end
		if(destroyOnReach){
			Destroy(gameObject); // Destroy the object
		}
		else{
			Vector2 pos = transform.position; // We store the current position of the object
			transform.position = new Vector2(pos.x + Random.Range(-searchDistance,searchDistance),pos.y + Random.Range(-searchDistance, searchDistance)); // We offset the position by a random value
		}
	}
}
