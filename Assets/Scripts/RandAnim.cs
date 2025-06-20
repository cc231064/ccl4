using UnityEngine;

public class RandAnim : MonoBehaviour
{
    [SerializeField] Vector2 RandRange;
    private float Timer = 0;
    [SerializeField] string Trigger;

    void Start()
    {
        Timer = Random.Range(0, RandRange.y);        
    }
    void Update()
    {
        if (Timer < 0)
        {
            Timer = Random.Range(RandRange.x, RandRange.y);
            GetComponent<Animator>().SetTrigger(Trigger);
        }
        Timer -= Time.deltaTime;
    }
}
