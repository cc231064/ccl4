using UnityEngine;

[RequireComponent(typeof(PlateData))]
public class Simplemover : MonoBehaviour
{
    [SerializeField] Vector3 Move;

    PlateData sp;
    [SerializeField] float Timer;

    void Start()
    {
        sp = GetComponent<PlateData>();
    }

    void Update()
    {
        if (Timer < 1)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            sp.SnappedCoord += Move;
            Timer -= 1;
        }
    }
}
