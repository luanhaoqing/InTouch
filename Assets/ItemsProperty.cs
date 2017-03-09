using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class ItemsProperty : MonoBehaviour {
    public int Player_ID;
    public bool trade;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if(Input.GetKeyDown(KeyCode.T))
        {
            TradeItem();
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
         //   Player_ID = other.GetComponentInParent<PlayerIDOnBoard>().PlayerIDOB;
            this.transform.position = other.transform.parent.transform.position;
            this.transform.parent = other.transform;
        }
    }
    public void TradeItem()
    {
    //    if (Player_ID != GameObject.Find("TrunCounter").GetComponent<TurnCounter>().OwnId)
      //      return;
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        
        for(int i=0;i<2;i++)
        {
            if(this.transform.parent!=Players[i])
            {
                
                this.gameObject.transform.position = Players[i].transform.position;
                this.transform.parent = Players[i].transform;
            }
        }
    }

}
