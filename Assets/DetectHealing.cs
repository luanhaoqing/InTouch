using UnityEngine;
using System.Collections;

public class DetectHealing : MonoBehaviour {
    public bool couldHeal=false;
    public GameObject model;
    private GameObject currentTile;
	// Use this for initialization
	void Start () {
        model.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	if(this.transform.position.y<-50)
        {
            Heal();
            this.transform.position = this.transform.parent.transform.position;
            model.SetActive(false);
            GetComponentInParent<Inventory>().UseItem();
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile") && other.GetComponent<TileHealthyManager>().HasExploded)
        {
            if (other.GetComponent<TileHealthyManager>().health < 4)
            {
                couldHeal = true;
                currentTile = other.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tile") && other.GetComponent<TileHealthyManager>().HasExploded)
        {
            couldHeal = false;
            currentTile = null;
        }
    }
    public void Heal()
    {
        currentTile.GetComponent<TileHealthyManager>().health++;
    }
}
