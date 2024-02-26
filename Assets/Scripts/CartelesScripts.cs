using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CartelesScripts : MonoBehaviour
{
    public GameObject window;
    public GameObject indicator;
    public List<string> dialogues;
    public TMP_Text dialogueText;
    public float writingSpeed;
    private int index;
    private int charIndex;
    private bool starte;
    private bool waitForText;

    private void Awake()
    {
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    public void StartDialogue()
    {
        if (starte)
            return;
        starte = true;
        ToggleWindow(true);
        ToggleIndicator(false);
        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        index = 0;
        charIndex = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(Writing());
    }

    public void EndDialogue()
    {
        starte = false;
        waitForText = false;
        StopAllCoroutines();
        ToggleWindow(false);
    }

    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);
        string currentDialogue = dialogues[index];
        dialogueText.text += currentDialogue[charIndex];
        charIndex++;
        if (charIndex < currentDialogue.Length)
        {
            yield return new WaitForSeconds(writingSpeed);
            StartCoroutine(Writing());
        }
        else
        {
            waitForText = true;

        }
    }
    private void Update()
    {
        if (!starte || waitForText)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            index++;
            if (index < dialogues.Count)
            {
                GetDialogue(index);
            }
            else
            {
                ToggleIndicator(true);
                EndDialogue();
            }
        }
    }
}
