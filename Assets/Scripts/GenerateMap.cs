using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class GenerateMap : NetworkBehaviour {
    [SyncVar]
    public int RanTileNum;
    public GameObject[] tiles;
    
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
        if (GameObject.Find("TrunCounter").GetComponent<TurnCounter>().OwnId == player.GetComponent<PlayerIDOnBoard>().PlayerIDOB)
        {
            int ran = Random.Range(0, 4);
            RanTileNum = ran;
        }
    }
}
