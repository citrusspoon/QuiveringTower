using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	// Use this manager to handle UI activity
	public PauseMenuManager pmm;
	public Text timerText;
	public Image timerFill;
	public Text scoreText;
	public GameObject crosshair, cannotShootIcon;

	private float turnTimeLeft;
	private bool gamePaused = false;
	public float totalTurnTime;

	// Use this for initialization
	void Start () {
		turnTimeLeft = totalTurnTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gamePaused) {
			turnTimeLeft -= Time.deltaTime;
			timerText.text = Mathf.RoundToInt (turnTimeLeft).ToString ();
		}

		//print (turnTimeLeft / totalTurnTime);
		timerFill.fillAmount = turnTimeLeft / totalTurnTime;

		if (Input.GetButtonDown ("Pause")) {
			if (pmm.getPauseState () == PauseMenuManager.PAUSE_STATE.UNPAUSED) {
				pmm.SetPauseState (PauseMenuManager.PAUSE_STATE.PAUSED);
			} else
				pmm.SetPauseState (PauseMenuManager.PAUSE_STATE.UNPAUSED);
		}

		scoreText.text = "Score: " + RulesManager.manager.playerScore.ToString();
		crosshair.SetActive(RulesManager.manager.playerCanShoot);
		cannotShootIcon.SetActive(!RulesManager.manager.playerCanShoot);
	}

	public bool isGamePaused(){
		if (pmm.getPauseState() == PauseMenuManager.PAUSE_STATE.PAUSED){
			return true;
		} else {
			return false;
		}
	}

}
