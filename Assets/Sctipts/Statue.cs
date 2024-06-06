using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public GameObject eyes;
    public GameObject floor;
    public AudioClip eyesActivationSound; // Declare an AudioClip for the activation sound

    private AudioSource audioSource; // Declare an AudioSource

    void Start()
    {
        eyes.SetActive(false);
        floor.SetActive(true);

        // Initialize the AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public IEnumerator WaitAndExecute(float time)
    {
        yield return new WaitForSeconds(time);

        // Play the activation sound when the eyes are set active
        if (eyesActivationSound != null)
        {
            audioSource.PlayOneShot(eyesActivationSound);
        }
        else
        {
            Debug.LogWarning("Eyes activation sound is not assigned.");
        }

        eyes.SetActive(true);
        yield return new WaitForSeconds(time);
        floor.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(WaitAndExecute(3f));
    }
}
