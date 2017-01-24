using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	private static MusicManager singleMusicManager;

	// Using Load function to use a singleton pattern
	void Awake () {
		if (singleMusicManager == null) {
			DontDestroyOnLoad (this.gameObject);
			singleMusicManager = this;
		} else {
			GameObject.Destroy (this.gameObject);
		}
	}

	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
}
