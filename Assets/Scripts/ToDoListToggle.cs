using TMPro;
using UnityEngine;

public class ToDoListToggle : MonoBehaviour
{ 
    [SerializeField] private GameObject taskPanel;
    private bool isHidden;

    private void Start()
    {
        isHidden = true;
    }

    public void HideShow()
    {
        if(isHidden)
        {
            //show task panel
            gameObject.GetComponentInChildren<TMP_Text>().text = "▼   TO-DO LIST  ▼";
            taskPanel.SetActive(true);
        }
        else
        {
            //hide task panel
            gameObject.GetComponentInChildren<TMP_Text>().text = "▲   TO-DO LIST  ▲";
            taskPanel.SetActive(false);
        }
        
        isHidden = !isHidden; //change the value for the next time i click
    }
}
