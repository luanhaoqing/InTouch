using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class CloseControl : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            this.GetComponent<OVRCameraRig>().enabled = false;
            this.GetComponent<OVRManager>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
