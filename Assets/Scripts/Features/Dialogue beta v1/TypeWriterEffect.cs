using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField][Range(5, 25)] private float typeSpeed = 25f;
    public bool isRunning { get; private set; }
    
    private readonly Dictionary<HashSet<char>, float> punctuations = new() {
            { new HashSet<char> { '.', '!', '?' }, .7f },
            { new HashSet<char> { ',', ';', ':' }, .4f }
        };

    private Coroutine typingCoroutine;
     public Coroutine run(string textToType, TMP_Text textLabel) {
        return StartCoroutine(typeText(textToType, textLabel));
    }
     
    public void Stop() {
        StopCoroutine(typingCoroutine);
        isRunning = false;
    }

    private IEnumerator typeText(string textToType, TMP_Text textLabel) {
        isRunning = true;
        
        textLabel.text = string.Empty;
        
        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length) {
            int lastCharIndex = charIndex;
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);
            
            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;
                textLabel.text = textToType.Substring(0, i + 1);
                if (isPunctuation(textToType[i], out float waitTime) && !isLast && !isPunctuation(textToType[i + 1], out _)) {
                    yield return new WaitForSeconds(waitTime);
                }
            }
            yield return null;
        }
        isRunning = false;
        }
    private bool isPunctuation(char character, out float waitTime) {
        foreach (var punctuationCategory in punctuations.Where(punctuationCategory => punctuationCategory.Key.Contains(character))) {
            waitTime = punctuationCategory.Value;
            return true;
        }
        waitTime = default;
        return false;
    }
}
