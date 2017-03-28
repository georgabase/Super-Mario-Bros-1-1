using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{

	#region PUBLIC

	// Movement Speed
	public float speed = 3;

	// Upwards push force
	public float upForce = 800;

	#endregion

	#region PRIVATE

	// Current movement Direction
	Vector2 dir = Vector2.right;

	#endregion

	void Start ()
	{
		

	}

	void FixedUpdate ()
	{
		GameObject _score = GameObject.Find ("score");


		// Set the Velocity
		GetComponent<Rigidbody2D> ().velocity = dir * speed;
		//GetComponent<Rigidbody2D> ().isKinematic = false; 
	}

	IEnumerator Wait ()
	{
		GameObject _score = GameObject.Find ("score");
		_score.GetComponent<score> ().isGod = true; // makes this whole function unusable since invincible is no longer false
		Debug.Log (_score.GetComponent<score> ().isGod);
		yield return new WaitForSeconds (5);
		_score.GetComponent<score> ().isGod = false; // makes this whole function unusable since invincible is no longer false
		Debug.Log (_score.GetComponent<score> ().isGod);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		GameObject _player = GameObject.FindGameObjectWithTag ("Player");
		GameObject _score = GameObject.Find ("score");

			
		if (col.gameObject.CompareTag ("destination")) {
			// Hit a destination? Then move into other direction
			transform.localScale = new Vector2 (-1 * transform.localScale.x,
				transform.localScale.y);

			// And mirror it
			dir = new Vector2 (-1 * dir.x, dir.y);
		}

		if (!_score.GetComponent<score> ().isGod) {
			

			if ((col.gameObject.CompareTag ("Player"))) {
				Debug.Log ("circlecollider");
				if (_score.GetComponent<score> ().isSuper) {
					Debug.Log ("super");
					Instantiate (_player.GetComponent<playerMovement> ().babyMario, _player.GetComponent<playerMovement> ().transform.position, Quaternion.identity);
					Destroy (_player.GetComponent<playerMovement> ().gameObject);

					StartCoroutine (Wait ());

					_score.GetComponent<score> ().isBaby = true;
					_score.GetComponent<score> ().isSuper = false;
					_score.GetComponent<score> ().isFire = false;

				} else if (_score.GetComponent<score> ().isFire) {
					Debug.Log ("fire");
					Instantiate (_player.GetComponent<playerMovement> ().babyMario, _player.GetComponent<playerMovement> ().transform.position, Quaternion.identity);
					Destroy (_player.GetComponent<playerMovement> ().gameObject);

					StartCoroutine (Wait ());

					_score.GetComponent<score> ().isBaby = true;
					_score.GetComponent<score> ().isSuper = false;
					_score.GetComponent<score> ().isFire = false;

				} else {
					SceneManager.LoadScene ("Main");
				}
			}
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		GameObject _player = GameObject.FindGameObjectWithTag ("Player");
		GameObject _score = GameObject.Find ("score");

		if (!_score.GetComponent<score> ().isGod) {
			
			// Collided with BabyMario?
			if (col.gameObject.CompareTag ("Player")) {
			
				//	if (gameObject.GetComponent<Collider2D> ().GetType () == typeof(BoxCollider2D)) {
				{// Play Animation 
					Debug.Log ("boxcollider");
					GetComponent<Animator> ().SetTrigger ("Dead");

					gameObject.GetComponent<CircleCollider2D> ().enabled = false;
					gameObject.GetComponent<BoxCollider2D> ().enabled = false;
					GetComponent<Rigidbody2D> ().isKinematic = false; //Let Unity Physics affect the item.
					GetComponent<Rigidbody2D> ().gravityScale = 6;
					// Push BabyMario upwardss
					col.gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * upForce);
					GameObject score = GameObject.Find ("score");
					score.GetComponent<score> ().playerScore += 100;
					// Die in a few seconds
					Invoke ("Die", 5);
				}
			}
		} 
		



	}

	void Die ()
	{
		Destroy (gameObject);
	}
}