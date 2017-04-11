using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HelperAnimation : NetworkBehaviour {
    public GameObject turnCount;
    public GameObject helper;
    public GameObject countsShow;

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
                int remainAction = turnCount.GetComponent<CurrentPlayer>().RemainActionPoint;
                countsShow.GetComponent<Text>().text = "You have " + remainAction + " action left";
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
