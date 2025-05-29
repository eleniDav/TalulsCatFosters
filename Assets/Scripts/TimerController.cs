using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text time;
    private Image timerBar;

    private float timeLeft;
    private float timeLimit;

    //references to level results script
    [SerializeField] LevelResults levelResults;

    private void Start()
    {
        timerBar = gameObject.GetComponent<Image>();
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                timeLimit = 600; //L1 - 10mins
                break;
            case 2:
                timeLimit = 720; //L2 - 12mins
                break;
            case 3:
                timeLimit = 900; //L3 - 15mins
                break;
        }
        timeLeft = timeLimit; 
    }

    void Update()
    {
        //time passes along with deltatime => time passed between frames (in seconds)
        timeLeft -= Time.deltaTime;

        //if theres still time left
        if (timeLeft >= 0)
        { 
            //change the text of the timer and the format to 00:00
            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);
            time.text = string.Format("{0:00}:{1:00}", minutes, seconds); //0 & 1 => placeholders, 00 => the format i want -2digits 0 if no num-

            //and also change the widget fill bar - emptying out
            timerBar.fillAmount = (timeLeft/timeLimit);
        }
        else
        {
            time.fontStyle = FontStyles.Bold;
            time.color = Color.red;
            //time run out -- loose level
            levelResults.ShowResults("loose");
            //deactivate script to stop countiing internally and to also just call the function above once and hear the result audio normally
            enabled = false;
        }
    }
}
