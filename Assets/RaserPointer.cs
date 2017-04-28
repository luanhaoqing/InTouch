using UnityEngine;
using System.Collections;

public class RaserPointer : MonoBehaviour {
    public LineRenderer RaserLight;
    public bool Show=false;
	// Use this for initialization
	void Start () {
        RaserLight=this.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Show)
        {
            StopCoroutine("ShowLaser");
            StartCoroutine("ShowLaser");
        }
	}
    IEnumerator ShowLaser()
    {
        RaserLight.enabled = true;
        while(Show)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            RaserLight.SetPosition(0, ray.origin);
            if (Physics.Raycast(ray, out hit, 10))
                RaserLight.SetPosition(1, hit.point);
            else
                RaserLight.SetPosition(1, ray.GetPoint(10));

            yield return null;
        }
        RaserLight.enabled = false;
    }
}
