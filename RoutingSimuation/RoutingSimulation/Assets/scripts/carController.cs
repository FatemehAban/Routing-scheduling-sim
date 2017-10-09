using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{

	private float SPEED = 2.0f;
	private Vector3 direction;
	private float bound = 0.0f;
	private static int randomness = 15;

	// Use this for initialization
	void Start ()
	{
		//SPEED = Random.Range (0.5f, 2.5f);
		bound = controller.bound;
		direction = transform.right;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += direction * SPEED * Time.deltaTime;
		checkBoundary ();
	}

	void checkBoundary ()
	{

		if (transform.position.x > bound) { 
			transform.position = new Vector3 (bound - 0.5f, transform.position.y, transform.position.z);
			float angle = Random.Range ((180.0f - transform.eulerAngles.y) + randomness,(180.0f - transform.eulerAngles.y) - randomness);
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, angle ,transform.eulerAngles.z);
		}

		if (transform.position.z > bound) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, bound - 0.5f);
			float angle = Random.Range ((360.0f - transform.eulerAngles.y) + randomness,(360.0f - transform.eulerAngles.y)- randomness);
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, angle ,transform.eulerAngles.z);
		}
		
		if (transform.position.x < -bound) {
			transform.position = new Vector3 (-bound + 0.5f, transform.position.y, transform.position.z);
			float angle = Random.Range ((360.0f - transform.eulerAngles.y +180.0f) + randomness,(360.0f - transform.eulerAngles.y +180.0f)- randomness);
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x,angle ,transform.eulerAngles.z);
		}

		if (transform.position.z < -bound) { 
			transform.position = new Vector3 (transform.position.x, transform.position.y, -bound + 0.5f);
			float angle = Random.Range ((270.0f - transform.eulerAngles.y +90.0f) + randomness,(270.0f - transform.eulerAngles.y +90.0f)- randomness);
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, (270.0f - transform.eulerAngles.y +90.0f),transform.eulerAngles.z);
		}
			
		direction = transform.right;

	}



}
