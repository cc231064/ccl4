using UnityEngine;

public class MouseEventForwarder : MonoBehaviour
{
    public Selector parentSelector;

    void Start()
    {
        parentSelector = gameObject.GetComponentInParent<Selector>();
    }

    void OnMouseOver()
    {
        if (parentSelector != null)
        {
            parentSelector.OnMouseOver();
        }
    }

    void OnMouseExit()
    {
        if (parentSelector != null)
        {
            parentSelector.OnMouseExit();
        }
    }

    void OnMouseDown()
    {
        if (parentSelector != null)
        {
            parentSelector.OnMouseDown();
        }
    }
}
