using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TaskController : MonoBehaviour
{
    //item removed from inventory -> change the corresponding task panel(check & strikethrough)

    //item names will be the exact same as the task names
    public string taskName;
    public string receiver;

    public bool taskCompleted;

    //to change the task panel children 
    [SerializeField] private TMP_Text taskInfo;
    [SerializeField] private Image taskCheck;

    [SerializeField] private TMP_Text hintText;

    //to update the point widget with every task completion
    [SerializeField] private Image pointsProgress;
    [SerializeField] private TMP_Text scorePercentage;
    private float targetPoints = 100;
    private float pointsToAdd;
    [SerializeField] GameObject numOfTasks;

    //references to level results script
    [SerializeField] private LevelResults levelResults;

    public void CompleteTask(string itemRemoved)
    {
        //points to add after every task completion
        pointsToAdd = targetPoints / numOfTasks.transform.childCount;

        if (itemRemoved == taskName)
        {
            taskCheck.gameObject.SetActive(true);
            taskInfo.fontStyle = FontStyles.Strikethrough | FontStyles.Bold;
            taskInfo.color = new Color(0, 0, 0, 1);
            hintText.text = "You completed one task! Click the TO-DO LIST button to see the rest of the tasks!";
            taskCompleted = true;

            //update the points widget -- max=1
            pointsProgress.fillAmount += (pointsToAdd / targetPoints);
            scorePercentage.text = Math.Round(pointsProgress.fillAmount * targetPoints) + "%";

            TrackResults();
        }
    }

    //gets called whenever a new task is completed
    public void TrackResults()
    {
        //if all tasks are completed -- win level
        if (Math.Round(pointsProgress.fillAmount * targetPoints) >= 100)
        {
            scorePercentage.text = "DONE!";
            levelResults.ShowResults("win");
        }
    }
}
