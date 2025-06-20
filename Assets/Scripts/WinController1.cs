using UnityEngine;

public class WinController1 : MonoBehaviour
{
    [SerializeField] CreatureController Creatures;
    [SerializeField] DialogueManager dialogue;

    public bool WinState = false;
    private int TerritorySize = 0;
    private string Response;
    private int Tries;

    private string AKBlorbo = "Blorbo";
    private string AKConf = "Blorboconf";
    private string AKMad = "Blorbomad";
    void OnMouseDown()
    {
        gameObject.AddComponent<AkGameObj>();
        Creatures.UpdateTerritory();
        WinState = true;
        TerritorySize = 0;
        Tries++;
        for (int i = 0; i < Creatures.Animals.GetLength(0); i++)
        {
            for (int j = 0; j < Creatures.Animals.GetLength(1); j++)
            {
                if (Creatures.Map[i, j].GetComponent<PlateData>().Territory.Contains(Creatures.Predator.name) && Creatures.Predator.name == "fox")
                {
                    TerritorySize++;
                }
                if (Creatures.Animals[i, j] != null)
                {
                    Debug.Log(Creatures.Animals[i, j].name.Contains(Creatures.Prey.name) && Creatures.Map[i, j].GetComponent<PlateData>().Territory.Contains(Creatures.Predator.name));
                    if (Creatures.Animals[i, j].name.Contains(Creatures.Prey.name) && Creatures.Map[i, j].GetComponent<PlateData>().Territory.Contains(Creatures.Predator.name))
                    {
                        WinState = false;
                        Response = "I think the " + Creatures.Prey.name + " can be hunted by the " + Creatures.Predator.name + ".";
                        AkSoundEngine.PostEvent(AKConf, gameObject);
                    }
                }
            }
        }
        if (TerritorySize < 6)
        {
            WinState = false;
            Response = "I think the fox wants more space.";
            AkSoundEngine.PostEvent(AKConf, gameObject);
        }
        if (WinState)
        {
            dialogue.ShowDialogue("Try" + Tries, $"You did it![br]You really did![br][br]This world is perfect![br][br]And you did all this in just " + Tries + " tries!");
            GetComponent<Animator>().SetTrigger("trHappy");
            AkSoundEngine.PostEvent(AKBlorbo, gameObject);
        }
        if (!WinState)
        {
            dialogue.ShowDialogue("Try" + Tries, "Its not quite perfect...[br][br]" + Response);
            GetComponent<Animator>().SetTrigger("trAngry");
            AkSoundEngine.PostEvent(AKMad, gameObject);
        }
    }
}
