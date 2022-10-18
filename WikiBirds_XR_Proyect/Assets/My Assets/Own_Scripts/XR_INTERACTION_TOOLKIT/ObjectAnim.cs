using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnim : MonoBehaviour
{
    // This scripts contains all animations actions for all objects to be called by the XR Interactable Events

    [SerializeField]
    private Animator objectAnimator;
    [SerializeField]
    private string StateAnimationName;

    // Start is called before the first frame update
    void Start()
    {
        if (objectAnimator == null)
            objectAnimator = GetComponent<Animator>();
        if (StateAnimationName == null)
            StateAnimationName = "";
    }

    //Pull the Trigger
    public void PullTheTrigger()
    {
        objectAnimator.Play(StateAnimationName);
    }


}
