using UnityEngine;
using System.Collections;

public class FirstTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
            GameObject.Find("TrunCounter").GetComponent<CurrentPlayer>().RemainActionPoint--;
        }
    }
}
