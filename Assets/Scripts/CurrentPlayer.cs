using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CurrentPlayer : NetworkBehaviour {
    [SyncVar]
    public int CurrentPlayerID;
    [SyncVar]
    public float counter = 0;
    private bool reverse;
    public GameObject TEXT;
    [SyncVar]
    private int test;
    [SyncVar]
    private bool trig;
    [SyncVar]
    private int ran;
    public bool MyTurn = false;
    public int RemainActionPoint = 3;
    [SyncVar]
    public int turnCount=0;
    public GameObject TileManager;
    private bool HealthDown;
    // Use this for initialization
    void Start () {
        CurrentPlayerID = 1;
     
	}

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (!reverse)
            {
                counter += Time.deltaTime;
                if (counter >= 30f || RemainActionPoint == 0)
                {
                    CurrentPlayerID = 2;
                    reverse = true;
                    counter = 30f;
                    RemainActionPoint = 3;
                    turnCount++;

                }
            }
            else
            {
                counter -= Time.deltaTime;
                if (counter <= 0f || RemainActionPoint == 0)
                {
                    CurrentPlayerID = 1;
                    reverse = false;
                    counter = 0;
                    RemainActionPoint = 3;
                    turnCount++;
                }
            }
        }



        if (isClient)
        {
            //    Debug.Log(turnCount);
            if (CurrentPlayerID == this.GetComponent<TurnCounter>().OwnId)
            {
                TEXT.SetActive(true);
                TEXT.GetComponentInChildren<Text>().text = "Your Turn\n" + "Remain Action Point: "+RemainActionPoint;
                MyTurn = true;


            }
            else
            {
                TEXT.SetActive(false);
                MyTurn = false;
            }


            if ( turnCount == 4)
            {
                //  Debug.Log("1 TURN OVER");
                    Invoke("TurnOverHealthDown", 0.5f);
                    turnCount = 0;

            }

        }
    }
    private void TurnOverHealthDown()
    {
        GameObject[] tmp = TileManager.GetComponent<TileManager>().tiles;
        for (int i = 1; i < 48; i++)
        {
            if (tmp[i].GetComponent<TileHealthyManager>().HasExploded)
            {
                tmp[i].GetComponent<TileHealthyManager>().health--;

            }
        }
    }




}
