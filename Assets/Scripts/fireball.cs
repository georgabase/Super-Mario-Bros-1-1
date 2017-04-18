using UnityEngine;

public class fireball : MonoBehaviour
{

    #region PUBLIC

    // [HideInInspector]
    public Vector2 velocity;

    #endregion

    #region PRIVATE

    private Rigidbody2D rb;

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = rb.velocity;
        Destroy(gameObject, 3);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        rb.velocity = new Vector2(velocity.x, -velocity.y);

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("stairs"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (rb.velocity.y < velocity.y)
        {
            rb.velocity = velocity;
        }
    }
}
