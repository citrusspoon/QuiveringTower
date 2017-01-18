using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Goes on the player object */
public class MovementController : MonoBehaviour {
	public Rigidbody playerBody;
	public CharacterController cc;

	public float moveSpeed = 10.0f;	//	Speed of the player

	public float mouseSensitivity = 5.0f;	//	Look sensitivity of mouse

	public float verticalLook = 0;	//	Default vertical rotation value
	public float verticalLookRange = 60.0f;	//	Range of vertical look

	void Start () {
		playerBody = this.GetComponent<Rigidbody> ();	//	Rigidbody Component
		cc = GetComponent<CharacterController> ();	//	Character Controller Component
		Cursor.lockState = CursorLockMode.Locked;	//	Renders the cursor invisible
	}

	void Update () {
		/* Player & camera rotation */
		transform.Rotate (0, Input.GetAxis ("Mouse X") * mouseSensitivity , 0);	//	Turn the player with the mouse x position

		verticalLook -= Input.GetAxis ("Mouse Y") * mouseSensitivity;	//	Subtract mouse y position from vertical look
		verticalLook = Mathf.Clamp (verticalLook, -verticalLookRange, verticalLookRange);	//	Clamp the range

		Camera.main.transform.localRotation = Quaternion.Euler (verticalLook, 0, 0); //	Set the transform rotation of the camera

		/* Player movement */
		float sideMovement = Input.GetAxis ("Horizontal") * moveSpeed;	//	Get left/right input
		float fowardMovement = Input.GetAxis ("Vertical") * moveSpeed;	//	Get forward/backward input

		Vector3 movement = new Vector3 (sideMovement, 0, fowardMovement);	//	Set movement to the input values

		movement = transform.rotation * movement;

		cc.SimpleMove (movement);
	}
}
