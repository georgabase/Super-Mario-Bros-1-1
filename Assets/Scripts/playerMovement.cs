using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class playerMovement : MonoBehaviour
{

    #region PUBLIC

    public GameObject fireballPrefab;
    public Transform fireballPoint;

    public Vector2 velocity;
    public float moveSpeed = 2.5f;
    [Range(0, 1)]
    public float sliding = 0.9f;
    public float jumpForce = 180;


    public AudioSource jump;
    public AudioSource star;
    public AudioSource deathSound;


    [HideInInspector]
    public GameObject babyMario;
    [HideInInspector]
    public GameObject superMario;
    [HideInInspector]
    public GameObject fireMario;
    [HideInInspector]
    public float h;
    [HideInInspector]
    public bool dead;

    #endregion

    #region PRIVATE

    private float shootCooldown = 0.0f;
    private float shootingRate = 0.2f;

    #endregion

    #region IEnumirators

    IEnumerator WaitStar()
    {
        score.isStar = true; // makes this whole function unusable since invincible is no longer false
        print(score.isStar + "star");
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        star.Play();
        yield return new WaitForSeconds(12);
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3);
        star.Stop();
        score.isStar = false; // makes this whole function unusable since invincible is no longer false
        print(score.isGod + "star");
    }

    public static IEnumerator Death()
    {
        score.lives--;
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        _player.GetComponent<Animator>().SetBool("Dead", true);
        _player.GetComponent<playerMovement>().dead = true;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        _player.GetComponent<CapsuleCollider2D>().enabled = false;
        _player.GetComponent<playerMovement>().deathSound.Play();
        _player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150);
        yield return new WaitForSeconds(2f);
        _player.GetComponent<Animator>().SetBool("Dead", false);
        _player.GetComponent<CapsuleCollider2D>().enabled = true;
        _player.GetComponent<playerMovement>().dead = false;
        _player.GetComponent<playerMovement>().transform.position = new Vector2(-0.38f, -0.79f);
        GameObject.Find("Main Camera").transform.position = new Vector3(0, 0, -10);
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();


    }

    #endregion



    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }    
    }


    public void Attack()
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;
            GameObject projectile = (GameObject)Instantiate(fireballPrefab, fireballPoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * gameObject.transform.localScale.x, velocity.y);
        }
    }


    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }

  

    void FixedUpdate()
    {
        if (!dead)
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
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
                grounded = false;
                jump.Play();
            }
            if (Input.GetKey(KeyCode.Space) && CanAttack && score.isFire)
            {
                GetComponent<Animator>().SetTrigger("Shoot");
                Attack();
            }
            GetComponent<Animator>().SetBool("Jump", !grounded);
            // GetComponent<Animator>().SetBool("Shoot", !CanAttack);
        }
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
        if (!score.isGod)
        {
            if (col.gameObject.CompareTag("redShroom"))
            {
                score.playerScore += 1000;
            }

	
            if (col.gameObject.CompareTag("redShroom") && !score.isFire)
            {
                Debug.Log("shroom");
                //powerUp.Play();
                score.isFire = false;
                score.isBaby = false;
                score.isSuper = true;
                Instantiate(superMario, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if (col.gameObject.CompareTag("greenShroom"))
            {
                score.lives++;
                score.playerScore += 1000;
            }
        }

    }


    void OnTriggerEnter2D(Collider2D col)
    {		
		
        //	if (!_score.GetComponent<score> ().isGod) {
        if (col.gameObject.CompareTag("fireFlower"))
        {
            Debug.Log("flower");
            score.isSuper = false;
            score.isBaby = false;
            score.isFire = true;
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
