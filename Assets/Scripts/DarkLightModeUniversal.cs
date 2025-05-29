using UnityEngine;

public class DarkLightModeUniversal : MonoBehaviour
{
    //to keep the same color choice through the entire game - main menu, levels, screens blabla
    public static string colorChoice = "dark";
    public static void SetColorChoice(string color)
    {
        colorChoice = color;
    }

    public static string GetColorChoice()
    {
        return colorChoice;
    }
}
