using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MindImage : MonoBehaviour{

	public Sprite trans;
	public Sprite cross;
	public Sprite circle;
	
	private Image mImage;
	private Image validatorImage;

	private MindColor mColor = 0;

	private MindColor masterColor = 0;

	private void Awake(){
		mImage = GetComponent<Image>();
		validatorImage = transform.GetChild(0).gameObject.GetComponent<Image>();
	}

	// Use this for initialization
	void Start () {
		ResetMindImage();
	}
	
	// Update is called once per frame
	void Update () {
		if (mColor == MindColor.Yellow){
			mImage.color = Color.yellow;
		}else if (mColor == MindColor.Blue){
			mImage.color = Color.blue;
		}else if (mColor == MindColor.Green){
			mImage.color = Color.green;
		}else{
			mImage.color = Color.magenta;
		}
	}

	public MindColor GetCurrentColor(){
		return mColor;
	}

	public MindColor GetMasterColor(){
		return masterColor;
	}

	public bool CheckColor(){
		if (mColor == masterColor){
			validatorImage.sprite = circle;
			return true;
		}else{
			validatorImage.sprite = cross;
			return false;
		}
	}

	public void ResetMindImage(){
		masterColor = (MindColor) Random.Range(0, 4);
		mColor = MindColor.Yellow;
		validatorImage.sprite = trans;
	}

	public void cycleColor(){
		switch (mColor){
			case MindColor.Yellow:
				mColor = MindColor.Blue;
				break;
			case MindColor.Blue:
				mColor = MindColor.Green;
				break;
			case MindColor.Green:
				mColor = MindColor.Purple;
				break;
			case MindColor.Purple:
				mColor = MindColor.Yellow;
				break;
			default:
				mColor = MindColor.Yellow;
				break;
		}
	}
	
}
