﻿using UnityEngine;
using System.Collections;

public class create_random_acids : MonoBehaviour {
	public GameObject spawn_location;
	public GameObject spawn_object;

	void Start () {

		GameObject acid;
		SequenceDelay delayer;

		for(int i = 1; i <= 20; i++)
		{
			acid = (GameObject)Instantiate(spawn_object, spawn_location.transform.position, spawn_location.transform.rotation);
			delayer = acid.GetComponentInChildren<SequenceDelay>();
			delayer.delay = 1.5F * i;
			delayer.speed = 1.0F;
		}
	}
}
