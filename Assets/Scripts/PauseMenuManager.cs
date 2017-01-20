using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour {

	/* Close the pause menu */
	public void CloseMenu () {
		this.GetComponent<Canvas> ().enabled = false;
	}

	public void OpenSettings () {
		//	Open the settings menu
	}

	public void ExitGame () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu");
	}

}
