using UnityEngine;
using System.Collections;

public class blockOfManyCoins : MonoBehaviour
{

    #region PUBLIC

    public AnimationCurve curve;
    public GameObject spawnPrefab;
    public GameObject nextPrefab;

    #endregion

    #region PRIVATE

    private int hitCount = 0;

    #endregion

    IEnumerator sample()
    {
        // start position
        Vector2 pos = transform.position;

        // go through curve time
        for (float t = 0; t < curve.keys[curve.length - 1].time; t += Time.deltaTime)
        {
            // move a bit
            transform.position = new Vector2(pos.x, pos.y + curve.Evaluate(t));

            // come back next Update
            yield return null;
        }



    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // collision below?
        if ((coll.contacts[0].point.y < transform.position.y))
        {
            StartCoroutine("sample");
            // spawn an object if any

            if ((spawnPrefab) && (hitCount < 10))
            {
                Instantiate(spawnPrefab, transform.position + Vector3.up / 6, Quaternion.identity);
                hitCount++;
                Debug.Log("hit++");
            }
            // spawn next one if any, destroy box
            if ((nextPrefab) && (hitCount == 10))
            {
                Instantiate(nextPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
