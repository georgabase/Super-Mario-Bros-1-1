using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class playerMovement : MonoBehaviour
{

	#region PUBLIC

	public float moveSpeed = 2.5f;
	[Range(0, 1)]
	public float sliding = 0.9f;
	public float jumpForce = 180;

	[HideInInspector]
	public GameObject babyMario;
	[HideInInspector]
	public GameObject superMario;
	[HideInInspector]
	public GameObject fireMario;
	[HideInInspector]
	public float h;

	#endregion

	#region PRIVATE


	#endregion

	IEnumerator WaitStar()
	{
		GameObject _score = GameObject.Find("score");
		_score.GetComponent<score>().isStar = true; // makes this whole function unusable since invincible is no longer false
		Debug.Log(_score.GetComponent<score>().isStar + "star");
		yield return new WaitForSeconds(15);
		_score.GetComponent<score>().isStar = false; // makes this whole function unusable since invincible is no longer false
		Debug.Log(_score.GetComponent<score>().isGod + "star");
	}

	void Update()
	{
		

	}

	void FixedUpdate()
	{
		// Horizontal Movement
		h = Input.GetAxis("Horizontal");
		Vector2 v = GetComponent<Rigidbody2D>().velocity;
		if (h != 0)
		{
			// Move Left/Right
			GetComponent<Rigidbody2D>().velocity = new Vector2(h * moveSpeed, v.y);
			transform.localScale = new Vector2(Mathf.Sign(h), transform.localScale.y);
		}
		else
		{
			// Get slower (Super Mario style sliding motion)
			GetComponent<Rigidbody2D>().velocity = new Vector2(v.x * sliding, v.y);
		}
		GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(h));

		// Vertical Movement (Jumping)
		bool grounded = isGrounded();
		if (Input.GetKey(KeyCode.UpArrow) && grounded)
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
		GetComponent<Animator>().SetBool("Jump", !grounded);
	}

	bool isGrounded()
	{
		
		// Get Bounds and Cast Range (10% of height)
		Bounds bounds = GetComponent<Collider2D>().bounds;
		float range = bounds.size.y * 0.1f;

		// Calculate a position slightly below the collider
		Vector2 v = new Vector2(bounds.center.x,
			            bounds.min.y - range);

		// Linecast upwards
		RaycastHit2D hit = Physics2D.Linecast(v, bounds.center);

		// Was there something in-between, or did we hit ourself?
		return (hit.collider.gameObject != gameObject);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		GameObject _score = GameObject.Find("score");
		if (!_score.GetComponent<score>().isGod)
		{
			if (col.gameObject.CompareTag("redShroom"))
			{
				GameObject score = GameObject.Find("score");
				score.GetComponent<score>().playerScore += 1000;
			}

	
			if (col.gameObject.CompareTag("redShroom") && !_score.GetComponent<score>().isFire)
			{
				Debug.Log("shroom");
				_score.GetComponent<score>().isSuper = true;
				_score.GetComponent<score>().isBaby = false;
				Instantiate(superMario, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}

	}


	void OnTriggerEnter2D(Collider2D col)
	{		
		GameObject _score = GameObject.Find("score");
		
		//	if (!_score.GetComponent<score> ().isGod) {
		if ((col.gameObject.CompareTag("fireFlower")) && (!_score.GetComponent<score>().isSuper))
		{
			Debug.Log("flower");
			_score.GetComponent<score>().isSuper = false;
			_score.GetComponent<score>().isBaby = false;
			_score.GetComponent<score>().isFire = true;
			Instantiate(fireMario, transform.position, Quaternion.identity);
			Destroy(gameObject);
			//		}
		}
		if (col.gameObject.CompareTag("star"))
		{
			StartCoroutine(WaitStar());
		}
	}
}
