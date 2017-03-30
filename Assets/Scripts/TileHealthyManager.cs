﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
public class TileHealthyManager : MonoBehaviour {

    public int[] Iden;
    public int health;
    public GameObject _text;
    public GameObject[] tiles;
    public bool HasExploded;
    private GameObject player;
    private GameObject tmp;
    private bool HasPlayer;
    public GameObject End;
    // Use this for initialization
    void Start () {
        this.GetComponent<MeshRenderer>().enabled = false;
        health = 5;
        _text.SetActive(false);
        End = GameObject.FindGameObjectWithTag("END");
        
    }
	
	// Update is called once per frame
	void Update () {
	if(health==0)
        {
            // this.gameObject.SetActive(false);
            Destroy(tmp);
            tmp = null;
            HasExploded = false;
            if (HasPlayer)
                End.GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.SetActive(false);
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
            HasPlayer = true;
            player = other.gameObject;
            GameObject.Find("TrunCounter").GetComponent<CurrentPlayer>().RemainActionPoint--;
            health -= 1;
            if (!HasExploded)
            {
                tmp = Instantiate(tiles[this.GetComponentInParent<GenerateMap>().RanTileNum]);
        
                tmp.transform.position = this.transform.position+new Vector3(0,0.002f,0);
                tmp.transform.parent = this.transform;
                tmp.transform.localScale = new Vector3(5, 8, 5);
                HasExploded = true;
                GameObject[] flames = this.GetComponent<UpdateHP>().flame;
                for(int i=0;i<4;i++)
                {
                    flames[i].SetActive(true);
                }
                //     _text.SetActive(true);
                Invoke("getRanTile",1.0f);
               
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HasPlayer = false;
        }
    }
    private void getRanTile()
    {
        this.GetComponentInParent<GenerateMap>().getRandomTile(player);
    }
}
