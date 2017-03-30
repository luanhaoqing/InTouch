﻿using UnityEngine;
using System.Collections;

public class EndState : MonoBehaviour {
    private GameObject P1;
    private GameObject P2;
    public int Pass=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Pass == 2)
            Debug.Log("Win");
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnTheBoard"))
        {
           if( other.GetComponentInParent<Inventory>().HasKey)
            {
                Pass++;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerOnTheBoard"))
        {
            if (other.GetComponentInParent<Inventory>().HasKey)
            {
                Pass--;
            }
        }
    }
}
