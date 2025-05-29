using TMPro;
using UnityEngine;

public class CleanUpBarf : MonoBehaviour
{
    //connections to scripts to access the kneel anim and task completion
    [SerializeField] private InventoryManagement inventoryManagement;
    [SerializeField] private TaskController taskController;
    [SerializeField] private TMP_Text hintText;

    private bool canClean;
    private Collider thisBarf;

    private void Update()
    {
        //press C to clean 
        if (Input.GetKeyDown(KeyCode.C) && canClean)
        {
            StartCoroutine(inventoryManagement.TalulKneel());
            if(thisBarf != null)
                Destroy(thisBarf.transform.parent.gameObject);
            canClean = false;

            //if all barfs are cleaned then check off the task
            if (gameObject.transform.childCount == 1)  //1 cause destroy finishes after every render so 0 wont appear right now
            {
                taskController.CompleteTask("Clean Up");
            }
            else
            {
                hintText.text = "The smell hasn't left.. maybe there's more to clean somewhere else in the house..";
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            hintText.text = "Oof this stinks.. Press \"C\" to clean.";
            canClean = true;
            //to get the specific barf talul came in contact with only - get the specific child collider
            thisBarf = collision.GetContact(0).thisCollider;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        hintText.text = "";
        canClean = false;
    }
}
