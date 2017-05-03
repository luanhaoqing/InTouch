using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {
    enum STATUS { OFF = 0, ON }
    public Renderer     coreRenderer;
    public Renderer[]   outerRenderer;
    public Transform    outerTransform;
    public Material[]   outerMaterial;
    public float        rotationTime = 3.0f;
    public Color[]      coreColor;

    bool    _isRotating = false;
    int     _index = 0;

    //For covering reverse
    int     _patternIndex= 0;
    int[,]  _pattern = new int[2, 4];

    //Event
    public delegate void OnRoundChange();
    OnRoundChange _roundChangeEvent;

    //--------------------
    // Function
    //--------------------

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DecreaseTurn();
        }
    }

    public void DecreaseTurn(){
        if (_isRotating) return;

        int idx = _pattern[_patternIndex, _index++];
        outerRenderer[idx].material = outerMaterial[(int)STATUS.ON];
        AudioCenter.PlayTurnOver();

        if (_index > outerRenderer.Length - 1){
            _patternIndex = ++_patternIndex % _pattern.GetLength(0);
            StartCoroutine(ChangeCore(0.2f, true, rotationTime));
            AudioCenter.PlayRoundOver();
            AudioCenter.PlayROExplaination();
        }
    }

    public void Register_RoundChange_Event(OnRoundChange ev){
        _roundChangeEvent += ev;
    }

    //Reset All 
    public void Reset(){
        _index = 0;
        for (int i = 0; i < outerRenderer.Length; i++)
        {
            outerRenderer[i].material = outerMaterial[(int)STATUS.OFF];
        }
    }

    void Start(){
        //For Reverse
        _pattern[0, 0] = 0;
        _pattern[0, 1] = 1;
        _pattern[0, 2] = 2;
        _pattern[0, 3] = 3;

        _pattern[1, 0] = 1;
        _pattern[1, 1] = 0;
        _pattern[1, 2] = 3;
        _pattern[1, 3] = 2;
        Reset();
    }

    //Change Gem Color
    IEnumerator ChangeCore(float time, bool isOn, float outerTime){
        float totalTime = 0;

        while(totalTime <= time){
            totalTime += Time.deltaTime;

            if (isOn){
                _isRotating = true;
                coreRenderer.material.color = Color.Lerp(coreColor[(int)STATUS.ON],
                                                        coreColor[(int)STATUS.OFF],
                                                        totalTime / time);
            }else{
                _isRotating = false;
                coreRenderer.material.color = Color.Lerp(coreColor[(int)STATUS.OFF],
                                                        coreColor[(int)STATUS.ON],
                                                        totalTime / time);
            }
            yield return null;
        }

        if(isOn)
            StartCoroutine(RotateOuter(outerTime, time));
        else{
            if (_roundChangeEvent != null){
                _roundChangeEvent();
            }
        }        
    }

    //Rotate Outer
    IEnumerator RotateOuter(float time, float coreTime){
        float totalTime = 0;
        Quaternion currRotation = outerTransform.transform.localRotation;

        float targetAngle = _patternIndex == 1 ? 180 : 0;
        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.left);
        
        while (totalTime <= time){
            totalTime += Time.deltaTime;
 
            if(totalTime/ time > 0.5f){
                Reset();
            }

            outerTransform.localRotation = Quaternion.Lerp(currRotation, targetRotation, totalTime/ time);
            yield return null;
        }

        StartCoroutine(ChangeCore(coreTime, false,0));
    }


}
