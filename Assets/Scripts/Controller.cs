using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
[RequireComponent(typeof(AnimationLib))]
[RequireComponent(typeof(Selector))]

public class Controller : MonoBehaviour
{
    KeyCode LDown = KeyCode.A;
    KeyCode RDown = KeyCode.D;
    KeyCode LUp = KeyCode.W;
    KeyCode RUp = KeyCode.E;
    KeyCode Select = KeyCode.S;
    Spawner spw;
    TechtonicsController tController;
    [SerializeField] GameObject Blorbo;

    [SerializeField] float Sensetivity = 10;

    float timer;

    [SerializeField] DialogueManager dialogue;

    void Start()
    {
        spw = GetComponent<Spawner>();
        tController = spw.GetComponent<TechtonicsController>();
        timer = 0;
        dialogue.ShowDialogue("Intro","Hello there! I'm Blorbo. [br]And this... [br]Is my world! [br][br]Why not try poking about!");
        Blorbo.GetComponent<Animator>().SetTrigger("trHappy");
    }

    void Update()
    {
        if (tController.Selected != null)
        {
            if (timer > 0.8f)
            {
                dialogue.ShowDialogue("SelectionMade", $"Nice! You picked a tile![br]That there is {AOrAn(tController.Selected.GetComponent<PlateData>().PlateType)} " + "-plate [br][br]Push it around with AWED and see what it does!");

                if (Input.GetKeyDown(LDown))
                {
                    tController.Selected.GetComponent<Selector>().Animate.DoAnimation(tController.Selected.GetComponent<Selector>().Animate.Crunch(new Vector3(-1, 0, 0)));
                    tController.DetermineCollide(new Vector3(-1, 0, 0));
                    timer = 0;
                }

                if (Input.GetKeyDown(RDown))
                {
                    tController.Selected.GetComponent<Selector>().Animate.DoAnimation(tController.Selected.GetComponent<Selector>().Animate.Crunch(new Vector3(0, 0, -1)));
                    tController.DetermineCollide(new Vector3(0, 0, -1));
                    timer = 0;
                }

                if (Input.GetKeyDown(LUp))
                {
                    tController.Selected.GetComponent<Selector>().Animate.DoAnimation(tController.Selected.GetComponent<Selector>().Animate.Crunch(new Vector3(0, 0, 1)));
                    tController.DetermineCollide(new Vector3(0, 0, 1));
                    timer = 0;
                }

                if (Input.GetKeyDown(RUp))
                {
                    tController.Selected.GetComponent<Selector>().Animate.DoAnimation(tController.Selected.GetComponent<Selector>().Animate.Crunch(new Vector3(1, 0, 0)));
                    tController.DetermineCollide(new Vector3(1, 0, 0));
                    timer = 0;
                }

                if (Input.GetKeyDown(Select))
                {
                    GetComponent<CreatureController>().ShowTerritory();
                    dialogue.ShowDialogue("Selector", "I see you found the... Thing.[br]Thing for the... \n Selector![br][br]It tells you where the predator can get to.[br]If it glows Red, then it is in hunting range...");
                    Blorbo.GetComponent<Animator>().SetTrigger("trConfused");
                }
                if (Input.GetKeyUp(Select))
                {
                    GetComponent<CreatureController>().HideTerritory();
                }

                if (Input.mouseScrollDelta != null)
                {
                    if (GetComponent<Camera>().orthographicSize <= 9 && GetComponent<Camera>().orthographicSize >= 1)
                    {
                        GetComponent<Camera>().orthographicSize += Input.mouseScrollDelta.y;
                    }
                    GetComponent<Camera>().orthographicSize = math.max(math.min(GetComponent<Camera>().orthographicSize, 9), 1);
                }

                if (Input.mousePositionDelta != null)
                {
                    if (Input.GetMouseButton(1))
                    {
                        GetComponent<Camera>().transform.RotateAround(new Vector3(4.5f, 0, 4.5f), Vector3.up, Input.mousePositionDelta.x * Time.deltaTime * Sensetivity);
                    }
                    //GetComponent<Camera>().transform.rotation = quaternion.EulerXYZ(new Vector3(GetComponent<Camera>().transform.rotation.x, math.max(math.min(GetComponent<Camera>().transform.rotation.y, 75), 15), GetComponent<Camera>().transform.rotation.z));
                }
            }
            timer += Time.deltaTime;
        }
        else if (Input.anyKeyDown && !Input.GetKeyDown(Select))
        {
            dialogue.ShowDialogue("FailSelect", "OH!\n\nTo move techtonic plates you need to select one.[br][br]Click it first!");
            Blorbo.GetComponent<Animator>().SetTrigger("trConfused");
        }
    } 

    public static string AOrAn(string word)
    {
        if (string.IsNullOrEmpty(word)) return "a";

        char firstLetter = char.ToLower(word[0]);

        // Common vowel sounds
        string vowels = "aeiou";

        // Special vowel-sound exceptions (e.g., “an hour”, “an honor”)
        string[] specialCases = { "hour", "honest", "honor", "heir" };
        foreach (var special in specialCases)
        {
            if (word.ToLower().StartsWith(special))
                return "an";
        }

        return vowels.Contains(firstLetter) ? "an " + word : "a " + word;
    }
}