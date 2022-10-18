using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 2;
    public GameObject menu_;
    public InputActionProperty showButton_;

    private void Update()
    {
        if (showButton_.action.WasPressedThisFrame())
        {
            menu_.SetActive(!menu_.activeSelf);
            menu_.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        menu_.transform.LookAt(new Vector3(head.position.x, menu_.transform.position.y, head.position.z));
        menu_.transform.forward *= -1;
    }
}
