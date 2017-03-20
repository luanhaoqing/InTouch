using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class PlayerIDOnBoard : NetworkBehaviour {
    [SyncVar]
    public int PlayerIDOB;
    public GameObject[] Items;
    public int ItemNumber=0;
	// Use this for initialization
	void Start () {
        Items = new GameObject[5];
        if (!isLocalPlayer)
            return;
        PlayerIDOB = GameObject.Find("TrunCounter").GetComponent<TurnCounter>().OwnId;
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
