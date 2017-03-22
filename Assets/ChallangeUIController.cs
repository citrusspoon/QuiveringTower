using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChallangeUIController : MonoBehaviour {

	[SerializeField] Text messageText;
	[SerializeField] Button retryButton, nextButton;

	void Start(){
		reset();
	}

	public void challangeSuccess(){
		
		messageText.text = "Challange Completed";
		messageText.enabled = true;
		retryButton.gameObject.SetActive(true);
		nextButton.gameObject.SetActive(true);
	}

	public void challangeFail(){
		messageText.text = "Challange Failed";
		messageText.enabled = true;
		retryButton.gameObject.SetActive(true);
		nextButton.gameObject.SetActive(true);
	}

	public void reset(){
		messageText.enabled = false;
		retryButton.gameObject.SetActive(false);
		nextButton.gameObject.SetActive(false);
	}

}
