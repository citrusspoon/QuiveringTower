using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text timerText;
	public Image timerFill;

	private float turnTimeLeft;
	public float totalTurnTime;

	// Use this for initialization
	void Start () {
		turnTimeLeft = totalTurnTime;
	}
	
	// Update is called once per frame
	void Update () {
		turnTimeLeft -= Time.deltaTime;
		timerText.text = Mathf.RoundToInt (turnTimeLeft).ToString();

		//print (turnTimeLeft / totalTurnTime);
		timerFill.fillAmount = turnTimeLeft / totalTurnTime;
	}
}
