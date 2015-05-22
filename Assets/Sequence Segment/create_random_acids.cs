using UnityEngine;
using System.Collections;

public class create_random_acids : MonoBehaviour {
	public GameObject spawn_location;
	public GameObject spawn_object;

	// Use this for initialization
	void Start () {
		GameObject acid1 = (GameObject)Instantiate(spawn_object, spawn_location.transform.position, spawn_location.transform.rotation);
		SequenceDelay acid1_delayer = acid1.GetComponent<SequenceDelay>();
		acid1_delayer.delay = 5.0F;
		acid1_delayer.speed = 0.5F;

		GameObject acid2 = (GameObject)Instantiate(spawn_object, spawn_location.transform.position, spawn_location.transform.rotation);
		SequenceDelay acid2_delayer = acid2.GetComponent<SequenceDelay>();
		acid2_delayer.delay = 10.0F;
		acid2_delayer.speed = 1.0F;

		GameObject acid8 = (GameObject)Instantiate(spawn_object, spawn_location.transform.position, spawn_location.transform.rotation);
		SequenceDelay acid8_delayer = acid8.GetComponent<SequenceDelay>();
		acid8_delayer.delay = 20.0F;
		acid8_delayer.speed = 5.0F;
	}
}
