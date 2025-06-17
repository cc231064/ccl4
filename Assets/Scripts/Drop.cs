using UnityEngine;

public class Drop : MonoBehaviour
{
    public float raycastDistance = 5f;
    public LayerMask groundLayer;

    void Start()
    {
        SnapToSurfaceBelow();
    }

    void SnapToSurfaceBelow()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            // Move to surface
            Vector3 newPosition = hit.point;
            Quaternion surfaceRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            transform.position = newPosition + new Vector3(0, transform.localScale.y / 2, 0);
            transform.rotation = surfaceRotation;

            // Parent to the hit object
            transform.SetParent(hit.collider.transform);

            if (GetComponent<AnimationLib>() != null)
            {
                GetComponent<AnimationLib>().positionOriginal = transform.position;
            }
        }
        else
        {
            Debug.LogWarning("No ground detected below.");
        }
    }
}

