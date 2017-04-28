using UnityEngine;
using System.Collections;

public class DetectionandHighLight : MonoBehaviour {
    bool CouldMove=false;
    public GameObject cursor;
	// Use this for initialization
	void Start () {
        cursor.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {


    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tile")) //&& this.transform.position != transform.parent.position) // add this condition to make sure the cursor does not bounce back and forth.
        {
            other.GetComponent<TileHealthyManager>().couldMoveTo = true;
            cursor.SetActive(true);
            CouldMove = true;
            if(other.gameObject.name != "StartTile"&&!other.GetComponent<TileHealthyManager>().HasExploded)
                other.GetComponent<TileHealthyManager>().cursor.SetActive(true);
            //other.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tile") && other.gameObject.name != "StartTile")
        {
            other.GetComponent<TileHealthyManager>().couldMoveTo = false;
            cursor.SetActive(false); 
            CouldMove = false;
            other.GetComponent<TileHealthyManager>().cursor.SetActive(false);
            //  other.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public bool IfCouldMove()
    {
        return CouldMove;
    }
}
