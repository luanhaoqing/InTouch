using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
    public GameObject[] Items;
    public int ItemNumber = 0;
    public Transform[] positions;
    public int GemNumber = 0;
    public bool HasKey=false;
    public GameObject turnCounter;
    public GameObject[] Aplights;
    public bool ItemActive=false;
    private GameObject _preuseItem;
    // Use this for initialization
    void Start () {
        Items = new GameObject[4];
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


    public void ActiveUseItem()
    {
        if (!ItemActive)
        {
            for (int i = 0; i < ItemNumber; i++)
            {
                if (Items[i].tag == "ITEM" && Items[i].GetComponent<ItemsProperty>().CouldUse)
                {
                    Items[i].transform.position += new Vector3(0, 0.1f, 0);
                }
            }
            ItemActive = true;
        }
    }
    public void DeActiveUseItem()
    {
        if (ItemActive)
        {
            for (int i = 0; i < ItemNumber; i++)
            {
                if (Items[i].tag == "ITEM" && Items[i].GetComponent<ItemsProperty>().CouldUse)
                {
                    Items[i].transform.position -= new Vector3(0, 0.1f, 0);
                }
            }
            ItemActive = false;
        }
    }

    public void preuseItem(GameObject healingItem)
    {
        Debug.Log("PreUseItem");
        Vector3 temp = GetComponentInChildren<DetectHealing>().model.transform.position;
        temp.y = GetComponentInChildren<DetectHealing>().OriginHeight;
        healingItem.SetActive(false);
        _preuseItem = healingItem;
       
        GetComponentInChildren<DetectHealing>().model.transform.position =temp;
    }
    public void UseItem()
    {
        if(_preuseItem!=null)
        {
            for(int i=0;i<ItemNumber;i++)
            {
                if(Items[i]==_preuseItem)
                {
                    Items[i] = null;
                    Destroy(_preuseItem);
                    Rearrange();
                    break;
                }
            }
        }
    }
    public void Setpositon(GameObject objct)
    {
        if (objct.tag == "ITEM"&& !objct.GetComponent<ItemsProperty>().CouldUse)
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
                    if(!obj.GetComponent<ItemsProperty>().CouldUse)
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
            if(Items[i].tag=="ITEM"&& !Items[i].GetComponent<ItemsProperty>().CouldUse)
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
        GameObject[] temp = new GameObject[4];
        for(int i=0;i<4;i++)
        {
            temp[i] = null;
        }
        int ItemNum=0;
        for(int i=0;i<4;i++)
        {
            if(Items[i]!=null)
            {
                temp[ItemNum] = Items[i];
                Items[i] = null;
                ItemNum++;
            }
        }
        ItemNumber = 0;
        for(int i=0;i< ItemNum; i++)
        {
            Setpositon(temp[i]);
        }

        /*
        int i = 0;
        while(Items[i]==null)
        {
            i++;
            if (i == 3)
                break;
        }
        Items[0] = Items[i];
        Items[i] = null;*/
    }
        
}
