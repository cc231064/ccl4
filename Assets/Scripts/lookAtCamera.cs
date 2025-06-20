using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class lookAtCamera : MonoBehaviour
{
    private float Timer;
    public float Speed = 2;
    [SerializeField] public GameObject Camera;
    void Start()
    {
        Speed += Random.Range(-Speed / 10, Speed / 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.transform);
        transform.Rotate(new Vector3(90, 0, 0));
        transform.localScale = new Vector3(0.02f * Camera.GetComponent<Camera>().orthographicSize * (math.sin(Timer) * 0.2F + 0.8f), 1, 0.02f * Camera.GetComponent<Camera>().orthographicSize * (math.sin(Timer) * 0.2F + 0.8f));

        Timer += Time.deltaTime * Speed;

    }
}
