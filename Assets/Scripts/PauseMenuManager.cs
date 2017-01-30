using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour {

	void Start () {
		CloseMenu ();
	}

	/* Close the pause menu */
	public void CloseMenu () {
		this.GetComponent<Canvas> ().enabled = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void ShowMenu (){
		this.GetComponent<Canvas> ().enabled = true;
		// Should be Confined but somehow does not work in Unity Editor
		Cursor.lockState = CursorLockMode.None;
	}

	public void OpenSettings () {
		//	Open the settings menu
	}

	public void ExitGame () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu");
	}


}
