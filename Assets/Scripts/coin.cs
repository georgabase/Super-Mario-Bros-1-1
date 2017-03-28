using UnityEngine;
using System.Collections;
using System;

public class coin : MonoBehaviour
{

	#region PUBLIC

	public float lifetime = 2.0f;

	#endregion

	#region PRIVATE



	#endregion


	void OnTriggerEnter2D (Collider2D col)
	{
		
	}

	void Start ()
	{
		Destroy (gameObject, lifetime);
		GameObject score = GameObject.Find ("score");
		score.GetComponent<score> ().playerScore += 200;
	}

	void Update ()
	{
		
	}
}
