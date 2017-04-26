using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController controller;
	public bool isPaused;
	public bool playerShouldShoot;

	public List<Player> players;
	public Player activePlayer;


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

	void Update(){
		if (Input.GetKeyDown(KeyCode.Space) && activePlayer.isWaitingPlayerInput){
			activePlayer.isWaitingPlayerInput = false;
		}
	}

	public Player getNextPlayer(){
		int activePlayerIndex = players.IndexOf(activePlayer);
		int nextPlayerIndex;
		if (activePlayerIndex == players.Count - 1){
			nextPlayerIndex = 0;
		} else {
			nextPlayerIndex = activePlayerIndex + 1;
		}

		return players[nextPlayerIndex];
	}

	void Start (){
		resetGame();
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

		foreach(Player player in players){
			player.gameObject.SetActive(false);
		}
		activePlayer = players[0];
		players[0].gameObject.SetActive(true);


		playerShouldShoot = true;
	}

	public void restartCurrentLevel(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		resetGame();
	}

	public void nextPlayer(){
		activePlayer.gameObject.SetActive(false);
		activePlayer = getNextPlayer();
		activePlayer.isWaitingPlayerInput = true;
		activePlayer.gameObject.SetActive(true);
	}

	public void nextChallenge(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(
			UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1
		);
		resetGame();
	}
}
