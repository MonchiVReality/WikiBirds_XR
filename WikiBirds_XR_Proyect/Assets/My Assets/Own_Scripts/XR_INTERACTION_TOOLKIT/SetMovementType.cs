using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetMovementType : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider continiousMovement;
    public ActivateTeleportationRay teleportationMovement;

    public void SetTypeFromIndex(int index)
    {
        switch (index)
        {
            case 0:
                continiousMovement.enabled = true;
                teleportationMovement.enabled = false;
                break;
            case 1:
                continiousMovement.enabled = false;
                teleportationMovement.enabled = true;
                break;

        }
    }
}
