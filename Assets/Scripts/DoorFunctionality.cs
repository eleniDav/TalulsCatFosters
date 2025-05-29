using UnityEngine;

public class DoorFunctionality : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.Play("DoorOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.Play("DoorClose");
        }
    }
}
