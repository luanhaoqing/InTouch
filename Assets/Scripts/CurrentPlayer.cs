using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class CurrentPlayer : NetworkBehaviour {
    [SyncVar]
    public int CurrentPlayerID;
    public float counter = 0;
    private bool reverse;
    public GameObject TEXT;
    public GameObject Table;
    private int test=0;
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
                if (counter >= 10f)
                {
                    CurrentPlayerID = 2;
                    reverse = true;
                    int ran = Random.Range(0, 3);
                    Table.GetComponent<GenerateMap>().GenerateTile(new Vector3(test++, 0, 0), ran);
                }
            }
            else
            {
                counter -= Time.deltaTime;
                if (counter <= 0f)
                {
                    CurrentPlayerID = 1;
                    reverse = false;
                    int ran = Random.Range(0, 3);
                    Table.GetComponent<GenerateMap>().GenerateTile(new Vector3(test++, 0, 0), ran);
                }
            }
        }
        if (isClient)
        {
           
            if(CurrentPlayerID==this.GetComponent<TurnCounter>().OwnId)
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
