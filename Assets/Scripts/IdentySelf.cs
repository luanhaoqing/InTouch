using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class IdentySelf : NetworkBehaviour {
    public GameObject body,LH,RH;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
    }
    public override void OnStartLocalPlayer()
    {
      //  body.GetComponent<MeshRenderer>().material.color = Color.blue;
        LH.GetComponent<MeshRenderer>().material.color = Color.blue;
        RH.GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
