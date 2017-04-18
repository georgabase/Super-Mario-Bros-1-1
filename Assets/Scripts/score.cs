using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class score : MonoBehaviour
{

    #region PUBLIC

    [HideInInspector]
    public static int playerScore = 0;
    [HideInInspector]
    public static bool isBaby = true;
    [HideInInspector]
    public static bool isSuper = false;
    [HideInInspector]
    public static bool isFire = false;
    [HideInInspector]
    public static bool isGod = false;
    [HideInInspector]
    public static bool isStar = false;
    [HideInInspector]
    public static bool troopaIsShell = false;
    [HideInInspector]
    public static int lives = 3;
    [HideInInspector]
    public static int coins = 0;

    #endregion

    #region PRIVATE

    private Text scoreLabel;


    #endregion

    void Start()
    {
        scoreLabel = GetComponent<Text>();
    }

    void Update()
    {
        scoreLabel.text = "" + playerScore;

        if (lives == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
