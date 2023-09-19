using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;

    private TypeWriterEffect typeWriterEffect;
    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        closeDialogueBox();
        showDialogue(testDialogue);
    }

    private void showDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(stepThroughDialogue(dialogueObject));
    }

    private IEnumerator stepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return runTypingEffect(dialogue);
            textLabel.text = dialogue;
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }
        closeDialogueBox();
    }
    
    private IEnumerator runTypingEffect(string dialogue) {
        typeWriterEffect.run(dialogue, textLabel);
        while (typeWriterEffect.isRunning) {
            yield return null;
            if (Input.GetKeyDown(KeyCode.F)) {
                typeWriterEffect.Stop();
            }
        }
    }

    private void closeDialogueBox() {
       dialogueBox.SetActive(false);
       textLabel.text = string.Empty;
    }
}
