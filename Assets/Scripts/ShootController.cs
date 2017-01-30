using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour {
	public GameObject arrowPrefab;
    public GameObject dissolveArrowPrefab;
    public GameObject pullArrowPrefab;
    public int currentArrow = 1; //  1 = normal | 2 = dissolve | 3 = pull
	private GameObject arrowClone;
	public Transform playerFace;
	public Transform firePoint;
	public float shootForce = 500.0f;
	private Quaternion rotation = Quaternion.identity;

	// NOTE> Can we do this in a more elegant way?
	public Image drawPowerMeter;


	private float currentShootPower = 0.0f;
	private int shootPowerSpeed = 75;

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			currentShootPower = 0;
			//ShootArrow();
		} else if (Input.GetButtonDown ("Select Normal Arrow")) {
			currentArrow = 1;
			print ("Selected normal arrow");
		} else if (Input.GetButtonDown ("Select Dissolve Arrow")) {
			currentArrow = 2;
			print ("Selected dissolve arrow");
		} else if (Input.GetButtonDown ("Select Pull Arrow")) {
			currentArrow = 3;
			print ("Selected pull arrow");
		} else if (Input.GetButton ("Fire1")) {
			currentShootPower += Mathf.Clamp(shootPowerSpeed * Time.deltaTime,0,100);
		} else if (Input.GetButtonUp ("Fire1")) {
			ShootArrow ();
		}

		drawPowerMeter.fillAmount = currentShootPower / 100;
    }

    void ShootArrow() {
		rotation = firePoint.rotation * Quaternion.Euler (new Vector3 (90, 0));
        switch (currentArrow)
        {
            case 1:
                arrowClone = Instantiate(arrowPrefab, firePoint.position, rotation);    //	Create the arrow
                break;
            case 2:
                arrowClone = Instantiate(dissolveArrowPrefab, firePoint.position, rotation);    //	Create the arrow
                break;
            case 3:
                arrowClone = Instantiate(pullArrowPrefab, firePoint.position, rotation);    //	Create the arrow
                break;
            default:
                arrowClone = Instantiate(arrowPrefab, firePoint.position, rotation);    //	Create the arrow
                break;
        }

            

    
		arrowClone.GetComponent<Rigidbody> ().AddForce (playerFace.forward * shootForce * (currentShootPower / 100));	//	Add force to it
		DestroyClone ();

		GetComponent<AudioSource> ().Play ();
	}

	private void DestroyClone () {
		Destroy (arrowClone, 2);
	}

}
