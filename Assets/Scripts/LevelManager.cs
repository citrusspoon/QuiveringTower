using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public void StartNewGame(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Main");
	}


}
