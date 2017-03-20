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
        if (!isLocalPlayer)
            return;
        PlayerIDOB = GameObject.Find("TrunCounter").GetComponent<TurnCounter>().OwnId;
        Items = new GameObject[5];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
