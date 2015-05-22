using UnityEngine;
using System.Collections;

public class SequenceDelay : MonoBehaviour {

    //public Animation anim;
	public Animator animator;
	public float delay = 1.0F;
	public float speed = 1.0F;
	public float start_time;
	public bool started = false;

    void Start()
	{
		animator = GetComponent<Animator>();
		animator.speed = 0.0F;

		start_time = Time.time + delay;
	}


	void Update ()
	{
		if(!started && Time.time > start_time)
		{
			animator.speed = speed;
			started = true;
		}
	}
}
