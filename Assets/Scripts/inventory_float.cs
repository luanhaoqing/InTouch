using UnityEngine;
using System.Collections;

public class inventory_float : MonoBehaviour {
    public float riseHeight;


	// Use this for initialization
	void Start () {
        InventoryMoveUp();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InventoryMoveUp()
    {
        iTween.MoveBy(gameObject, iTween.Hash("y", riseHeight, "space","world", "easeType", "easeInOutCubic", "loopType", "pingPong", "delay", 5));
        AudioCenter.PlayCantDoThat();
        Debug.Log("Play Sound: Can't Do That");
    }
}
