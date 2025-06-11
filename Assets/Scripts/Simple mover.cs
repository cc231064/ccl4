using UnityEngine;

[RequireComponent(typeof(Snapper))]
public class Simplemover : MonoBehaviour
{
    [SerializeField] Vector3 Move;

    Snapper sp;
    [SerializeField] float Timer;

    void Start()
    {
        sp = GetComponent<Snapper>();
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
