using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour {

	private GameController gameController;

	void Start(){
		gameController = GameObject.FindObjectOfType<GameController>();
		GetComponent<Canvas> ().enabled = false;
		// TODO CHECK Controller
	}

	void Update(){
		// When the pause button is pressed toggle the pause lockState
		// And hide/display menu accordingly
		if (Input.GetButtonDown ("Pause")) {
			if (!gameController.isPaused) {
				// Display pause menu
				GetComponent<Canvas> ().enabled = true;
				gameController.pauseGame();
			} else {
				resumeGame();
			}
		}
	}

	public void OpenSettings () {
		//	Open the settings menu
	}

	public void ExitGame () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu");
	}

	public void resumeGame(){
		GetComponent<Canvas> ().enabled = false;
		gameController.unpauseGame();
	}

	public void restartLevel(){
		GameController.controller.restartCurrentLevel();
	}


}
