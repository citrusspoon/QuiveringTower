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
		if (RulesManager.manager != null){
			float turnTimeLeft = RulesManager.manager.turnTimeLeft;

			timerText.text = Mathf.RoundToInt (turnTimeLeft).ToString ();
			timerFill.fillAmount = turnTimeLeft / RulesManager.manager.turnTime;

			crosshair.SetActive(RulesManager.manager.playerCanShoot);
		}
		scoreText.text = "Score: " + GameController.controller.activePlayer.score.ToString();
		

		if (gameController.playerShouldShoot && gameController.activePlayer.isWaitingPlayerInput){
			cannotShootIcon.SetActive(false);
			ingameTextMessage.text = gameController.activePlayer.playerName + " Press Space to Start Turn";
		} else if (gameController.playerShouldShoot && !gameController.activePlayer.isWaitingPlayerInput) {
			cannotShootIcon.SetActive(false);
			ingameTextMessage.text = "";
		} else {
			cannotShootIcon.SetActive(true);
			ingameTextMessage.text = gameController.getNextPlayer().playerName + " Get Ready!";
		}
	}

	void displayMessage(string message){
		ingameTextMessage.text = message;
	}

}
