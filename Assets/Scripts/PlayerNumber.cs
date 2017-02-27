using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class PlayerNumber : NetworkBehaviour {
    public int PlayerCount = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnPlayerConnected(NetworkPlayer player)
    {
        PlayerCount++;
    }

}
