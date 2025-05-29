using UnityEngine;

public class NegativeConfirmationControl : MonoBehaviour
{
    [SerializeField] private GameObject confirmation;
    [SerializeField] private AudioSource buttonClick;
    [SerializeField] private GameObject screens;
    [SerializeField] private MainMenuScript mainMenuScript;

    public void ContinuePlaying()
    { 
        //if confirmation pops up when a screen is displayed
        if (screens.activeSelf)
        {
            confirmation.SetActive(false);
            buttonClick.Play();
        }
        //else if confirmation pops up through settings
        else
        {
            confirmation.SetActive(false);
            buttonClick.Play();
            mainMenuScript.UnPauseGame();
        }
    }
}
