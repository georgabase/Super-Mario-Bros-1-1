using UnityEngine;

public class flower : MonoBehaviour
{

    #region PUBLIC

    public AudioSource powerUp;


    #endregion

    #region PRIVATE



    #endregion

    void Start()
    {
    }

    void Update()
    {
		
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            score.playerScore += 100;
            powerUp.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(this.gameObject, 3);
        }
    }
}

