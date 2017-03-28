using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

	#region PUBLIC



	#endregion

	#region PRIVATE

	private Text timerLabel;
	private float time = 150;

	#endregion

	void Start ()
	{
		timerLabel = GetComponent<Text> ();
	}

	void Update ()
	{
		time -= Time.deltaTime;
		timerLabel.text = " " + Mathf.Round (time);
	}

	IEnumerator StartCountdown ()
	{
		while (time > 0) {
			Debug.Log ("Countdown: " + time);
			time--;
			yield return new WaitForSeconds (1.0f);
			//update the label value
			timerLabel.text = string.Format ("{0}", time);
		}
	}
		
}
