using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sobe_mapa : MonoBehaviour
{
    public static sobe_mapa instance;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.gameStart)
        {
            contador = 0;

        }
        if (!GameControl.instance.gameOver && !GameControl.instance.gameStart)
        {
            if (Time.time - contador >= 1 / 10)
            {
                Vector3 tmp = transform.position;
                tmp.y += 0.004f + contador * 0.0002f;
                transform.position = new Vector3(tmp.x, tmp.y, tmp.z);

                
            }
            contador += Time.deltaTime;
        }
    }

    public Vector3 posicao()
    {
        return transform.position;
    }
}