using UnityEngine;
using TMPro;

public class ItemsToCollect : MonoBehaviour
{
    //info to pass down to the inventory management script so it knows what to send to the items in inventory script -differentiates the items obvi-
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private Sprite itemSprite; //sprites (2D images) of the items to show them in the widget
    private GameObject thisGameObject;

    //to pass the item data 
    private InventoryManagement inventoryManagement;

    public TMP_Text hintText;

    //to collect items ONLY when talul collides with them
    private bool canCollect;

    void Start()
    {
        //i want the item to be destroyed AFTER talul c press handling (collect animation etc) so ill pass a reference to this whole gameobject (item) in the add method etc
        thisGameObject = gameObject;

        //connection to inventory managemenet script -> on talul 
        inventoryManagement = GameObject.Find("Talul").GetComponent<InventoryManagement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if talul collides with any of these items they will be added to the inventory
        if (collision.gameObject.CompareTag("Player"))
        {   
            hintText.text = "You found something! Press \"C\" to collect it!";
            canCollect = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintText.text = "";
            canCollect = false;
        }
    }

    private void Update()
    {
        //press C to collect smth -only when prompt of course- and hold it in the inventory
        if (Input.GetKeyDown(KeyCode.C) && canCollect)
        {
            canCollect = false;
            inventoryManagement.AddItem(itemName, itemDescription, itemSprite,thisGameObject);
            if (inventoryManagement.inventorySpotAvailable)
            {
                StartCoroutine(inventoryManagement.TalulCollect());
            }
        }
    }
}
