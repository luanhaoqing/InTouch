using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
    public GameObject[] Items;
    public int ItemNumber = 0;
    public Transform[] positions;
    // Use this for initialization
    void Start () {
        Items = new GameObject[4];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Setpositon(GameObject objct)
    {
        if (ItemNumber < 4)
        {
            objct.transform.position = positions[ItemNumber].position;
            objct.transform.parent = this.transform;
            Items[ItemNumber] = objct;
            ItemNumber++;
        }
    }
    public void Trade(GameObject obj)
    {
        for(int i=0; i<4;i++)
        {
            if(Items[i]==obj)
            {
                Items[i] = null;
                ItemNumber--;
                //re-arrange inventory
                for (int j =i+1;j<ItemNumber;j++)
                {
                    Items[j - 1] = Items[j];
                }
                //change to other's 
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                for (int j = 0; j < 2; j++)
                {
                    if (players[j] != this.transform.parent.gameObject)
                    {
                        players[j].GetComponentInChildren<Inventory>().Setpositon(obj);
                    }
                   
                }
                break;
            }
        }    
     }
        
}
