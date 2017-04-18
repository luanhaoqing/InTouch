using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class ItemsProperty : MonoBehaviour {
    public bool CouldUse=false;
	// Use this for initialization
	void Start () {
        if (this.gameObject.name == "HealingItem")
            CouldUse = true;
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
            AudioCenter.PlayGetItem();
        }
    }
   

}
