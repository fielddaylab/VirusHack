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

	public float timeUntilNext;
	public List<AminoAcid> aminoAcidChain;

	public Dictionary<string,Protein> targetProteins; 
	public Protein proteinPrefab;



	void Awake(){
		geneticCodeTable = new Dictionary<string, AminoAcid>();
		CreateAminoAcid("UUU", "UUC", null, null, "Phe");
		CreateAminoAcid("UUA", "UUG", null, null, "Leu");
		CreateAminoAcid("UCU", "UCC", "UCA", "UCG", "Ser");
		CreateAminoAcid("UAU", "UAC", null, null, "Tyr");
		CreateAminoAcid("UAA", "UAG", "UGA", null, "STOP");
		//create protein
		targetProteins = new Dictionary<string,Protein>();
		CreateProtein("Poopy", "Phe Leu STOP");
		CreateProtein("Test", "Phe STOP");
		self = this;
		DNA = new List<NucleicAcid>();
		aminoAcidChain = new List<AminoAcid>();
		

		//Construct dna sequence;
		for(int i=0; i<sequence.Length; i++){
			char c = sequence[i];
			NucleicAcid na = CreateNucleicAcid(c);
			if(na != null){
				DNA.Add(na);
			}
		}

	}
	public void CreateProtein(string name, string aminoAcidSeq){
		Protein p = Instantiate(proteinPrefab) as Protein;
		p.name = name;
		p.aminoAcidsStr = aminoAcidSeq;
		targetProteins.Add(p.aminoAcidsStr, p);
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
			geneticCodeTable.Add(codon0,aa);
		}
		if(codon1 != null){
			aa.codons.Add(codon1);
			geneticCodeTable.Add(codon1,aa);
		}
		if(codon2 != null){
			aa.codons.Add(codon2);
			geneticCodeTable.Add(codon2,aa);
		}
		if(codon3 != null){
			aa.codons.Add(codon3);
			geneticCodeTable.Add(codon3,aa);
		}
	}

	// Use this for initialization
	void Start () {
		activeNucleicAcid = DNA[0];
		activeNucleicAcidIndex = 0;
		timeUntilNext = 3.0f;
	}
	public void ProgressToNextNecleicAcid(){
		//detect amino acid
		if((activeNucleicAcidIndex+1) % 3 == 0 && activeNucleicAcidIndex > 0){
			
			NucleicAcid na0 = DNA[activeNucleicAcidIndex-2];
			NucleicAcid na1 = DNA[activeNucleicAcidIndex-1];
			//Look up in encoding table
			string codon = na0.name.ToString() 
				+ na1.name.ToString() 
				+ activeNucleicAcid.name.ToString();
			Debug.Log(codon);
			if(geneticCodeTable.ContainsKey(codon)){
				AminoAcid aa = geneticCodeTable[codon];
				aminoAcidChain.Add(aa);
				Debug.Log(aa.name);
				if(aa.name == "STOP"){
					//check protein
					string aaStr = "";
					for(int i=0; i < aminoAcidChain.Count; i++){
						AminoAcid aminoAcid = aminoAcidChain[i];
						if(i > 0){
							aaStr = aaStr + " " +aminoAcid.name;
						}else{
							aaStr = aminoAcid.name;
						}
						
					}//end for
					Debug.Log(aaStr);
					if(targetProteins.ContainsKey(aaStr)){
						Debug.Log("Protein " + targetProteins[aaStr].name);
					}
				}
			}
			
		}
		
		activeNucleicAcidIndex += 1;

		if(activeNucleicAcidIndex < DNA.Count){
			activeNucleicAcid = DNA[activeNucleicAcidIndex];
		}else{
		//end of game
		}
	}
	// Update is called once per frame
	void Update () {
		if(timeUntilNext <= 0f){
			timeUntilNext = 3.0f;
			ProgressToNextNecleicAcid();
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
			ProgressToNextNecleicAcid();
			return;
		}
		if(activeNucleicAcid != null && keypressed){
			activeNucleicAcid.ChangeTypeTo(newType);
			//fast forward to next one
			timeUntilNext = 3.0f;
			ProgressToNextNecleicAcid();
			return;
		}
		
	}
}
