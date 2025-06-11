using UnityEngine;

[RequireComponent(typeof(Animation))]

public class Selector : MonoBehaviour
{
    public Animation Animate;
    public Spawner Instantiator;
    public bool isSelected;
    public bool isHover;

    void Awake()
    {
        Animate = GetComponent<Animation>();
        isSelected = false;
        isHover = false;
    }

    void OnMouseOver()
    {
        if (!isHover)
        {
            Animate.DoAnimation(Animate.HoverHighlight());
            isHover = true;
        }
    }

    void OnMouseExit()
    {
        if (!isSelected)
        {
            isHover = false;
            Animate.DoAnimation(Animate.HoverDeHighlight());
        }
    }

    void OnMouseDown()
    {
        if (!isSelected)
        {
            Instantiator.ClearSelection();
            isSelected = true;
            Animate.DoAnimation(Animate.SelectedHighlight());
            Instantiator.GetComponent<TechtonicsController>().Selected = gameObject;
        }
    }

    public void Deselect()
    {
        isSelected = false;
    }
} 
