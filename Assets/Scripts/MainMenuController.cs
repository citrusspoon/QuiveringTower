using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
	[SerializeField] private Button SinglePlayerFreePlay, SinglePlayerChallanges;

	void Start(){
		SinglePlayerChallanges.onClick.AddListener(StartSinglePlayerChallanges);
		SinglePlayerFreePlay.onClick.AddListener(StartSinglePlayerFreePlay);
	}

	void StartSinglePlayerChallanges(){
		LevelManager.manager.StartNewGame(LevelManager.GameMode.SinglePlayerChallange);
	}

	void StartSinglePlayerFreePlay(){
		LevelManager.manager.StartNewGame(LevelManager.GameMode.SinglePlayerFreeplay);
	}

}
