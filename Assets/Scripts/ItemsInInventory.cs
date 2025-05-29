using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemsInInventory : MonoBehaviour, IPointerClickHandler
//IPointerClickHandler -> interface that allows the script to detect when the pointer clicks on this gameobject
{
    //info of the item that this spot will hold
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;

    //to check if this inventory spot is filled or not
    public bool isFilled;

    //to change the image inside the spot
    [SerializeField] private Image itemImage;

    public TMP_Text hintText;
    public TMP_Text InvName;
    public TMP_Text InvDescription;

    public GameObject selectedHighlight;
    public bool isSelected;
    public GameObject itemBG;

    //reference to the inventory management script -to deactivate previously selected spots etc
    private InventoryManagement inventoryManagement;

    //to force a ui refresh on the description - to show adjusted scrollbar correctly
    public RectTransform contectRectTransform;

    //connection to task controller script so we can call its method when an item is removed from inventory -> complete the right task
    [SerializeField] private TaskController[] taskController;

    [SerializeField] private GameObject trackColorScheme;

    public void Start()
    {
        inventoryManagement = GameObject.Find("Talul").GetComponent<InventoryManagement>();
    }

    //adds the actual item to the inventory spot
    public void AddItem(string itemName, string itemDescription, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        isFilled = true;

        itemImage.gameObject.SetActive(true);
        itemImage.sprite = itemSprite;  //the sprite of the image will be equal to the sprite that was sent from our item
        
        hintText.text = "You've collected something! Press \"R\" to view it in your inventory!";
    }

    //implementing the interface - what to do when i click on this inventory spot (that contains an item of course)
    public void OnPointerClick(PointerEventData eventData)
    {
        //will do smth only with left mouse button click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isFilled)
            {
                //turn off any other previously selected spot
                inventoryManagement.DeselectAllSpots();

                //select the one clicked
                selectedHighlight.SetActive(true);
                itemBG.SetActive(true);
                isSelected = true;
                InvName.text = itemName;
                InvDescription.text = itemDescription;
                //to immediately adjust the scrollbar to the content size
                LayoutRebuilder.ForceRebuildLayoutImmediate(contectRectTransform);

                //keep track of which item is selected in the inventory
                inventoryManagement.itemSelected = itemName;
            }
        }
    }

    public void RemoveItem(string receiver)
    {
        InvName.text = null;
        InvDescription.text = null;
        selectedHighlight.SetActive(false);
        itemBG.SetActive(false);
        isFilled = false;
        isSelected = false;
        itemImage.gameObject.SetActive(false);
        itemImage.sprite = null;

        //and then again force the layout to adjust to the size of the content
        LayoutRebuilder.ForceRebuildLayoutImmediate(contectRectTransform);

        //when item is removed from inventory it also calls this function to adjust the task style -> mark as done
        for (int i = 0; i < taskController.Length; i++)
        {
            if (taskController[i].taskName == itemName && taskController[i].receiver == receiver)
            {
                taskController[i].CompleteTask(itemName);
                break;
            }
        }
        Debug.Log(itemName);
    }
}
