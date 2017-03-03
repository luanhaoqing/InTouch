using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class TileHealthyManager : MonoBehaviour {

    public int[] Iden;
    public int health;
	// Use this for initialization
	void Start () {
        health = 5;
    }
	
	// Update is called once per frame
	void Update () {
	if(health==0)
        {
            this.gameObject.SetActive(false);
        }
	}
}
