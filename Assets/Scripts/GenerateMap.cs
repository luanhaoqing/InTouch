using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class GenerateMap : NetworkBehaviour {
    [SyncVar]
    public int RanTileNum;
    public GameObject[] tiles;
    private int GemNumbers = 6;
    private int KeyNumbers = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       

	}
    public void GenerateTile(Vector3 position,int ran)
    {
       
       
        GameObject tmp = Instantiate(tiles[ran]);
        tmp.transform.position = position;
        tmp.transform.parent = this.transform;
    }
    public void getRandomTile(GameObject player)
    {
        if (!isServer)
            return;
        {
            int ran = Random.Range(0, 4);
            if (ran == 2)
                GemNumbers--;
            if(GemNumbers==0)
            {
                RanTileNum = 0;
            }
            else
                RanTileNum = ran;
        }
    }
}
