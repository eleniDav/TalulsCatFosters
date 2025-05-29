using UnityEngine;

public class ScrollbarReset : MonoBehaviour
{
    //this function is called whenever an object becomes enabled and active - whenever i open/reopen any object with scrollbar -> have content come back at the top
    private void OnEnable()
    {
        transform.position = new Vector2(transform.position.x, 0f);
    }
}
