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

	void Awake(){
		geneticCodeTable = new Dictionary<string, AminoAcid>();
		CreateAminoAcid("UUU", "UUC", null, null, "Phe");
		CreateAminoAcid("UUA", "UUG", null, null, "Leu");
		CreateAminoAcid("UCU", "UCC", "UCA", "UCG", "Ser");
		CreateAminoAcid("UAU", "UAC", null, null, "Tyr");
		self = this;
	}

	public void CreateAminoAcid(string codon0, string codon1, string codon2, string codon3,
		 string name){
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
	
	}
	
	// Update is called once per frame
	void Update () {
		char newType = activeNucleicAcid.name;
		if(Input.GetKey("a")){
			newType = 'A';
		}else if(Input.GetKey("g")){
			newType = 'G';
		}else if(Input.GetKey("t")){
			newType = 'T';
		}else if(Input.GetKey("c")){
			newType = 'C';
		}
		if(activeNucleicAcid != null && newType != activeNucleicAcid.name){
			activeNucleicAcid.ChangeTypeTo(newType);
		}
	}
}
