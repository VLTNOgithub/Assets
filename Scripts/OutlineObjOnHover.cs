using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OutlineObjOnHover : MonoBehaviour
{
    private XRDirectInteractor directInteractor;

    void Start()
    {
        directInteractor = GetComponent<XRDirectInteractor>();

        directInteractor.hoverEntered.AddListener(OnHoverEntered);
        directInteractor.hoverExited.AddListener(OnHoverExited);
    }

    void OnHoverEntered(HoverEnterEventArgs args)
    {
        GameObject hoveredObject = args.interactableObject.colliders[0].gameObject;
        OutlineObject(hoveredObject, Outline.Mode.OutlineAll, Color.yellow, 5f);
    }
    
    void OnHoverExited(HoverExitEventArgs args)
    {
        GameObject hoveredObject = args.interactableObject.colliders[0].gameObject;
        hoveredObject.GetComponent<Outline>().enabled = false;
    }

    public void OutlineObject(GameObject obj, Outline.Mode mode, Color col, float width)
    {
        var outline = obj.GetComponent<Outline>();
        outline.enabled = true;

        outline.OutlineMode = mode;
        outline.OutlineColor = col;
        outline.OutlineWidth = width;
    }
}
