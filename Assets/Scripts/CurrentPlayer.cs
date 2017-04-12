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
    [SyncVar]
    public int RemainActionPoint = 3;
    [SyncVar]
    public int turnCount=0;
    public GameObject TileManager;
    private bool HealthDown;
    private bool LoseHealth;
    public GameObject clock;
    [SyncVar]
    public bool HasTurn=true;
    private bool localHasTurn=false;
    [SyncVar]
    public bool TradeOn=false;
    public GameObject currentplayer;
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
              //  counter += Time.deltaTime;
                if ( RemainActionPoint == 0&&CurrentPlayerID==1)
                {
                    CurrentPlayerID = 2;
                    reverse = true;
                    counter = 30f;
                    RemainActionPoint = 3;
                    turnCount++;
                    HasTurn = false;
                    TradeOn = false;


                }
            }
            else
            {
               // counter -= Time.deltaTime;
                if ( RemainActionPoint == 0 && CurrentPlayerID == 2)
                {
                    CurrentPlayerID = 1;
                    reverse = false;
                    counter = 0;
                    RemainActionPoint = 3;
                    turnCount++;
                    HasTurn = false;
                    TradeOn = false;

                }
            }
        }



        if (isClient)
        {
            GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
            for(int i=0;i<2;i++)
            {
                if(Players[i].GetComponent<PlayerIDOnBoard>().PlayerIDOB==this.GetComponent<TurnCounter>().OwnId)
                {
                    currentplayer = Players[i];
                    break;
                }
            }
            TradeOn = currentplayer.GetComponent<ControllerOfPlayerOntheBoard>().tradeon;
            if (!localHasTurn&&!HasTurn)
            {
              
                localHasTurn = true;
                Invoke("setClock", 0.5f);
            }
            //    Debug.Log(turnCount);
            if (CurrentPlayerID == this.GetComponent<TurnCounter>().OwnId)
            {
                //TEXT.SetActive(true);
               // TEXT.GetComponentInChildren<Text>().text = "You have "+ RemainActionPoint + " action left";
                MyTurn = true;
               

            }
            else
            {
              //  TEXT.SetActive(false);
                MyTurn = false;
            }


            if ( turnCount == 4&&!LoseHealth)
            {
                    LoseHealth = true;
                 
                    Invoke("TurnOverHealthDown", 0.5f);

                    

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
        if (isServer)
            turnCount = 0;
        Invoke("setLoseHealth",0.5f);
    }
    public void setClock()
    {
     
        
            
            clock.GetComponent<Clock>().DecreaseTurn();
            Invoke("SetHasTurn",0.5f);
        
    }

    public void SetHasTurn()
    {
        HasTurn = true;
        localHasTurn = false;
    }
    public void setLoseHealth()
    {
        LoseHealth = false;
    }



}
