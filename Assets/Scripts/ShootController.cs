using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {
	public GameObject arrowPrefab;
    public GameObject dissolveArrowPrefab;
    public GameObject pullArrowPrefab;
    public int currentArrow = 1; //  1 = normal | 2 = dissolve | 3 = pull
	private GameObject arrowClone;
	public Transform playerFace;
	public Transform firePoint;
	public float shootForce = 500.0f;
	public Quaternion rotation = Quaternion.identity;

	void Update () {
        if (Input.GetButtonDown("Fire1")) {
            ShootArrow();
        }
        else if (Input.GetButtonDown("Select Normal Arrow")) {
            currentArrow = 1;
            print("Selected normal arrow");
        }
        else if (Input.GetButtonDown("Select Dissolve Arrow"))
        {
            currentArrow = 2;
            print("Selected dissolve arrow");
        }
        else if (Input.GetButtonDown("Select Pull Arrow"))
        {
            currentArrow = 3;
            print("Selected pull arrow");
        }
    }

    void ShootArrow() {

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

            

    
		arrowClone.GetComponent<Rigidbody> ().AddForce (playerFace.forward * shootForce);	//	Add force to it
		DestroyClone ();
	}

	private void DestroyClone () {
		Destroy (arrowClone, 2);
	}

}
