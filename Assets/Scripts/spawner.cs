using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawner : MonoBehaviour
{

    public Transform[] criaPecas;
    public float queda;
    public int aleat;

    public static spawner instance;

    public float contador = 0;
    public void setcontador()
    {
        contador = 0;

    }
    // Use this for initialization
    void Start()
    {
        contador = 0;
        if (instance == null)
        {
            instance = this;
        }
        //Instantiate(criaPecas[Random.Range(0, 4)], transform.position, Quaternion.identity);
    }
    
    void Update()
    {
        if (GameControl.instance.gameStart)
        {
            contador = 0;
        }
        if (Time.time - contador >= 1 / 10)
        {
            Vector3 tmp = transform.position;
            tmp.y += 0.007f + contador * 0.003f;
            transform.position = new Vector3(tmp.x, tmp.y, tmp.z);
        }
        contador +=  Time.deltaTime;
        if (!GameControl.instance.gameOver && !GameControl.instance.gameStart)
        {
            if (Time.time - queda >= 1.5)
            {
                int randPeca = Random.Range(0, 18);

                int randPos = Random.Range(1, 8);

                transform.position = new Vector3(randPos, 28, 0);

                Instantiate(criaPecas[randPeca], transform.position, Quaternion.identity);
                
                queda = Time.time;
            }

        }
    }
}