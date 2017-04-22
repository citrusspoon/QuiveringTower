using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

	[SerializeField] private float startHeight;
	[SerializeField] private float endHeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (endHeight > startHeight) {
			transform.position = new Vector3 (transform.position.x, Mathf.PingPong (Time.time, Mathf.Abs (endHeight - startHeight)) + startHeight, transform.position.z);
		} else if(endHeight < startHeight){
			transform.position = new Vector3 (transform.position.x, -1*(Mathf.PingPong (Time.time, Mathf.Abs (endHeight - startHeight)) - startHeight), transform.position.z);
		}
	}
}
