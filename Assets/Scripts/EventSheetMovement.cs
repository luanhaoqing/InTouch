using UnityEngine;
using System.Collections;

public class EventSheetMovement : MonoBehaviour {

    private Vector3 originalPos;
    private GameObject scrollWrapper;

    // Use this for initialization
    void Start () {
        //iTween.MoveTo(transform.GetChild(0).gameObject, targetPosition.position, 1f);
        scrollWrapper = transform.GetChild(0).gameObject;
        originalPos = scrollWrapper.transform.position;
        // reset transparency of the material
        iTween.FadeTo(transform.GetChild(0).gameObject, iTween.Hash(
            "alpha", 0,
            "time", 0f,
            "oncompletetarget", gameObject
            ));
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void StartFlying(Transform targetPosition)
    {

        iTween.FadeTo(transform.GetChild(0).gameObject, iTween.Hash(
            "alpha", 1,
            "time", 2.5f,
            "oncompletetarget", gameObject
            ));
        iTween.MoveTo(transform.GetChild(0).gameObject, iTween.Hash(
            "position", targetPosition.position + targetPosition.forward * 0.6f,
            "looktarget", targetPosition.position,
            "time", 3f,
            "easetype", iTween.EaseType.easeOutSine,
            "oncomplete", "Disappear", 
            "oncompletetarget", gameObject));
    }

    void Disappear()
    {
        iTween.FadeTo(transform.GetChild(0).gameObject, iTween.Hash(
            "alpha", 0,
            "delay", 3f,
            "time", 1f,
            "oncomplete", "ResetPosition",
            "oncompletetarget", gameObject
            ));
    }

    void ResetPosition()
    {
        Debug.Log(originalPos);
        scrollWrapper.transform.position = originalPos; 
    }
}
