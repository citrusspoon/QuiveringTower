using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour {
	// Use this manager to handle UI activity
	public Text timerText;
	public Image timerFill;
	public Text scoreText;
	public GameObject crosshair, cannotShootIcon;

	private float turnTimeLeft;
	public float totalTurnTime;
	private GameController gameController;
	[SerializeField] Text ingameTextMessage;

	// Use this for initialization
	void Start () {
		turnTimeLeft = totalTurnTime;
		gameController = GameController.controller;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameController.isPaused) {
			turnTimeLeft -= Time.deltaTime;
			timerText.text = Mathf.RoundToInt (turnTimeLeft).ToString ();
		}
		timerFill.fillAmount = turnTimeLeft / totalTurnTime;

		scoreText.text = "Score: " + RulesManager.manager.playerScore.ToString();
		crosshair.SetActive(RulesManager.manager.playerCanShoot);

		if (gameController.playerShouldShoot){
			cannotShootIcon.SetActive(false);
		} else {
			cannotShootIcon.SetActive(true);
		}
	}

	void displayMessage(string message){
		ingameTextMessage.text = message;
	}

}
