using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
public class TileHealthyManager : MonoBehaviour {

    public int[] Iden;
    public int health;
    public GameObject _text;
    public GameObject[] tiles;
    public bool HasExploded;
    private GameObject player;
    public GameObject tmp;
    private bool HasPlayer;
    public GameObject End;
    public GameObject clock;
    public GameObject cursor;
    public GameObject TileManager;
    public bool couldMoveTo=false;
    public GameObject cursor_arrow;
    public bool CurrentTarget = false;
    // Use this for initialization
    void Start () {
        this.GetComponent<MeshRenderer>().enabled = false;
        cursor_arrow.SetActive(false);
        health = 5;
        _text.SetActive(false);
        clock = GameObject.Find("pf_Clock");
        End = GameObject.Find("you_lose");
        TileManager = GameObject.Find("TileManager");
    }
	
	// Update is called once per frame
	void Update () {
	if(health==0&&HasExploded)
        {
            // this.gameObject.SetActive(false);
            Invoke("DetectDeath", 1f);         
        }
        if (CurrentTarget)
            cursor_arrow.SetActive(true);
        else
            cursor_arrow.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
            HasPlayer = true;
            player = other.gameObject;
            GameObject.Find("TrunCounter").GetComponent<CurrentPlayer>().RemainActionPoint--;
            health -= 1;
            if (!HasExploded)
            {
                tmp = Instantiate(tiles[this.GetComponentInParent<GenerateMap>().RanTileNum]);
        
                tmp.transform.position = this.transform.position+new Vector3(0,0.002f,0);
                tmp.transform.parent = this.transform;
                tmp.transform.localScale = new Vector3(5, 8, 5);
                HasExploded = true;
                GameObject[] flames = this.GetComponent<UpdateHP>().flame;
                for(int i=0;i<4;i++)
                {
                    flames[i].SetActive(true);
                }
                //     _text.SetActive(true);
                Invoke("getRanTile",1.0f);
               
            }
        }
    }




    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
            HasPlayer = false;
        }
    }
    private void getRanTile()
    {
        this.GetComponentInParent<GenerateMap>().getRandomTile(player);
    }

    public void turnOffCursor() { 
        this.GetComponent<MeshRenderer>().enabled = false;
        foreach (Transform child in transform) 
            if (child.CompareTag("Cursor"))
            {
                child.gameObject.SetActive(false);
            }
    }

    public void turnOnCursor()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
        foreach (Transform child in transform)
            if (child.CompareTag("Cursor"))
            {
                child.gameObject.SetActive(true);
            }
    }
    public void DetectDeath()
    {
        if (HasPlayer)
        {
            GameObject[] temp = TileManager.GetComponent<TileManager>().tiles;
            for (int i = 1; i < 48; i++)
            {
                if (temp[i].GetComponent<TileHealthyManager>().HasExploded)
                {
                    temp[i].GetComponent<TileHealthyManager>().health = 0;
                    temp[i].GetComponent<TileHealthyManager>().tmp.GetComponentInChildren<Animator>().SetTrigger("Break");

                }
            }
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for(int i=0;i<2;i++)
            {
                players[i].GetComponent<ControllerOfPlayerOntheBoard>().PlayerOnBoard.SetActive(false);
            }
            clock.SetActive(false);
            End.transform.localScale = new Vector3(2, 2, 2);
           // Destroy(tmp);
         //   tmp = null;
            HasExploded = false;
         //   this.gameObject.SetActive(false);
        }
        else
        {
            tmp.GetComponentInChildren<Animator>().SetTrigger("Break");
            HasExploded = false;
            Invoke("DeleteIsland", 2.0f);
        }
    }

    public void DeleteIsland()
    {
        Destroy(tmp);
        
    }
}
