using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grav_Const : MonoBehaviour {


    public float queda;


	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1, 0) / 8;
    }
	
	// Update is called once per frame
	void Update () {


 

   
	}
}
