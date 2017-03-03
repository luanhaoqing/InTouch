﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
public class TileHealthyManager : MonoBehaviour {

    public int[] Iden;
    public int health;
    public GameObject _text;
    public GameObject[] tiles;
    private bool HasExploded;
    // Use this for initialization
    void Start () {
        this.GetComponent<MeshRenderer>().enabled = false;
        health = 5;
    }
	
	// Update is called once per frame
	void Update () {
	if(health==0)
        {
            this.gameObject.SetActive(false);
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
            
            health -= 1;
            this.GetComponentInChildren<Text>().text = health.ToString();
            //    this.GetComponent<MeshRenderer>().enabled = true;
            if (!HasExploded)
            {
                GameObject tmp = Instantiate(tiles[this.GetComponentInParent<GenerateMap>().RanTileNum]);
                this.GetComponentInParent<GenerateMap>().getRandomTile(other.gameObject);
                tmp.transform.position = this.transform.position;
                tmp.transform.parent = this.transform;
                HasExploded = true;
            }
        }
    }
    private void CheckHeath()
    {

    }
}
