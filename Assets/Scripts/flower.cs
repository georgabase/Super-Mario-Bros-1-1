using UnityEngine;

public class flower : MonoBehaviour
{

	#region PUBLIC

	

	#endregion

	#region PRIVATE



	#endregion

	void Start ()
	{
	}

	void Update ()
	{
		
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player")) {
			GameObject score = GameObject.Find ("score");
			score.GetComponent<score> ().playerScore += 100;
			Destroy (gameObject);
		}
	}
}

