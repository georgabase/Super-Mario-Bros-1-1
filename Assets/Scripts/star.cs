using UnityEngine;

public class star : MonoBehaviour
{

    #region PUBLIC



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
            Destroy(gameObject);
        }
    }
}

