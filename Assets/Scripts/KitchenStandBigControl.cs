using UnityEngine;
using TMPro;

public class KitchenStandController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    //to alter ui text and let the user know what to do next - hint text i guess you could call it
    public TMP_Text hintText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            anim.Play("KS_Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hintText.text = "";
            anim.Play("KS_Close");
        }
    }
}
