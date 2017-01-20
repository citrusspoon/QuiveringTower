using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {
	public GameObject arrowPrefab;
	private GameObject arrowClone;
	public Transform playerFace;
	public Transform firePoint;
	public float shootForce = 500.0f;
	public Quaternion rotation = Quaternion.identity;
	private Vector3 center;

	void Start () {
	}

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			arrowClone = Instantiate (arrowPrefab, firePoint.position, rotation);
			arrowClone.GetComponent<Rigidbody> ().isKinematic = false;
			arrowClone.GetComponent<Rigidbody> ().AddForce (transform.rotation.eulerAngles * shootForce);
		}
	}
}
