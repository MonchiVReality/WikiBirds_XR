/*
 * Script que recoge valores de los inputs de mandos VR
 * 
 * Copyright (c) 2022 Javier Munoz Martin de la Sierra
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue_ = pinchAnimationAction.action.ReadValue<float>();
        float gripValue_ = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue_);
        handAnimator.SetFloat("Grip", gripValue_);
        //Debug.Log(triggerValue_);
        //Debug.Log(gripValue_);
    }
}
