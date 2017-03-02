using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TileManager : NetworkBehaviour {
    public GameObject tile;
    public  GameObject[] tiles;
	// Use this for initialization
	void Start () {
     /*   if (isServer)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                    // GameObject tmp = Instantiate(tile);
                   // this.GetComponent<GenerateMap>().GenerateTile(new Vector3(0.26f + j * 0.2f, 0, -0.46f + i * 0.2f), 0);
                    GameObject tmp = Instantiate(tile);
                    tmp.transform.position = new Vector3(0.26f + j * 0.2f, 0, -0.46f + i * 0.2f);
                    tmp.transform.parent = this.transform;
                    tmp.transform.parent = this.transform;
                    tiles[6 * i + j] = tmp;
                }
            }
        }*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
