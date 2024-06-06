using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera.gameObject.SetActive(true);
        secondaryCamera.gameObject.SetActive(false);
    }
    public void SetMainCameraFalse()
    {
        mainCamera.gameObject.SetActive(true);
        secondaryCamera.gameObject.SetActive(true);
    }

    public void SetSecondaryCameraFalse()
    {
        mainCamera.gameObject.SetActive(false);
        secondaryCamera.gameObject.SetActive(false);
    }
}
