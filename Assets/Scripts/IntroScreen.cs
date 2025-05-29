using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreen : MonoBehaviour
{
    //show intro screen after few seconds of gameplay
    [SerializeField] private GameObject introScreen;

    private void Start()
    {
        //intro displayed on the first level
        if(SceneManager.GetActiveScene().buildIndex == 1)
            StartCoroutine(WaitASec());
    }

    public IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        introScreen.transform.parent.gameObject.SetActive(true);
        introScreen.SetActive(true);
    }
}
