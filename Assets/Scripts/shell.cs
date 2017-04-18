using UnityEngine;
using System.Collections;

public class shell : MonoBehaviour
{

    #region PUBLIC



    #endregion

    #region PRIVATE

    Vector2 dir = Vector2.right;
    private float speed;

    #endregion


    IEnumerator shellMove()
    {
        speed = 1;
        GetComponent<Rigidbody2D>().velocity = dir * speed;
        yield return new WaitForSeconds(5f);
        speed = 0;
    }

    void Start()
    {
		
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = dir * speed;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(shellMove());
        }

        if (col.gameObject.CompareTag("destination"))
        {
            // Hit a destination? Then move into other direction
            transform.localScale = new Vector2(-1 * transform.localScale.x,
                transform.localScale.y);

            // And mirror it
            dir = new Vector2(-1 * dir.x, dir.y);
        }
    }

   
}
