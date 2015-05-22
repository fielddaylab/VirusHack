using UnityEngine;
using System.Collections;

public class SequenceDelay : MonoBehaviour {

    public Animation anim;
	public float delay = 1.0F;
	public float speed = 0.1F;
	public float start_time;
	public bool started = false;

    void Start()
	{
        anim = GetComponent<Animation>();
        foreach (AnimationState state in anim)
		{
            state.speed = speed;
        }

		start_time = Time.time + delay;
	}


	void Update ()
	{
		if(!started && Time.time > start_time)
		{
			anim.Play();
			started = true;
		}
	}
}
