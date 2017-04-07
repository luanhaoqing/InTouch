using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class HelperAnimation : NetworkBehaviour {
    public GameObject turnCount;
    public GameObject helper;
	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
            return;
        {
            turnCount = GameObject.Find("TrunCounter");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        {
            if(turnCount.GetComponent<TurnCounter>().OwnId==turnCount.GetComponent<CurrentPlayer>().CurrentPlayerID)
            {
                helper.GetComponent<Animator>().SetBool("fly",true);
            }
            else
            {
                helper.GetComponent<Animator>().SetBool("fly", false);
            }
        }
	}
}
