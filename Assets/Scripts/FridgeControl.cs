using UnityEngine;

public class FridgeControl : MonoBehaviour
{

    [SerializeField] private Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("open the noor");
            anim.Play("F_Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("close the noor");
            anim.Play("F_Close");
        }
    }
}
