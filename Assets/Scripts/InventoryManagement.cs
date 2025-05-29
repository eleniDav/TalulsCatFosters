using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class InventoryManagement : MonoBehaviour
{
    //this is the middle of the communication between each collected item and the spots in the inventory
    /*
        when an item is collected the item info will be sent to the inventorymanagement and then the inventorymanagement
        will sent the info to the item spots in the inventory widget .. sorta way
    */

    public GameObject inventory;
    private bool isActive = false;

    //to keep a reference of the entire gameobject of each item - so i can then destroy it = collect it successfully
    private GameObject item;

    //rigidbody of main character
    private Rigidbody rigidB;

    //to play animations
    private Animator anim;

    //to hint stuff for player
    public TMP_Text hintText;

    //to pass the item data, multiple inventory spots so i need array (i want 4 spots btw)
    public ItemsInInventory[] itemsInInventory;

    //to see if method has been executed
    public bool gaveSuccess;
    
    //track which item from the inventory is selected (to check if the pet actually wants this item)
    public string itemSelected;

    //to check the case when the 4 spots are already filled 
    public bool inventorySpotAvailable;

    void Start()
    {
        rigidB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        inventory.SetActive(isActive);
    }

    void Update()
    {
        //to open inventory on press "r"
        if (Input.GetKeyDown(KeyCode.R))
        {
            DeselectAllSpots();
            Debug.Log("youre viewing the inventory");
            if (!isActive)
            {
                inventory.SetActive(true);
                isActive = true; 
            }
            else
            {
                inventory.SetActive(false);
                isActive = false;
            }
        }
    }

    //to add item in the inventory
    public void AddItem(string itemName, string itemDescription, Sprite itemSprite,GameObject gmobj)
    {
        //need this to destroy the item from the scene after collection
        item = gmobj;
        Debug.Log(item);

        //to access the inventory spots
        for(int i=0; i < itemsInInventory.Length; i++)
        {
            if (!itemsInInventory[i].isFilled)
            {
                itemsInInventory[i].AddItem(itemName, itemDescription, itemSprite);
                inventorySpotAvailable = true;
                break; //to stop the loop after addition to inventory spot
            }
            else
            {
                hintText.text = "Inventory is full! Complete a task to empty out space for new items!";
                inventorySpotAvailable = false;
            }
        }
    }

    public void GiveItem(string receiver)
    {
        //to give the item that is currently selected
        for (int i = 0; i < itemsInInventory.Length; i++)
        {
            if (itemsInInventory[i].isSelected)
            {
                //prepei na kaneis edw elegxo an to sugkekrimeno gati to thelei auto pou einai selected !!!!
                itemsInInventory[i].RemoveItem(receiver);
                itemSelected = "";
                gaveSuccess = true;
                break; //to stop the loop after removing the selected item
            }
            else
            {
                hintText.text = "Select an item and Press Space to complete a task.";
                gaveSuccess = false;
            }
        }
    }

    public IEnumerator TalulCollect()
    {
        //play the pick up sound effect
        gameObject.GetComponent<AudioSource>().Play();
        //freeze all rotations and movements so the animation can play and player wont be able to move
        rigidB.constraints = RigidbodyConstraints.FreezeAll;
        anim.Play("collect");
        yield return new WaitForSeconds(2f);
        //default => no rotations or y movement
        rigidB.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        //remove item from scene -> its sprite will be added to the inventory widget now
        Destroy(item);
    }

    public IEnumerator TalulKneel()
    {
        gameObject.GetComponent<AudioSource>().Play();
        rigidB.constraints = RigidbodyConstraints.FreezeAll;
        anim.Play("kneel");
        yield return new WaitForSeconds(2f);
        rigidB.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    //to deactivate previously clicked spots when new click occurs - and also to deactivate and erase description when inventory closes and reopens
    public void DeselectAllSpots()
    {
        for(int i=0; i < itemsInInventory.Length; i++)
        {
            itemsInInventory[i].selectedHighlight.SetActive(false);
            itemsInInventory[i].itemBG.SetActive(false);
            itemsInInventory[i].isSelected = false;
            itemsInInventory[i].InvName.text = null;
            itemsInInventory[i].InvDescription.text = null;
        }
    }
}
