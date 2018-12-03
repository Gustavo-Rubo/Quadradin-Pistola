using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    public static GameControl instance;
    
    public GameObject gameOverObject;
    public GameObject gameStartObject;
    public GameObject gameRunObject;

    public bool gameOver = false;
    public bool gameStart = true;
    public bool gameRun = false;

    public int score = 0;

    public Text textScore;
    public Text textBest;
    public GameObject medal0;
    public GameObject medal1;
    public GameObject medal2;
    public GameObject medal3;

    public Text runTextScore;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool up = false, dir = false, esq = false;
        Touch myTouch = Input.GetTouch(0);

        Touch[] myTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (myTouches[i].position.x < Screen.width / 3) esq = true;
            else if (myTouches[i].position.x < Screen.width * 2 / 3) up = true;
            else dir = true;
        }

        if (gameStart == true && (Input.GetKeyDown(KeyCode.UpArrow) || up))
        {
            gameStartObject.SetActive(false);
            gameStart = false;
            gameRun = true;
            gameRunObject.SetActive(true);
        }

        if (gameOver == true && (Input.GetKeyDown(KeyCode.UpArrow) || up))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameOver = false;
            gameRun = true;
            gameRunObject.SetActive(true);
            GameObject.FindObjectOfType<spawner>().setcontador();
            sobe_mapa.instance.setcontador();
           

        }

        score = (int)(5*(sobe_mapa.instance.posicao().y - 11));
        runTextScore.text = score.ToString();
    }

    public void characterDied()
    {
        gameOverObject.SetActive(true);
        textScore.text = score.ToString();
        if (score < 15) medal3.SetActive(true);
        else if (score < 30) medal2.SetActive(true);
        else if (score < 45) medal1.SetActive(true);
        else medal0.SetActive(true);
        gameOver = true;
        gameRun = false;
        gameRunObject.SetActive(false);
    }
}
