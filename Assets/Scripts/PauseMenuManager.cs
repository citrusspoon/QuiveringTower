using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour {

	public enum PAUSE {
		PAUSED=1, UNPAUSED=0
	}

	private int pauseState;

	void Start () {
		SetPauseState (PAUSE.UNPAUSED);
	}

	public void SetPauseState (PauseMenuManager.PAUSE flag) {
		this.pauseState = (int)flag;
		ToggleMenu ();
	}

	public void ToggleMenu () {
		if (pauseState == 1) {
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

	public int getPauseState () {
		return pauseState;
	}


}
