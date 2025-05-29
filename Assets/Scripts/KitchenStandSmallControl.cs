using UnityEngine;

public class KitchenStandControl2 : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("open kitchen stand 2");
            anim.Play("KSD_Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("close kitchen stand 2");
            anim.Play("KSD_Close");
        }
    }
}
