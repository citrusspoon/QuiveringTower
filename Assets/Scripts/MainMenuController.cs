using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
	[SerializeField] private Button SinglePlayerFreePlay, SinglePlayerChallanges, HotSeat, ExitGame;

	void Start(){
		SinglePlayerChallanges.onClick.AddListener(StartSinglePlayerChallanges);
		SinglePlayerFreePlay.onClick.AddListener(StartSinglePlayerFreePlay);
		HotSeat.onClick.AddListener(StartHotSeat);
		ExitGame.onClick.AddListener(StartExitGame);
		Cursor.lockState = CursorLockMode.None;
	}

	void StartSinglePlayerChallanges(){
		LevelManager.manager.StartNewGame(LevelManager.GameMode.SinglePlayerChallange);
	}

	void StartSinglePlayerFreePlay(){
		LevelManager.manager.StartNewGame(LevelManager.GameMode.SinglePlayerFreeplay);
	}

	void StartHotSeat(){
		LevelManager.manager.StartNewGame(LevelManager.GameMode.HotSeat);
	}

	void StartExitGame(){
		Application.Quit();
	}

}
