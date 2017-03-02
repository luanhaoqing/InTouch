using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class GenerateMap : MonoBehaviour {
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
}
