using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelResults : MonoBehaviour
{
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject loose;
    [SerializeField] private GameObject final;

    [SerializeField] private AudioClip winAudio;
    [SerializeField] private AudioClip looseAudio;
    [SerializeField] private AudioClip finalAudio;

    //stop the bg music and play one of the clips
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource[] catSounds;

    public void ShowResults(string result)
    {
        //pause game
        Time.timeScale = 0f;

        gameObject.SetActive(true);
        bgMusic.Stop();

        StartCoroutine(WaitForCatsToShutUp());

        bgMusic.loop = false;
        bgMusic.playOnAwake = false;

        if (result == "win")
        {
            if(SceneManager.GetActiveScene().buildIndex < 3) //win level -> win level scene
            {
                win.SetActive(true);
                bgMusic.clip = winAudio;
                bgMusic.Play();
            }
            else if(SceneManager.GetActiveScene().buildIndex == 3) //last level won -> win game screen
            {
                final.SetActive(true);
                bgMusic.clip = finalAudio;
                bgMusic.Play();
            }
        }
        else if (result == "loose")
        {
            loose.SetActive(true);
            bgMusic.clip = looseAudio;
            bgMusic.Play();
        }
    }

    public IEnumerator WaitForCatsToShutUp()
    {
        yield return new WaitForEndOfFrame(); //so this will be the last step - otherwise they stop and then get played right away from catcontroller functions
        foreach (AudioSource cat in catSounds)
        {
            cat.Stop();
        }
    }
}
