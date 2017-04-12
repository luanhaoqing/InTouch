using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HandControl : NetworkBehaviour
{
    public bool TradeModeActive = false;
    [SyncVar]
    int test=0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isLocalPlayer)
            return;
        if (other.CompareTag("ITEM"))
        {
            /*
            if (this.GetComponentInParent<Inventory>().ItemNumber != 0)
            {
                
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < 2; i++)
                {
                    if(players[i] == this.transform.parent.gameObject)
                    {
                        players[i].GetComponentInChildren<Inventory>().Setpositon(other.gameObject);
                    }
                    if (players[i] != this.transform.parent.gameObject)
                    {
                        
                        players[i].GetComponent<Inventory>().Items[players[i].GetComponent<Inventory>().ItemNumber] = this.GetComponentInParent<Inventory>().Items[this.GetComponentInParent<Inventory>().ItemNumber - 1];
                        players[i].GetComponent<Inventory>().Items[players[i].GetComponent<Inventory>().ItemNumber].transform.position = players[i].transform.position;
                        players[i].GetComponent<Inventory>().Items[players[i].GetComponent<Inventory>().ItemNumber].transform.parent = players[i].transform;
                        players[i].GetComponent<Inventory>().ItemNumber++;

                        this.GetComponentInParent<Inventory>().ItemNumber--;
                        this.GetComponentInParent<Inventory>().Items[this.GetComponentInParent<Inventory>().ItemNumber] = null;
                        
                        players[i].GetComponentInChildren<Inventory>().Setpositon(other.gameObject);                
                     }
                }
        
            }*/
            other.GetComponentInParent<Inventory>().Trade(other.gameObject);
        }
    }

    public void ActivateTrade(bool command)
    {
        TradeModeActive = command;
    }

}
