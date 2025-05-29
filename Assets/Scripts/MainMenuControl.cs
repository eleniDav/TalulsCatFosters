using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private int currentScene;

    //make cats shut up when setting are open and game gets paused
    [SerializeField] private AudioSource[] catSounds;

    //to play animation of fade in
    [SerializeField] private Animator transition;

    private void Start()
    {
        //which scene is loaded right now
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void StartGame()
    {
        StartCoroutine(FadeIn(1));
    }

    //close application
    public void ExitGame()
    {
        //quits the player application - shuts down the running application (btw editor ignores this - make build to try)
        Application.Quit();
    }

    //go back to main menu (from gameplay)
    public void ReturnToMainMenu()
    {
        StartCoroutine(FadeIn(0));
    }

    //load next level - next level option
    public void LoadNextLevel()
    {
        StartCoroutine(FadeIn(currentScene + 1));
    }

    //reload the level - try again option
    public void ReloadLevel()
    {
        StartCoroutine(FadeIn(currentScene));
    }

    //to pause game when settings are opened and upause it when theyre closed again
    public void PauseGame()
    {
        Time.timeScale = 0f;
        foreach (AudioSource cat in catSounds)
        {
            cat.Pause();
        }
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        foreach (AudioSource cat in catSounds)
        {
            cat.UnPause();
        }
    }

    //to have the fade in effect whenever a new scene will be loaded (from transparent to black sceen)
    //fade out (black to transparent) happens right when a scene is loaded - default transition
    public IEnumerator FadeIn(int index)
    {
        //unpause game -cause all scene transitions are fired when game is paused and we need scaled time for smooth in and out transitions
        Time.timeScale = 1f;
        transition.SetTrigger("fadeIn");
        yield return new WaitForSeconds(1);
        //loads the Scene in the background as the current Scene runs (and animation is playing) - asynchronoysly
        SceneManager.LoadSceneAsync(index);
    }
}
