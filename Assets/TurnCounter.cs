using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class TurnCounter : MonoBehaviour {

    public int count = 0;
	// Use this for initialization
	void Start () {
       
      //  OwnID = netId;
    }
	
	// Update is called once per frame
	void Update () {

	}
    private void OnPlayerConnected(NetworkPlayer player)
    {
        count++;
    }
}
