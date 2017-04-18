using UnityEngine;
using UnityEngine.UI;

public class livesScript : MonoBehaviour
{
    #region PRIVATE

    private Text text;


    #endregion

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "" + score.coins;
    }
}
