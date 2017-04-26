using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController controller;
	public bool isPaused;
	public bool playerShouldShoot;

	public Player player1, player2;
	private Player activePlayer;


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

	public void resetGame(){
		unpauseGame();
		player1.gameObject.SetActive(true);
		player2.gameObject.SetActive(false);
		activePlayer = player1;

		playerShouldShoot = true;
	}

	public void restartCurrentLevel(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		resetGame();
	}

	public void nextPlayer(){
		if(player1.gameObject.activeSelf){
			player1.gameObject.SetActive(false);
			player2.gameObject.SetActive(true);
			activePlayer = player2;
		} else {
			player1.gameObject.SetActive(true);
			player2.gameObject.SetActive(false);
			activePlayer = player1;
		}

	}

	public void nextChallenge(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(
			UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1
		);
		resetGame();
	}
}
