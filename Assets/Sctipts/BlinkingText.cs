using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    public Text textComponent;

    public float blinkSpeed = 0.5f;

    void Start()
    {
        blinkSpeed = 0.5f;

        if (textComponent != null)
        {
            StartCoroutine(BlinkText());
        }
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 1);
            yield return new WaitForSeconds(blinkSpeed);

            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
