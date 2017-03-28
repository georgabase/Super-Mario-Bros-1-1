using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class score : MonoBehaviour
{

	#region PUBLIC

	[HideInInspector]
	public int playerScore = 0;
	public static score instace;

	public bool isBaby = true;
	public bool isSuper = false;
	public bool isFire = false;
	public bool isGod = false;

	#endregion

	#region PRIVATE

	private Text scoreLabel;


	#endregion

	void Start ()
	{
		scoreLabel = GetComponent<Text> ();
	}

	void Update ()
	{
		scoreLabel.text = "" + playerScore;
	}
}
