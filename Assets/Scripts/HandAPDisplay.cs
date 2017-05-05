using UnityEngine;
using System.Collections;

public class HandAPDisplay : MonoBehaviour {
    private GameObject turnCounter;
    public GameObject[] Aplights;
    // Use this for initialization
    void Start () {
        turnCounter = GameObject.Find("TrunCounter");

    }
	
	// Update is called once per frame
	void Update () {
         if(turnCounter.GetComponent<CurrentPlayer>().RemainActionPoint==3)
           {
               for(int i=0;i<3;i++)
               {
                   Aplights[i].SetActive(true);
               }
           }
           if(turnCounter.GetComponent<CurrentPlayer>().RemainActionPoint == 2)
           {
               Aplights[0].SetActive(false);
               Aplights[1].SetActive(true);
               Aplights[2].SetActive(true);
           }
           if (turnCounter.GetComponent<CurrentPlayer>().RemainActionPoint == 1)
           {
               Aplights[0].SetActive(false);
               Aplights[1].SetActive(false);
               Aplights[2].SetActive(true);
           }
           if (turnCounter.GetComponent<CurrentPlayer>().RemainActionPoint == 0)
           {
               Aplights[0].SetActive(false);
               Aplights[1].SetActive(false);
               Aplights[2].SetActive(false);
           }
    }
}
