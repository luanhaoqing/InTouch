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
    private GameObject tmp;
    // Use this for initialization
    void Start () {
        this.GetComponent<MeshRenderer>().enabled = false;
        health = 5;
        _text.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	if(health==0)
        {
            // this.gameObject.SetActive(false);
            Destroy(tmp);
            tmp = null;
            HasExploded = false;
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
            player = other.gameObject;
            GameObject.Find("TrunCounter").GetComponent<CurrentPlayer>().RemainActionPoint--;
            health -= 1;
  
            if (!HasExploded)
            {
                tmp = Instantiate(tiles[this.GetComponentInParent<GenerateMap>().RanTileNum]);
        
                tmp.transform.position = this.transform.position;
                tmp.transform.parent = this.transform;
                HasExploded = true;
                _text.SetActive(true);
                Invoke("getRanTile",1.0f);
               
            }
        }
    }
    private void getRanTile()
    {
        this.GetComponentInParent<GenerateMap>().getRandomTile(player);
    }
}
