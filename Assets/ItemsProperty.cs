using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class ItemsProperty : MonoBehaviour {
    public int Player_ID;
   
    public bool trade;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
            //   Player_ID = other.GetComponentInParent<PlayerIDOnBoard>().PlayerIDOB;
            other.GetComponentInParent<Inventory>().Setpositon(this.gameObject);
            
        }
    }

}
