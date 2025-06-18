using UnityEngine;

[RequireComponent(typeof(Animation))]

public class Selector : MonoBehaviour
{
    public Animation Animate;
    public Spawner Instantiator;
    public bool isSelected;
    public bool isHover;

        private string AKEventButton = "Select";

    void Awake()
    {
        gameObject.AddComponent<AkGameObj>();
        Animate = GetComponent<Animation>();
        isSelected = false;
        isHover = false;

        // Auto-assign forwarders to children with colliders
        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            if (col.gameObject != gameObject)
            {
                MouseEventForwarder forwarder = col.gameObject.AddComponent<MouseEventForwarder>();
                forwarder.parentSelector = this;
            }
        }
    }
    
    public void OnMouseOver()
    {
        if (!isHover)
        {
            Animate.DoAnimation(Animate.HoverHighlight());
            isHover = true;
        }
    }

    public void OnMouseExit()
    {
        if (!isSelected)
        {
            isHover = false;
            Animate.DoAnimation(Animate.HoverDeHighlight());
        }
    }

    public void OnMouseDown()
    {
        if (!isSelected)
        {
            Instantiator.ClearSelection();
            isSelected = true;
            Animate.DoAnimation(Animate.SelectedHighlight());
            Instantiator.GetComponent<TechtonicsController>().Selected = gameObject;
            AkSoundEngine.PostEvent(AKEventButton, gameObject);
        }
    }

    public void Deselect()
    {
        isSelected = false;
    }
} 
