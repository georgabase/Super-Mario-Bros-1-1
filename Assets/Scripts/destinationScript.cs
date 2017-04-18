using UnityEngine;

public class destinationScript : MonoBehaviour
{

    #region PUBLIC

	

    #endregion

    #region PRIVATE



    #endregion

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GetComponent<CircleCollider2D>().radius = 0f;
            //   GetComponent<CircleCollider2D>().enabled = false;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        GetComponent<CircleCollider2D>().radius = 0.12f;

        // GetComponent<CircleCollider2D>().enabled = true;
    }
}
