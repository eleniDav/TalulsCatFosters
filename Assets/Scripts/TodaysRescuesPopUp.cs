using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TodaysRescuesPopUp : MonoBehaviour
{
    [SerializeField] GameObject todaysRescues;

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex > 1)
            StartCoroutine(WaitASec());
    }

    public IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        todaysRescues.transform.parent.gameObject.SetActive(true);
        todaysRescues.SetActive(true);
    }

    public void PopUpAfterIntro()
    {
        todaysRescues.transform.parent.gameObject.SetActive(true);
        todaysRescues.SetActive(true);
    }
}
