using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 initialLocalPos_;
    private Quaternion initialLocalRot_;

    // Start is called before the first frame update
    void Start()
    {
        if (!attachTransform)
        {
            GameObject attachPoint_ = new GameObject("Offset Grab Pivot");
            attachPoint_.transform.SetParent(transform, false);
            attachTransform = attachPoint_.transform;
        }
        else
        {
            initialLocalPos_ = attachTransform.localPosition;
            initialLocalRot_ = attachTransform.localRotation;

        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            attachTransform.position = args.interactorObject.transform.position;
            attachTransform.rotation = args.interactorObject.transform.rotation;
        }
        else
        {
            attachTransform.localPosition = initialLocalPos_;
            attachTransform.localRotation = initialLocalRot_;
        }

        base.OnSelectEntered(args);
    }
}
