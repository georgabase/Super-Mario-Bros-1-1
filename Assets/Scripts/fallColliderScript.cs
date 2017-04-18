using UnityEngine;
using System.Collections;

public class fallColliderScript : MonoBehaviour
{

    #region PUBLIC

	

    #endregion

    #region PRIVATE



    #endregion

    IEnumerator die(Collider2D col)
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

        // _player.GetComponent<playerMovement>().transform.position = new Vector2(-0.38f, -0.79f);
        Instantiate(_player.GetComponent<playerMovement>().babyMario, new Vector2(-0.38f, -0.79f), Quaternion.identity);

        Destroy(col.gameObject);


        GameObject.Find("Main Camera").transform.position = new Vector3(0, 0, -10);
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play(); 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(die(col));
        }
    }

   
}
