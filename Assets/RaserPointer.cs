using UnityEngine;
using System.Collections;

public class RaserPointer : MonoBehaviour {
    public LineRenderer RaserLight;
    public bool Show=false;
    public GameObject tile;
    public GameObject rabbitForLaser;
	// Use this for initialization
	void Start () {
        RaserLight=this.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rabbitForLaser.transform.position.x > 50)
            Show = true;
        else
            Show = false;
	    if(Show)
        {
            StopCoroutine("ShowLaser");
            StartCoroutine("ShowLaser");
        }
        else
        {
            RaserLight.enabled = false;
        }
	}
    IEnumerator ShowLaser()
    {
        RaserLight.enabled = true;
        while(Show)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit[] hit;
            hit = Physics.RaycastAll(ray, 5);
            RaserLight.SetPosition(0, ray.origin);
            bool hasTile = false;
            RaycastHit hitTile=hit[0];
            for(int i=0;i<hit.Length;i++)
            {
                if(hit[i].transform.CompareTag("Tile")&& hit[i].transform.gameObject.name!="StartTile")
                {
                    if (tile != null && tile!=hit[i].transform.gameObject)
                        tile.GetComponent<TileHealthyManager>().CurrentTarget = false;
                    hasTile = true;
                    hitTile = hit[i];
                    tile = hit[i].transform.gameObject;
                    tile.GetComponent<TileHealthyManager>().CurrentTarget = true;
                    break;
                }
            }
            if (hasTile)
                RaserLight.SetPosition(1, hitTile.point);
            else
            {
                RaserLight.SetPosition(1, ray.GetPoint(5));

                if(tile!=null)
                    tile.GetComponent<TileHealthyManager>().CurrentTarget = false;
                tile = null;
            }
            yield return null;
        }
        RaserLight.enabled = false;
    }
}
