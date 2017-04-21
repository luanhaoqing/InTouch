using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class HandControl : MonoBehaviour
{
    public bool TradeModeActive = false;
    public bool UseItemActive = false;
    public GameObject TurnCounter;
    public GameObject helper;
    public bool pokeHelperGrey;
    private bool TradeCoolDown = true;
    private bool UseItemCoolDown = true;
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
        Debug.Log(other.name);

  
        if (other.CompareTag("ITEM")&& TurnCounter.GetComponent<CurrentPlayer>().TradeOn&& TurnCounter.GetComponent<CurrentPlayer>().RemainActionPoint==3&& TradeCoolDown)
        {
            Debug.Log("TouchTradeItem");
            TradeCoolDown = false;
            Invoke("reduceActionPoint", 2.0f);
            other.GetComponentInParent<Inventory>().Trade(other.gameObject);
        }
        if(other.CompareTag("ITEM") && other.GetComponent<ItemsProperty>().CouldUse&& TurnCounter.GetComponent<CurrentPlayer>().UseItemOn&& UseItemCoolDown)
        {
            //use item
            other.GetComponentInParent<Inventory>().preuseItem(other.gameObject);
            GetComponentInParent<ControllerOfPlayerOntheBoard>().controlMode = 5;
       //     UseItemCoolDown = false;
            
        }
        // Poke helper to trigger some feedback
        if (pokeHelperGrey && (other.gameObject == helper))
        {

            this.transform.GetComponentInParent<HelperAnimation>().SetHelperSad(3f);
        }
    }

    public void ActivateTrade(bool command)
    {
        TradeModeActive = command;
    }
    public void reduceActionPoint()
    {
        Debug.Log("reducedActionPoint");
        TurnCounter.GetComponent<CurrentPlayer>().RemainActionPoint = 0;
        TradeCoolDown = true;
    }
     void UseItem(GameObject other)
    {
       
    }



}
