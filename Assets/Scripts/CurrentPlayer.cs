using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
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
    // Use this for initialization
    void Start () {
        CurrentPlayerID = 1;
     
	}

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
/*     if (counter >= 10 || counter <= 0)
            {
                int ran = Random.Range(0, 3);
                trig = true;
            }
            else
                trig = false;
*/
                if (!reverse)
            {
                counter += Time.deltaTime;
                if (counter >= 30f|| RemainActionPoint==0)
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
                if (counter <= 0f||RemainActionPoint == 0)
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
          

            if (CurrentPlayerID==this.GetComponent<TurnCounter>().OwnId)
            {
                TEXT.SetActive(true);
                MyTurn = true;
             

            }
            else
            {
                TEXT.SetActive(false);
                MyTurn = false;
            }


            if(turnCount!=0&& turnCount==2)
            {

                GameObject[] tmp = TileManager.GetComponent<TileManager>().tiles;
                for(int i=1;i<48;i++)
                {
                    if(tmp[i].GetComponent<TileHealthyManager>().HasExploded)
                    {
                        tmp[i].GetComponent<TileHealthyManager>().health--;
                    
                    }
                }
                turnCount = 0;
            }

        }

    }




}
