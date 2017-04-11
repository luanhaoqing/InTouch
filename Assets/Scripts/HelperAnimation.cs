using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class HelperAnimation : NetworkBehaviour {
    public GameObject turnCount;
    public GameObject helper;

    bool prompted;
    bool first_time = true;

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

                if (!prompted)
                {
                    HelperPrompt();
                }
            }
            else
            {
                helper.GetComponent<Animator>().SetBool("fly", false);
                prompted = false;
            }
        }
	}

    void HelperPrompt()
    {
        if (!first_time) {
            AudioCenter.PlayHelperPrompt();
            prompted = true;
        }

        else
        {
            first_time = false;
            prompted = true;
        }


    }
}
