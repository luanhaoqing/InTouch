using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
    public GameObject[] Items;
    public int ItemNumber = 0;
    public Transform[] positions;
    public int GemNumber = 0;
    // Use this for initialization
    void Start () {
        Items = new GameObject[4];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Setpositon(GameObject objct)
    {
        if (objct.tag == "ITEM")
            GemNumber++;
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
        if (obj.tag == "ITEM")
        {
            for (int i = 0; i < 4; i++)
            {
                if (Items[i] == obj)
                {
                    Items[i] = null;
                    ItemNumber--;
                    GemNumber--;
                    //re-arrange inventory
                    for (int j = i + 1; j < ItemNumber; j++)
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
    public void delete3Gem()
    {
        int deleteNum=0;
        for(int i=0;i<4&&deleteNum<3;i++)
        {
            if (Items[i] == null)
                continue;
            if(Items[i].tag=="ITEM")
            {
                Destroy(Items[i]);
                Items[i] = null;
                deleteNum++;
            }
        }
        ItemNumber -= 3;
        Rearrange();
    }
    private void Rearrange()
    {
        int i = 0;
        while(Items[i]==null)
        {
            i++;
            if (i == 3)
                break;
        }
        Items[0] = Items[i];
        Items[i] = null;
    }
        
}
