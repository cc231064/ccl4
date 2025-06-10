using UnityEngine;

public class Snapper : MonoBehaviour
{
    public Vector3 SnapDistance = new Vector3(1, 1, 1);
    public Vector3 SnapOffset;
    public Vector3 SnappedCoord;
    void Start()
    {
        SnappedCoord = transform.position;
        UpdateSnap();
    }

    void UpdateSnap()
    {
        transform.position = SnapOffset + new Vector3(SnappedCoord.x * SnapDistance.x, SnappedCoord.y * SnapDistance.y, SnappedCoord.z * SnapDistance.z);
    }
}
