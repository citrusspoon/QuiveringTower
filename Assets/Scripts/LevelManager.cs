using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

public static LevelManager manager = null;
public enum GameMode{SinglePlayerFreeplay, SinglePlayerChallange}

	void Start(){
		if (manager == null){
			manager = this;
			DontDestroyOnLoad(this);
		} else {
			GameObject.Destroy(this.gameObject);
		}
	}

	public void StartNewGame(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Main");
	}

	public void StartNewGame(GameMode gameMode){
		switch (gameMode){
			case GameMode.SinglePlayerFreeplay:
			StartNewGame();
			break;
			case GameMode.SinglePlayerChallange:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Level_01");
			break;
		}
	}


}
