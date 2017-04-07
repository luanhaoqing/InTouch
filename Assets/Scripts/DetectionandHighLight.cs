﻿using UnityEngine;
using System.Collections;

public class DetectionandHighLight : MonoBehaviour {
    bool CouldMove=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tile")) //&& this.transform.position != transform.parent.position) // add this condition to make sure the cursor does not bounce back and forth.
        {
            CouldMove = true;
            other.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            CouldMove = false;
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public bool IfCouldMove()
    {
        return CouldMove;
    }
}
