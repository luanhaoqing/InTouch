using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class CurrentPlayer : NetworkBehaviour {
    [SyncVar]
    public int CurrentPlayerID;
    public float counter = 0;
    private bool reverse;
	// Use this for initialization
	void Start () {
        CurrentPlayerID = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isServer)
            return;
        if (!reverse)
        {
            counter += Time.deltaTime;
            if (counter >= 10f)
            {
                CurrentPlayerID = 1;
                reverse = true;

            }
        }
        else
        {
            counter -= Time.deltaTime;
            if (counter <= 0f)
            {
                CurrentPlayerID = 0;
                reverse = false;
            }
        }
    }
}
