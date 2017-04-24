using UnityEngine;
using System.Collections;

public class EndState : MonoBehaviour {
    private GameObject P1;
    private GameObject P2;
    public int Pass=0;
    public GameObject Clock;
    public GameObject win;
    public GameObject door;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Pass == 2)
        {
            Debug.Log("Win");
            Clock.SetActive(false);
            win.SetActive(true);
            door.GetComponent<Animator>().SetTrigger("DoorOpen");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerOnBoard"))
        {
           if( other.GetComponentInParent<Inventory>().HasKey)
            {
                Pass++;
                if (GameObject.Find("TrunCounter").GetComponent<CurrentPlayer>().RemainActionPoint != 3)
                    GameObject.Find("TrunCounter").GetComponent<CurrentPlayer>().RemainActionPoint = 0;
            }
          
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerOnBoard"))
        {
            if (other.GetComponentInParent<Inventory>().HasKey)
            {
                Pass--;
            }
        }
    }
}
