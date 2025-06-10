using UnityEngine;

[RequireComponent(typeof(Spawner))]
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(Selector))]

public class Controller : MonoBehaviour
{
    KeyCode LDown = KeyCode.A;
    KeyCode RDown = KeyCode.D;
    KeyCode LUp = KeyCode.W;
    KeyCode RUp = KeyCode.E;
    Spawner spw;
    Selector selector;

    float timer;

    void Start()
    {
        spw = GetComponent<Spawner>();
        timer = 0;
    }

    void Update()
    {
        if (timer > 0.8f)
        {
            if (Input.GetKeyDown(LDown))
            {
                spw.Selected.GetComponent<Selector>().Animate.DoAnimation(spw.Selected.GetComponent<Selector>().Animate.Crunch(new Vector3(-1, 0, 0)));
                timer = 0;
            }

            if (Input.GetKeyDown(RDown))
            {
                spw.Selected.GetComponent<Selector>().Animate.DoAnimation(spw.Selected.GetComponent<Selector>().Animate.Crunch(new Vector3(0, 0, -1)));
                timer = 0;
            }

            if (Input.GetKeyDown(LUp))
            {
                spw.Selected.GetComponent<Selector>().Animate.DoAnimation(spw.Selected.GetComponent<Selector>().Animate.Crunch(new Vector3(0, 0, 1)));
                timer = 0;
            }

            if (Input.GetKeyDown(RUp))
            {
                spw.Selected.GetComponent<Selector>().Animate.DoAnimation(spw.Selected.GetComponent<Selector>().Animate.Crunch(new Vector3(1, 0, 0)));
                timer = 0;
            }
        }
        timer += Time.deltaTime;
    }
}