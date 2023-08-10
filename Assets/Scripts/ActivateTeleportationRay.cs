using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    void Update()
    {
        InputActionProperty leftInput = leftTeleportation.GetComponent<ActionBasedController>().selectAction;
        InputActionProperty rightInput = rightTeleportation.GetComponent<ActionBasedController>().selectAction;
        
        leftTeleportation.SetActive(leftInput.action.ReadValue<float>() > 0.1f);
        rightTeleportation.SetActive(rightInput.action.ReadValue<float>() > 0.1f);
        
    }
}
