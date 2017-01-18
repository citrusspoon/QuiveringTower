using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Goes on the player object */
public class MovementController : MonoBehaviour {
	public Rigidbody playerBody;
	public CharacterController cc;

	public float moveSpeed = 10.0f;
	public float jumpForce;

	public float mouseSensitivity = 5.0f;

	public float verticalRotation = 0;
	public float upDownRange = 60.0f;

	void Start () {
		playerBody = this.GetComponent<Rigidbody> ();
		cc = GetComponent<CharacterController> ();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update () {
		/* Rotation */
		transform.Rotate (0, Input.GetAxis ("Mouse X") * mouseSensitivity , 0);

		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);

		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0); 

		/* Movement */
		float sideMovement = Input.GetAxis ("Horizontal") * moveSpeed;
		float fowardMovement = Input.GetAxis ("Vertical") * moveSpeed;

		Vector3 speed = new Vector3 (sideMovement, 0, fowardMovement);

		speed = transform.rotation * speed;
		cc.SimpleMove (speed);
	}
}
