using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour
{

	#region PUBLIC

	public Transform target;
	public Transform rightCamBoundary;
	public Transform levelEnd;

	#endregion

	#region PRIVATE

	Vector3 velocity = Vector3.zero;
	Vector3 destination;
	float nextTimeToSearch = 0;

	#endregion

	void Start ()
	{
		destination = Vector3.ClampMagnitude (levelEnd.position, 31.5f);
		destination = new Vector3 (destination.x, destination.y, -10f);
	}

	void Update ()
	{
		if (target == null) {
			FindPlayer ();
			return;
		}

		if (Vector3.Distance (target.position, rightCamBoundary.position) < 2)
			transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, 4.5f, 2.4f);

	}

	void FindPlayer ()
	{
		if (nextTimeToSearch <= Time.time) {
			GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
			if (searchResult != null)
				target = searchResult.transform;
			nextTimeToSearch = Time.time + 0.5f;
		}

	}
}
