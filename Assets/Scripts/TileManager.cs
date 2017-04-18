using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TileManager : NetworkBehaviour {
    public GameObject tile;
    public  GameObject[] tiles;
    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int ran = Random.Range(0, 5);

                }
            }
        }
            if (isClient)
            {
                int k = 1;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (i == 3 && j == 2)
                            continue;
                        // GameObject tmp = Instantiate(tile);
                        // this.GetComponent<GenerateMap>().GenerateTile(new Vector3(0.26f + j * 0.2f, 0, -0.46f + i * 0.2f), 0);
                        GameObject tmp = Instantiate(tile);
                        tmp.transform.position = new Vector3(0.26f + j * 0.2f, 0, -0.46f + i * 0.2f);
                        tmp.transform.parent = this.transform;
                        tmp.GetComponent<TileHealthyManager>().Iden[0] = i;
                        tmp.GetComponent<TileHealthyManager>().Iden[1] = j;
                        tiles[k] = tmp;
                        k++;

                    }
                }
            }
        
    }
    // Update is called once per frame
    void Update () {
	
	}
}
