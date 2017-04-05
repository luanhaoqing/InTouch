using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class HelperAnimation : NetworkBehaviour {
    public GameObject turnCount;
	// Use this for initialization
	void Start () {
	if(!isLocalPlayer)
        {
            turnCount = GameObject.Find("TrunCounter");
        }
	}
	
	// Update is called once per frame
	void Update () {
	if(!isLocalPlayer)
        {
            if(turnCount.GetComponent<TurnCounter>().OwnId==turnCount.GetComponent<CurrentPlayer>().CurrentPlayerID)
            {
                this.GetComponent<Animator>().SetBool("fly",true);
            }
            else
            {
                this.GetComponent<Animator>().SetBool("fly", false);
            }
        }
	}
}
