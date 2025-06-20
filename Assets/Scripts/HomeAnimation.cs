using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class HomeAnimation : MonoBehaviour
{
    [SerializeField] float RotateAmount;
    [SerializeField] float RotateSpeed;
    private float timer;

    void Start()
    {
        timer = Random.Range(0f, 360f);
    }
    void Update()
    {
        transform.rotation = quaternion.EulerXYZ(new Vector3(0, 0, math.sin(timer) * RotateAmount / 90));
        timer += Time.deltaTime * RotateSpeed;
    }
}