using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Goes on the player object */
public class MovementController : MonoBehaviour {
	public Rigidbody playerBody;
	public Camera playerCam;
	public float moveSpeed;
	public float jumpForce;

	void Start () {
		playerBody = this.GetComponent<Rigidbody> ();
	}

	void Update () {
		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * moveSpeed;
		float z = Input.GetAxis ("Vertical") * Time.deltaTime * moveSpeed;

		Vector3 mousePos = Input.mousePosition;
		float mousePosToPlayer = Input.mousePosition.x - transform.position.x;

		transform.LookAt (playerCam.ScreenToWorldPoint (mousePos), Vector3.up); 

		transform.Translate (x, 0, z);
	}
}
