using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    [Header("Typing Settings")]
    public float typingSpeed = 0.02f;
    public float ellipsisPause = 0.5f;
    public float lineBreakPause = 0.3f;

    private Coroutine typingCoroutine;

    // Track triggered dialogue IDs to prevent repeats
    private HashSet<string> triggeredDialogues = new HashSet<string>();

    /// <summary>
    /// Show a dialogue line by unique ID.
    /// If already triggered, will skip.
    /// </summary>
    /// <param name="id">Unique dialogue ID</param>
    /// <param name="sentence">Text to display</param>
    public void ShowDialogue(string id, string sentence)
    {
        if (triggeredDialogues.Contains(id))
        {
            //Debug.Log($"Dialogue with ID '{id}' already triggered.");
            return;
        }

        triggeredDialogues.Add(id);

        dialogueBox.SetActive(true);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Hide the dialogue box and stop typing.
    /// </summary>
    public void HideDialogue()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueBox.SetActive(false);
        dialogueText.text = "";
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        for (int i = 0; i < sentence.Length; i++)
        {
            // Check for "..."
            if (i + 2 < sentence.Length && sentence[i] == '.' && sentence[i + 1] == '.' && sentence[i + 2] == '.')
            {
                dialogueText.text += "...";
                i += 2; // Skip the next two dots
                yield return new WaitForSeconds(ellipsisPause);
            }
            // Check for "[br]" token (delayed line break)
            else if (i + 3 < sentence.Length && sentence.Substring(i, 4) == "[br]")
            {
                dialogueText.text += "\n";
                i += 3; // Skip "[br]"
                yield return new WaitForSeconds(lineBreakPause);
            }
            else
            {
                dialogueText.text += sentence[i];
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
}
