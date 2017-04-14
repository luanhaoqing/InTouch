using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HandControl : MonoBehaviour
{
    public bool TradeModeActive = false;
    public GameObject TurnCounter;
    public GameObject helper;
    public bool pokeHelperGrey;

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

        if (other.CompareTag("ITEM")&& TurnCounter.GetComponent<CurrentPlayer>().TradeOn)
        {
            other.GetComponentInParent<Inventory>().Trade(other.gameObject);
        }

        // Poke helper to trigger some feedback
        if (pokeHelperGrey && (other.gameObject == helper))
        {
            other.GetComponentInParent<Inventory>().Trade(other.gameObject);
            this.transform.GetComponentInParent<HelperAnimation>().SetHelperSad(3f);
        }
    }

    public void ActivateTrade(bool command)
    {
        TradeModeActive = command;
    }




}
