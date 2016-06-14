using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		if (speed == 0) 
		{
			speed = 10f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = Vector3.forward;
		transform.Translate (dir * Time.deltaTime*speed);
	}
}
