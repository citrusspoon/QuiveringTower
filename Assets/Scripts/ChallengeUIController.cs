using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChallengeUIController : MonoBehaviour {

	[SerializeField] Text messageText;
	[SerializeField] Button retryButton, nextButton;

	void Start(){
		reset();

		// Connect retry and Next Buttons
		retryButton.onClick.AddListener(retryLevel);
		nextButton.onClick.AddListener(nextLevel);
	}

	public void challangeSuccess(){
		
		messageText.text = "Challenge Completed";
		messageText.enabled = true;
		retryButton.gameObject.SetActive(true);
		nextButton.gameObject.SetActive(true);
	}

	public void challangeFail(){
		messageText.text = "Challenge Failed";
		messageText.enabled = true;
		retryButton.gameObject.SetActive(true);
		nextButton.gameObject.SetActive(true);
	}

	public void reset(){
		messageText.enabled = false;
		retryButton.gameObject.SetActive(false);
		nextButton.gameObject.SetActive(false);
	}

	void retryLevel(){
		GameController.controller.restartCurrentLevel();
	}

	void nextLevel(){
		GameController.controller.nextChallenge();
	}

}
