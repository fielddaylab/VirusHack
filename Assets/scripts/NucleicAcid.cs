using UnityEngine;
using System.Collections;

public class NucleicAcid : MonoBehaviour {
	public char name;
	public bool changeable;

	// Use this for initialization
	void Start () {
		changeable = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ChangeTypeTo(char newName){
		name = newName;
		changeable = false;
		if(newName == 'A'){
			GetComponent<MeshRenderer>().material = GameControl.self.matA;
		} else if(newName == 'G'){
			GetComponent<MeshRenderer>().material = GameControl.self.matG;
		} else if(newName == 'U'){
			GetComponent<MeshRenderer>().material = GameControl.self.matU;
		} else if(newName == 'C'){
			GetComponent<MeshRenderer>().material = GameControl.self.matC;
		}
	}
}
