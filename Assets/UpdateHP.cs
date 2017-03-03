using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UpdateHP : MonoBehaviour {
    public GameObject tile;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       this.GetComponent<Text>().text= tile.GetComponent<TileHealthyManager>().health.ToString();
	}
}
