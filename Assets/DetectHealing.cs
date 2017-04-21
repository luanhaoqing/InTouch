using UnityEngine;
using System.Collections;

public class DetectHealing : MonoBehaviour {
    public bool couldHeal=false;
    public GameObject model;
    private GameObject currentTile;
    private bool hasHeal=false;
    public GameObject Healingparticle;
	// Use this for initialization
	void Start () {
        model.transform.position+=new Vector3(0,100,0);
	}
	
	// Update is called once per frame
	void Update () {
	if(this.transform.position.y<-50)
        {
            if (!hasHeal)
            {
                hasHeal = true;
                Heal();
                Invoke("afterHeal", 2f);
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile")&&other.name!="StartTile" && other.GetComponent<TileHealthyManager>().HasExploded)
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
        if (other.CompareTag("Tile") && other.name != "StartTile" && other.GetComponent<TileHealthyManager>().HasExploded)
        {
            couldHeal = false;
         //   currentTile = null;
        }
    }
    public void Heal()
    {
        currentTile.GetComponent<UpdateHP>().Heal();
        Healingparticle.SetActive(false);
        Healingparticle.SetActive(true);
    }
    public void afterHeal()
    {
        this.transform.position = this.transform.parent.transform.position;
        model.transform.position += new Vector3(0, 100, 0);
        GetComponentInParent<Inventory>().UseItem();
        hasHeal = false;
    }
}
