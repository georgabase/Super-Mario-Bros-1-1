using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class troopa : MonoBehaviour
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
	Vector2 zero = Vector2.zero;

	#endregion

	void FixedUpdate()
	{
		// Set the Velocity
		GameObject _score = GameObject.Find("score");
		if (!_score.GetComponent<score>().troopaIsShell)
		{
			GetComponent<Rigidbody2D>().velocity = dir * speed;
		}
	}

	IEnumerator troopaShellMove()
	{
		Debug.Log("troopa moved");
		GameObject _player = GameObject.FindGameObjectWithTag("Player");
		//	GetComponent<Rigidbody2D>().velocity = _player.GetComponent<playerMovement>().h * speed;
		Vector2 v = GetComponent<Rigidbody2D>().velocity;
		dir = new Vector2(_player.GetComponent<playerMovement>().h, dir.y);
		GetComponent<Rigidbody2D>().velocity = dir * speed;
		GetComponent<CircleCollider2D>().isTrigger = true;
		yield return new WaitForSeconds(3);
		GetComponent<Rigidbody2D>().velocity = zero;
		GetComponent<CircleCollider2D>().isTrigger = false;
		Debug.Log(GetComponent<Rigidbody2D>().velocity);
	}

	IEnumerator Wait()
	{
		GameObject _score = GameObject.Find("score");
		_score.GetComponent<score>().isGod = true; // makes this whole function unusable since invincible is no longer false
		Debug.Log(_score.GetComponent<score>().isGod);
		yield return new WaitForSeconds(1);
		_score.GetComponent<score>().isGod = false; // makes this whole function unusable since invincible is no longer false
		Debug.Log(_score.GetComponent<score>().isGod);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject _player = GameObject.FindGameObjectWithTag("Player");
		GameObject _score = GameObject.Find("score");


		if (col.gameObject.CompareTag("destination"))
		{
			// Hit a destination? Then move into other direction
			transform.localScale = new Vector2(-1 * transform.localScale.x,
				transform.localScale.y);

			// And mirror it
			dir = new Vector2(-1 * dir.x, dir.y);
		}


		if (!_score.GetComponent<score>().troopaIsShell)
		{
			if (!_score.GetComponent<score>().isStar)
			{
				if (!_score.GetComponent<score>().isGod)
				{


					if ((col.gameObject.CompareTag("Player")))
					{
						Debug.Log("circlecollider");
						if (_score.GetComponent<score>().isSuper)
						{
							Debug.Log("super");
							Instantiate(_player.GetComponent<playerMovement>().babyMario, _player.GetComponent<playerMovement>().transform.position, Quaternion.identity);
							Destroy(_player.GetComponent<playerMovement>().gameObject);

							StartCoroutine(Wait());

							_score.GetComponent<score>().isBaby = true;
							_score.GetComponent<score>().isSuper = false;
							_score.GetComponent<score>().isFire = false;

						}
						else if (_score.GetComponent<score>().isFire)
						{
							Debug.Log("fire");
							Instantiate(_player.GetComponent<playerMovement>().babyMario, _player.GetComponent<playerMovement>().transform.position, Quaternion.identity);
							Destroy(_player.GetComponent<playerMovement>().gameObject);

							StartCoroutine(Wait());

							_score.GetComponent<score>().isBaby = true;
							_score.GetComponent<score>().isSuper = false;
							_score.GetComponent<score>().isFire = false;

						}
						else
						{
							SceneManager.LoadScene("Main");
						}
					}
				}
				else if (col.gameObject.CompareTag("Player"))
				{
					GetComponent<Animator>().SetTrigger("Dead");
					_score.GetComponent<score>().troopaIsShell = true;
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		GameObject _player = GameObject.FindGameObjectWithTag("Player");
		GameObject _score = GameObject.Find("score");



		// Collided with Mario?
		if (col.gameObject.CompareTag("Player"))
		{
			if (_score.GetComponent<score>().troopaIsShell)
			{
				StartCoroutine(troopaShellMove());
			}
			else if (_score.GetComponent<score>().isStar)
			{
				Debug.Log("star");
				GetComponent<Animator>().SetTrigger("Dead");
				_score.GetComponent<score>().playerScore += 100;

				// Become shell
				_score.GetComponent<score>().troopaIsShell = true;	
				GetComponent<Rigidbody2D>().velocity = zero;
				GetComponent<CircleCollider2D>().isTrigger = false;
			}
			else if (!_score.GetComponent<score>().isGod)
			{
				// Play Animation 
				Debug.Log("boxcollider");
				GetComponent<Animator>().SetTrigger("Dead");
				_score.GetComponent<score>().playerScore += 100;

				// become shell
				_score.GetComponent<score>().troopaIsShell = true;	
				GetComponent<Rigidbody2D>().velocity = zero;
				GetComponent<CircleCollider2D>().isTrigger = false;
			}
		} 




	}
}

