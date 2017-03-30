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
    if(trade)
        {
            TradeItem();
            trade = false;
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
            //   Player_ID = other.GetComponentInParent<PlayerIDOnBoard>().PlayerIDOB;
            other.GetComponentInParent<Inventory>().Setpositon(this.gameObject);
            
        }
    }
    public void TradeItem()
    {
    //    if (Player_ID != GameObject.Find("TrunCounter").GetComponent<TurnCounter>().OwnId)
      //      return;
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        
        for(int i=0;i<2;i++)
        {
            if(this.transform.parent.transform.parent!=Players[i])
            {
                
                this.transform.parent.gameObject.transform.position = Players[i].transform.position;
                this.transform.parent.transform.parent = Players[i].transform;
            }
        }
    }

}
