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
    [SyncVar]
    public bool UseItemOn = false;
    public GameObject MyPlayer,otherPlayer,currentplayer;
    public GameObject lightning;
    private bool RoundEnd = false;
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
                if ( RemainActionPoint <= 0&&CurrentPlayerID==1&& !RoundEnd)
                {
                    CurrentPlayerID = 2;
                    reverse = true;
                    //  counter = 30f;
                    RoundEnd = true;
                    Invoke("SetActionPointBack", 1.0f);
                  //  RemainActionPoint = 3;
                    turnCount++;
                    HasTurn = false;
                    TradeOn = false;
                    UseItemOn = false;

                }
            }
            else
            {
               // counter -= Time.deltaTime;
                if ( RemainActionPoint <= 0 && CurrentPlayerID == 2&& !RoundEnd)
                {
                    CurrentPlayerID = 1;
                    reverse = false;
                    //    counter = 0;
                    RoundEnd = true;
                    Invoke("SetActionPointBack", 1.0f);
                    //RemainActionPoint = 3;
                    turnCount++;
                    HasTurn = false;
                    TradeOn = false;
                    UseItemOn = false;

                }
            }
        }



        if (isClient)
        {
            GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
            if (Players.Length == 2)
            {
                for (int i = 0; i < Players.Length; i++)
                {
                    if (Players[i].GetComponent<PlayerIDOnBoard>().PlayerIDOB == this.GetComponent<TurnCounter>().OwnId)
                    {
                        MyPlayer = Players[i];
                    }
                    else
                    {
                        otherPlayer = Players[i];
                    }
                }
                if (CurrentPlayerID == MyPlayer.GetComponent<PlayerIDOnBoard>().PlayerIDOB)
                    currentplayer = MyPlayer;
                else
                    currentplayer = otherPlayer;
                /*Use for detect rabbit position to open or close trade mode and item use mode*/
                float temp = currentplayer.GetComponent<ControllerOfPlayerOntheBoard>().rabbit.transform.position.x;
                if (temp > 20 || temp < -20)
                    TradeOn = true;
                else
                    TradeOn = false;

                temp = currentplayer.GetComponent<ControllerOfPlayerOntheBoard>().rabbitForItem.transform.position.x;
                if (temp > 20 || temp < -20)
                    UseItemOn = true;
                else
                    UseItemOn = false;



            }
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
                    lightning.SetActive(false);
                    lightning.SetActive(true);
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
    public void SetActionPointBack()
    {
        RemainActionPoint = 3;
        RoundEnd = false;
    }


}
