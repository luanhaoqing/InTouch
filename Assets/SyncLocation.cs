using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SyncLocation : NetworkBehaviour {

    public GameObject anchor;

	// Use this for initialization
	void Start () {
        anchor = GameObject.Find("PlayerLeftHand");
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        } else
        {
            this.transform.position = anchor.transform.position;
        }
    }
}
