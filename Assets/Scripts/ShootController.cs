using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour {
	public ArrowController pushArrow, pullArrow, dissolveArrow, rocketArrow, freezeArrow;
	private GameObject arrowClone;
	public Transform playerFace;
	public Transform firePoint;
	public float shootForce = 500.0f;
	private Quaternion rotation = Quaternion.identity;
	private ArrowController selectedArrow = null;

	// NOTE> Can we do this in a more elegant way?
	public Image drawPowerMeter;


	private float currentShootPower = 0.0f;
	private int shootPowerSpeed = 75;

	void Update () {
		if (GameObject.FindObjectOfType<GameManager>().isGamePaused()){
			return;
		}

		if (Input.GetButtonDown ("Fire1")) {
			currentShootPower = 0;
		} else if (Input.GetButtonDown ("Select Normal Arrow")) {
			chooseArrowType(pushArrow);
			print ("Selected normal arrow");
		} else if (Input.GetButtonDown ("Select Dissolve Arrow")) {
			chooseArrowType(dissolveArrow);
			print ("Selected dissolve arrow");
		} else if (Input.GetButtonDown ("Select Pull Arrow")) {
			chooseArrowType(pullArrow);
			print ("Selected pull arrow");
		} else if (Input.GetButton ("Select Rocket Arrow")){
			chooseArrowType(rocketArrow);
			print("Selected Rocket Arrow");
		} else if (Input.GetButton ("Select Freeze Arrow")){
			chooseArrowType(freezeArrow);
			print("Selected Freeze Arrow");
		} else if (Input.GetButton ("Fire1")) {
			currentShootPower += Mathf.Clamp(shootPowerSpeed * Time.deltaTime,0,100);
		} else if (Input.GetButtonUp ("Fire1")) {
			if (currentShootPower > 0){
				ShootArrow ();
				currentShootPower = 0;
			}
		}

		drawPowerMeter.fillAmount = currentShootPower / 100;
    }

	private void chooseArrowType(ArrowController arrowType){
		selectedArrow = arrowType;
	}

    void ShootArrow() {
		rotation = firePoint.rotation * Quaternion.Euler (new Vector3 (90, 0));
        arrowClone = Instantiate(selectedArrow.gameObject,firePoint.position, rotation);
		arrowClone.GetComponent<Rigidbody> ().AddForce (playerFace.forward * shootForce * (currentShootPower / 100));	//	Add force to it
		DestroyClone ();

		GetComponent<AudioSource> ().Play ();
	}

	private void DestroyClone () {
		Destroy (arrowClone, 10);
	}

}
