using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableObjectCollision : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private int prevLayer;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        prevLayer = gameObject.layer;
        gameObject.layer = 3;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        gameObject.layer = prevLayer;
    }
}
