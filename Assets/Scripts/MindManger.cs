using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MindManger : MonoBehaviour{

	private Slider powerMeter;
	private Text gameInfo;
	private Text muteText;
	private AudioSource correct;
	private AudioSource wrong;
	private AudioSource music;

	private bool playMusic = true;
	
	public List<MindImage> MindImages = new List<MindImage>();

	public float maxTime = 60f;
	public float powerTimer = 60f;
	public float gameTimer = 0f;

	public int sequencesGuessed = 0;
	public int numOfAttempts = 0;
	public float bonusTime = 20f;

	private void Awake(){
		correct = GameObject.Find("correct").GetComponent<AudioSource>();
		wrong = GameObject.Find("wrong").GetComponent<AudioSource>();
		music = GameObject.Find("music").GetComponent<AudioSource>();
		gameInfo = GameObject.Find("gameInfo").GetComponent<Text>();
		muteText = GameObject.Find("muteText").GetComponent<Text>();
		powerMeter = GameObject.Find("powerMeter").GetComponent<Slider>();
		MindImages.Add(GameObject.Find("panelOne").GetComponent<MindImage>());
		MindImages.Add(GameObject.Find("panelTwo").GetComponent<MindImage>());
		MindImages.Add(GameObject.Find("panelThree").GetComponent<MindImage>());
		MindImages.Add(GameObject.Find("panelFour").GetComponent<MindImage>());
		powerTimer = maxTime;
	}

	// Use this for initialization
	void Start (){
		
	}
	
	// Update is called once per frame
	void Update (){
		powerTimer -= Time.deltaTime;
		if (powerTimer >= 0){
			powerMeter.value = powerTimer / maxTime;
			gameTimer += Time.deltaTime;
		}
		else{
			String endText = "You guessed " + sequencesGuessed + " generator codes to keep the power going for "
			                 + String.Format("{0:.##}", (gameTimer / 60))
				+ " minutes! Click the generator button to try again.";
			gameInfo.text = endText;
		}
	}

	public void CheckAnswers(){
		bool allCorrect = true;
		
		foreach (MindImage image in MindImages){
			bool isCorrect = image.CheckColor();
			if (!isCorrect){
				allCorrect = false;
			}
		}

		if (allCorrect){
			correct.Play();
			Debug.Log("Good Job! All Correct!");
			sequencesGuessed++;
			powerTimer += bonusTime / numOfAttempts;
			numOfAttempts = 0;
			foreach (MindImage image in MindImages){
				image.ResetMindImage();
			}
		}else{
			wrong.Play();
			Debug.Log("Fix the mistakes!");
			numOfAttempts++;
		}
	}

	public void GeneratorButton(){
		if (powerTimer >= 0){
			CheckAnswers();
		}
		else{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void MuteMusic(){
		if (playMusic){
			music.Stop();
			playMusic = false;
			muteText.text = "Play Music";
		}else{
			music.Play();
			playMusic = true;
			muteText.text = "Mute Music";
		}
	}
}
