using UnityEngine;
using System.Collections;

public class SPCouldMoveCollider : MonoBehaviour {

    public Transform walkerPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

       // transform.position = walkerPosition.position;


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile") && (other.GetComponent<TileHealthyManager>().couldMoveTo == false))
        {
            Debug.Log("Closed: " + other.name);
            other.GetComponent<TileHealthyManager>().couldMoveTo = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Tile") && (other.GetComponent<TileHealthyManager>().couldMoveTo == true))
        {
            other.GetComponent<TileHealthyManager>().couldMoveTo = false;
           
        }
    }

}
