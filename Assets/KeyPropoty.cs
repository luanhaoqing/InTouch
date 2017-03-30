using UnityEngine;
using System.Collections;

public class KeyPropoty : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerOnBoard"))
        {
            //   Player_ID = other.GetComponentInParent<PlayerIDOnBoard>().PlayerIDOB;
            if (other.GetComponentInParent<Inventory>().GemNumber >= 3)
            {
                other.GetComponentInParent<Inventory>().delete3Gem();
                other.GetComponentInParent<Inventory>().Setpositon(this.gameObject);
                other.GetComponentInParent<Inventory>().HasKey = true;


            }
        }
    }
}
