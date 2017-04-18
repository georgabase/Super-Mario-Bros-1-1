using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class troopa : MonoBehaviour
{

    #region PUBLIC

    // Movement Speed
    public float speed = 3;
    public GameObject shellPrefab;

    public AudioSource deathSound;
    public AudioSource deathByFireballSound;


    #endregion

    #region PRIVATE

    // Current movement Direction
    Vector2 dir = Vector2.right;

    #endregion

    void Start()
    {


    }

    void FixedUpdate()
    {


        // Set the Velocity
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }

    IEnumerator Wait()
    {
        score.isGod = true; // makes this whole function unusable since invincible is no longer false
        Debug.Log(score.isGod);
        yield return new WaitForSeconds(1);
        score.isGod = false; // makes this whole function unusable since invincible is no longer false
        Debug.Log(score.isGod);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");


        if (col.gameObject.CompareTag("destination"))
        {
            // Hit a destination? Then move into other direction
            transform.localScale = new Vector2(-1 * transform.localScale.x,
                transform.localScale.y);

            // And mirror it
            dir = new Vector2(-1 * dir.x, dir.y);
        }
        if (!score.isStar)
        {
            if (!score.isGod)
            {


                if ((col.gameObject.CompareTag("Player")))
                {
                    Debug.Log("circlecollider");
                    if (score.isSuper || score.isFire)
                    {
                        Debug.Log("super");
                        Instantiate(_player.GetComponent<playerMovement>().babyMario, _player.GetComponent<playerMovement>().transform.position, Quaternion.identity);
                        Destroy(_player.GetComponent<playerMovement>().gameObject);

                        StartCoroutine(Wait());

                        score.isBaby = true;
                        score.isSuper = false;
                        score.isFire = false;

                    }
                    /* else if (score.isFire)
                    {
                        Debug.Log("fire");
                        Instantiate(_player.GetComponent<playerMovement>().babyMario, _player.GetComponent<playerMovement>().transform.position, Quaternion.identity);
                        Destroy(_player.GetComponent<playerMovement>().gameObject);

                        StartCoroutine(Wait());

                        score.isBaby = true;
                        score.isSuper = false;
                        score.isFire = false;

                    }*/
                    else
                    {
                        if (score.isBaby)
                        {
                            StartCoroutine(playerMovement.Death());

                        }


                    }
                }
            }
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            deathSound.Play();
            Instantiate(shellPrefab, new Vector2(transform.position.x, -0.8f), Quaternion.identity);
            Destroy(gameObject);
            score.playerScore += 100;

        }

        if (col.gameObject.CompareTag("fireball"))
        {
            deathByFireballSound.Play();
            Instantiate(shellPrefab, new Vector2(transform.position.x, -0.8f), Quaternion.identity);
            Destroy(gameObject);
            score.playerScore += 100;

        }
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("fireball"))
        {
            deathByFireballSound.Play();
            Instantiate(shellPrefab, new Vector2(transform.position.x, -0.8f), Quaternion.identity);
            Destroy(gameObject);
            score.playerScore += 100;

        }
        // Collided with BabyMario?
        if (col.gameObject.CompareTag("Player"))
        {
            if (score.isStar)
            {
                
                deathSound.Play();
                Instantiate(shellPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                score.playerScore += 100;

            }
            else if (!score.isGod)
            {
                deathSound.Play();
                Instantiate(shellPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                score.playerScore += 100;
            }
        } 




    }
}

