using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
	public Text remainingTimeText;
	public void UpdateRemainingTime(){
		if(remainingTimeText != null){
			remainingTimeText.text = GameControl.self.timeUntilNext.ToString("0.0");
		}
		
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRemainingTime();
	}
}
