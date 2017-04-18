using UnityEngine;
using System.Collections;
using System;

public class shroom : MonoBehaviour
{

    #region PUBLIC

    public AudioSource powerUp;

    #endregion

    #region PRIVATE

    private Vector2 currentVelocity = new Vector2(1, 0);

    #endregion


    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = currentVelocity;

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            powerUp.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 3);
        }
    }
}
