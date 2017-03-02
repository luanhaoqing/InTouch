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
    public GameObject Table;
    [SyncVar]
    private int test;
    [SyncVar]
    private bool trig;
    [SyncVar]
    private int ran;
    // Use this for initialization
    void Start () {
        CurrentPlayerID = 1;
     
	}

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (counter >= 10 || counter <= 0)
            {
                int ran = Random.Range(0, 3);
                trig = true;
            }
            else
                trig = false;

                if (!reverse)
            {
                counter += Time.deltaTime;
                if (counter >= 10f)
                {
                    CurrentPlayerID = 2;
                    reverse = true;
                   
                   
                }
            }
            else
            {
                counter -= Time.deltaTime;
                if (counter <= 0f)
                {
                    CurrentPlayerID = 1;
                    reverse = false;
                   
                   
                }
            }
        }
        if (isClient)
        {
            if (trig)
            {
                test = test + 200;
                Table.GetComponent<GenerateMap>().GenerateTile(new Vector3(test, 0, 0), ran);
               
            }

            if (CurrentPlayerID==this.GetComponent<TurnCounter>().OwnId)
            {
                TEXT.SetActive(true);
                
            }
            else
            {
                TEXT.SetActive(false);
            }
        }

    }




}
