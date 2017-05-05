using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class ChangeCharacter : NetworkBehaviour {

    public GameObject[] mine;
    public GameObject[] pre;

	// Use this for initialization
	void Start () {
	    if(isLocalPlayer)
        {
            for (int i=0;i<4;i++)
            {
              
                mine[i].SetActive(true);
            }
            for(int i=0;i<4;i++)
            {
                pre[i].SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
       
    }
}
