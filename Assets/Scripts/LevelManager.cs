using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

public static LevelManager manager = null;
public enum GameMode{SinglePlayerFreeplay, SinglePlayerChallange, HotSeat}

	void Start(){
		if (manager == null){
			manager = this;
			DontDestroyOnLoad(this);
		} else {
			GameObject.Destroy(this.gameObject);
		}
	}

	public void StartNewGame(GameMode gameMode){
		switch (gameMode){
			case GameMode.SinglePlayerFreeplay:
				UnityEngine.SceneManagement.SceneManager.LoadScene ("Main");
				if (GameController.controller != null){
					GameController.controller.resetGame();
				}
				break;
			case GameMode.SinglePlayerChallange:
				UnityEngine.SceneManagement.SceneManager.LoadScene ("Level_01");
				if (GameController.controller != null){
					GameController.controller.resetGame();
				}
				break;
			case GameMode.HotSeat:
				UnityEngine.SceneManagement.SceneManager.LoadScene ("Hot_Level_01");
				if (GameController.controller != null){
					GameController.controller.resetGame();
				}
				break;
		}
	}


}
