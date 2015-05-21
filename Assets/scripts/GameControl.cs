using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {
	public static GameControl self;
	public Dictionary<string, AminoAcid> geneticCodeTable;
	public AminoAcid aminoAcidPrefab;
	public string sequence = "UUUAGCGCG";
	public Material matA;
	public Material matG;
	public Material matU;
	public Material matC;
	public NucleicAcid activeNucleicAcid;
	public int activeNucleicAcidIndex;

	public NucleicAcid APrefab;
	public NucleicAcid GPrefab;
	public NucleicAcid UPrefab;
	public NucleicAcid CPrefab;

	public List<NucleicAcid> DNA;

	public float timeUntilNext ;
	void Awake(){
		geneticCodeTable = new Dictionary<string, AminoAcid>();
		CreateAminoAcid("UUU", "UUC", null, null, "Phe");
		CreateAminoAcid("UUA", "UUG", null, null, "Leu");
		CreateAminoAcid("UCU", "UCC", "UCA", "UCG", "Ser");
		CreateAminoAcid("UAU", "UAC", null, null, "Tyr");
		self = this;
		DNA = new List<NucleicAcid>();
		//Construct dna sequence;
		for(int i=0; i<sequence.Length; i++){
			char c = sequence[i];
			NucleicAcid na = CreateNucleicAcid(c);
			if(na != null){
				DNA.Add(na);
			}
		}

	}	
	public NucleicAcid CreateNucleicAcid(char name){
		NucleicAcid na = null;
		if(name == 'A'){
			na = Instantiate(APrefab) as NucleicAcid;
		}
		if(name == 'G'){
			na = Instantiate(GPrefab) as NucleicAcid;
		}
		if(name == 'U'){
			na = Instantiate(UPrefab) as NucleicAcid;
		}
		if(name == 'C'){
			na = Instantiate(CPrefab) as NucleicAcid;
		}
		if(na == null)return na;
		//set position
		if(DNA.Count > 0){
			Vector3 lastPosition = DNA[DNA.Count-1].transform.position;
			lastPosition.x += 1.2f;
			na.transform.position = lastPosition;
		}else{
			na.transform.position = new Vector3(-9f, 0f, 0f);
		}
		
		return na;
	}
	public void CreateAminoAcid(string codon0, string codon1, string codon2, string codon3, string name){
		AminoAcid aa = Instantiate(aminoAcidPrefab) as AminoAcid;
		aa.name = name;
		aa.codons = new List<string>();
		if(codon0 != null){
			aa.codons.Add(codon0);
		}
		if(codon1 != null){
			aa.codons.Add(codon1);
		}
		if(codon2 != null){
			aa.codons.Add(codon2);
		}
		if(codon3 != null){
			aa.codons.Add(codon3);
		}
	}
	// Use this for initialization
	void Start () {
		activeNucleicAcid = DNA[0];
		activeNucleicAcidIndex = 0;
		timeUntilNext = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(timeUntilNext <= 0f){
			timeUntilNext = 3.0f;
			activeNucleicAcidIndex += 1;
			if(activeNucleicAcidIndex < DNA.Count){
				activeNucleicAcid = DNA[activeNucleicAcidIndex];
			}else{
			//end of game
			}
			return;
		}
		timeUntilNext -= Time.deltaTime;
		char newType = activeNucleicAcid.name;
		bool keypressed = false;
		if(Input.GetKeyDown("a")){
			newType = 'A';
			keypressed = true;
		}else if(Input.GetKeyDown("g")){
			newType = 'G';
			keypressed = true;
		}else if(Input.GetKeyDown("u")){
			newType = 'U';
			keypressed = true;
		}else if(Input.GetKeyDown("c")){
			newType = 'C';
			keypressed = true;
		}else if(Input.GetKeyDown("space")){
			activeNucleicAcidIndex += 1;
			timeUntilNext = 3.0f;
			if(activeNucleicAcidIndex < DNA.Count){
				activeNucleicAcid = DNA[activeNucleicAcidIndex];
			}else{
				//end of game
			}
		}
		if(activeNucleicAcid != null && keypressed){
			activeNucleicAcid.ChangeTypeTo(newType);
			//fast forward to next one
			activeNucleicAcidIndex += 1;
			timeUntilNext = 3.0f;
			if(activeNucleicAcidIndex < DNA.Count){
				activeNucleicAcid = DNA[activeNucleicAcidIndex];
			}else{
			//end of game
			}
		}
		
	}
}
