using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class CatController : MonoBehaviour
{
    //to control which audio will play along with each animation (different cats should produce audios at different times so you can hear both of the audios btw)

    //to play sound along with animation i need an audiosource 
    AudioSource catAudioSource;

    //and my different audio clips
    public AudioClip catPurr;
    public AudioClip catWhine;
    public AudioClip catHappy;

    private Animator anim;

    private bool canGive;

    //to hint stuff for player
    public TMP_Text hintText;

    //to remove the item from the inventory .. and give it to the cat (if she wants it) 
    [SerializeField] private InventoryManagement inventoryManagement; //connection to inventory managemenet script -> on talul 

    //to track all items that need to be given to the cat (using a list so i can easily remove them when they are completed)
    [SerializeField] private List<string> catItems;

    private bool catWants;

    //to do smth for a specific cat and not for all or whatever
    private string catName;

    //to change the cinemachine camera from the Talul follow one to the cat close up one
    [SerializeField] private CinemachineCamera mainCam;
    [SerializeField] private CinemachineCamera catCam;

    void Start()
    {
        catAudioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        catName = gameObject.name.ToString();

        CatPurr();
        //so different cats will start whining at different times
        int randomWaitTime = Random.Range(10, 15);
        StartCoroutine(WaitToWhine(randomWaitTime));
    }

    void Update()
    {
        //press Space to give smth -only if the cat ACTUALLY NEEDS this item.. and complete a task- 
        if (Input.GetKeyDown(KeyCode.Space) && canGive)
        {
            if (inventoryManagement.itemSelected != "")
            {
                catWants = false;
                for (int i = 0; i < catItems.Count; i++)
                {
                    if (catItems[i] == inventoryManagement.itemSelected)
                    {
                        inventoryManagement.GiveItem(catName); //to get the cat's name and give it to her specifically
                        //only if you actually have smth selected to give to the cat
                        if (inventoryManagement.gaveSuccess)
                        {
                            //remove this item from her list so she wont have like 2 waters -> 2 different tasks will get checked (bad)
                            catItems.Remove(catItems[i]);
                            //play talul animation & the correct cat animation based on the remaining items
                            StartCoroutine(inventoryManagement.TalulKneel());
                            CatHappy();
                            StartCoroutine(WaitToWhine(10));
                        }
                        catWants = true;
                        break;
                    }
                }
                if (!catWants)
                {
                    hintText.text = "Hmm.. seems like " + catName + " doesn't want this item..";
                }
            }
            else
            {
                hintText.text = "Select an item from the inventory to give to " + catName + ".";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hintText.text = "This is " + catName + "!";
            canGive = true;
            mainCam.gameObject.SetActive(false);
            catCam.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hintText.text = "";
            canGive = false;
            catCam.gameObject.SetActive(false);
            mainCam.gameObject.SetActive(true);
        }
    }

    private void CatWhine()
    {
        //WHINE WHEN SHE NEEDS STUFF - WHEN THERES TASKS FOR HER TO BE DONE
        anim.SetTrigger("need");
        catAudioSource.Stop();
        catAudioSource.loop = true;
        catAudioSource.clip = catWhine;
        catAudioSource.Play();
    }

    private void CatHappy()
    {
        //happy when item is served to her - task completion
        anim.SetTrigger("thank");
        catAudioSource.Stop();
        catAudioSource.loop = true;
        catAudioSource.clip = catHappy;
        catAudioSource.Play();
    }

    private void CatPurr()
    {
        //purr at the beginning and after all tasks are complete - empty list of items she wants
        anim.SetTrigger("complete");
        catAudioSource.Stop();
        catAudioSource.loop = true;
        catAudioSource.clip = catPurr;
        catAudioSource.Play();
    }

    private IEnumerator WaitToWhine(int seconds)
    {
        yield return new WaitForSeconds(seconds); //time to play the previous animation before moving on to the next one
        //if all the tasks for this cat are complete she rests purring (when all are purring -> end of game)
        if (catItems.Count == 0)
        {
            CatPurr();
        }
        //cat still needs smth
        else
        {
            CatWhine();
            yield return new WaitForSeconds(5f);
        }
    }
}
