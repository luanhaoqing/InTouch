using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HandControl : MonoBehaviour
{
    public bool TradeModeActive = false;
    public GameObject TurnCounter;

    // Use this for initialization
    void Start()
    {
        TurnCounter = GameObject.Find("TrunCounter");

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("ITEM")&& TurnCounter.GetComponent<CurrentPlayer>().MyTurn&& TurnCounter.GetComponent<CurrentPlayer>().TradeOn)
        {
            other.GetComponentInParent<Inventory>().Trade(other.gameObject);
        }
    }

    public void ActivateTrade(bool command)
    {
        TradeModeActive = command;
    }

}
