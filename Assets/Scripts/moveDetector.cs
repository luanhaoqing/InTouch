using UnityEngine;
using System.Collections;

public class moveDetector : MonoBehaviour {

    bool touched = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SPHelper")){
            touched = true;
        }
    }

    public bool IfTouched()
    {
        return touched;
    }
}
