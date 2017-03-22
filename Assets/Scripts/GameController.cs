using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController controller;
	public bool isPaused;
	public bool playerShouldShoot;

	// Use this for initialization
	void Awake () {
		// Singleton
		if (controller == null){
			controller = this;
			DontDestroyOnLoad(this);
		} else {
			Destroy(this.gameObject);
		}
	}

	void Start (){
		resetGame();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void pauseGame(){
		// Pause the game
		isPaused = true;
		// Free the mouse cursor
		Cursor.lockState = CursorLockMode.None;
	}

	public void unpauseGame(){
		// Unpause the game
		isPaused = false;
		// Free the mouse cursor
		Cursor.lockState = CursorLockMode.Locked;
	}

	void resetGame(){
		unpauseGame();
		playerShouldShoot = true;
	}

	public void restartCurrentLevel(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		resetGame();
	}
}
