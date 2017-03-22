using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Goes on the player object */
public class MovementController : MonoBehaviour {
	public CharacterController cc;

	public float moveSpeed = 10.0f;	//	Speed of the player
	public float jumpForce = 200.0f;

	public float mouseSensitivity = 5.0f;	//	Look sensitivity of mouse

	public float verticalLook = 0;	//	Default vertical rotation value
	public float verticalLookRange = 60.0f;	//	Range of vertical look

	private float verticalVelocity = 0;
	public float gravityMultiplier = 8.0f;
	private GameController gameController;

	void Start () {
		cc = GetComponent<CharacterController> ();	//	Character Controller Component
		Cursor.lockState = CursorLockMode.Locked;	//	Renders the cursor invisible
		gameController = GameController.controller;
	}

	void Update () {
		if (!gameController.isPaused){
			/* Player & camera rotation */
		transform.Rotate (0, Input.GetAxis ("Mouse X") * mouseSensitivity , 0);	//	Turn the player with the mouse x position

		verticalLook -= Input.GetAxis ("Mouse Y") * mouseSensitivity;	//	Subtract mouse y position from vertical look
		verticalLook = Mathf.Clamp (verticalLook, -verticalLookRange, verticalLookRange);	//	Clamp the range

		Camera.main.transform.localRotation = Quaternion.Euler (verticalLook, 0, 0); //	Set the transform rotation of the camera

		/* Player movement */
		float sideMovement = Input.GetAxis ("Horizontal") * moveSpeed;	//	Get left/right input
		float forwardMovement = Input.GetAxis ("Vertical") * moveSpeed;	//	Get forward/backward input

		/* Jump mechanics */
		if (!cc.isGrounded) {
			verticalVelocity += Physics.gravity.y * gravityMultiplier* Time.deltaTime;
		}

		if (Input.GetButtonDown ("Jump") && cc.isGrounded) {	//	Jump if the player is on the ground
			verticalVelocity = jumpForce;
		}

		Vector3 movement = new Vector3 (sideMovement, verticalVelocity, forwardMovement);	//	Movement vector3
		movement = transform.rotation * movement;

		cc.Move (movement * Time.deltaTime);	//	Move the player
		}
	}
}
