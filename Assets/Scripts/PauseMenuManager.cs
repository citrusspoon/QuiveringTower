using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour {

	public enum PAUSE_STATE {
		PAUSED, UNPAUSED
	}

	private PAUSE_STATE pauseState;

	void Start () {
		SetPauseState (PAUSE_STATE.UNPAUSED);
	}

	public void SetPauseState (PauseMenuManager.PAUSE_STATE flag) {
		this.pauseState = flag;
		ToggleMenu ();
	}

	public void ToggleMenu () {
		if (pauseState == PAUSE_STATE.PAUSED) {
			this.GetComponent<Canvas> ().enabled = true;
		} else {
			this.GetComponent<Canvas> ().enabled = false;
		}
			
	}

	public void OpenSettings () {
		//	Open the settings menu
	}

	public void ExitGame () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu");
	}

	public PAUSE_STATE getPauseState () {
		return pauseState;
	}

	public void resumeGame(){
		SetPauseState(PauseMenuManager.PAUSE_STATE.UNPAUSED);
	}


}
