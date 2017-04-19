using UnityEngine;
using System.Collections;

public class touchDetector : MonoBehaviour {

    bool touched = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SPHand"))
        {
            touched = true;
        }
    }

    public bool IfTouched()
    {
        return touched;
    }
}
