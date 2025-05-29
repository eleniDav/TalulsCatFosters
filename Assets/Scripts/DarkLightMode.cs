using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DarkLightMode : MonoBehaviour
{
    private float alphaKey;

    //to keep ALL of the gameobjects under this gameobject in a list and iterate through it to modify the colors
    private List<Transform> childrenList;

    private Color lightColor;
    private Color darkColor;

    [SerializeField] private GameObject[] inactiveOnes;

    private string colorChoice;
    private void Start()
    {
        childrenList = new List<Transform>();
        lightColor = new Color(0.9254902f, 0.8156863f, 0.8588235f);
        darkColor = new Color(0.2980392f, 0.05098039f, 0.1607843f);

        //grabs all the active ui gameobjects and adds them to the list 
        foreach (Transform canvaChild in gameObject.transform.GetComponentsInChildren<Transform>())
        {
            childrenList.Add(canvaChild);
        }

        //to also be able to change the color on the inactive gameobjects
        foreach (GameObject inactive in inactiveOnes)
        {
            //and for their children as well
            foreach(Transform inactiveChild in inactive.transform.GetComponentsInChildren<Transform>())
            {
                childrenList.Add(inactiveChild);
            }
        }
        
        //the universal color theme that has been chosen - to keep it for the rest of the scenes
        LoadTheRightTheme(DarkLightModeUniversal.GetColorChoice());
    }
    public void DarkToLight()
    {
        foreach (Transform child in childrenList)
        {
            //works for images
            if (child.GetComponent<Image>())
            {
                //keep the same transperancy and just change the color to the light one
                alphaKey = child.GetComponent<Image>().color.a;
                if (child.GetComponent<Image>().color == new Color(0.9254902f, 0.8156863f, 0.8588235f, alphaKey))
                {
                    child.GetComponent<Image>().color = new Color(0.2980392f, 0.05098039f, 0.1607843f, alphaKey);
                }
                else if (child.GetComponent<Image>().color == new Color(0.2980392f, 0.05098039f, 0.1607843f, alphaKey))
                {
                    child.GetComponent<Image>().color = new Color(0.9254902f, 0.8156863f, 0.8588235f, alphaKey);
                }
            }
            //works for text mesh pro
            else if (child.GetComponent<TextMeshProUGUI>())
            {
                if (child.GetComponent<TextMeshProUGUI>().color == lightColor)
                {
                    child.GetComponent<TextMeshProUGUI>().color = darkColor;
                }
                else if (child.GetComponent<TextMeshProUGUI>().color == darkColor)
                {
                    child.GetComponent<TextMeshProUGUI>().color = lightColor;
                }
            }
        }
    }
    public void LightToDark()
    {
        foreach (Transform child in childrenList)
        {
            //works for images
            if (child.GetComponent<Image>())
            {
                //keep the same transperancy and just change the color to the dark one
                alphaKey = child.GetComponent<Image>().color.a;
                if (child.GetComponent<Image>().color == new Color(0.2980392f, 0.05098039f, 0.1607843f, alphaKey))
                {
                    child.GetComponent<Image>().color = new Color(0.9254902f, 0.8156863f, 0.8588235f, alphaKey);
                }
                else if (child.GetComponent<Image>().color == new Color(0.9254902f, 0.8156863f, 0.8588235f, alphaKey))
                {
                    child.GetComponent<Image>().color = new Color(0.2980392f, 0.05098039f, 0.1607843f, alphaKey);
                }
            }
            //works for text mesh pro
            else if (child.GetComponent<TextMeshProUGUI>())
            {
                if (child.GetComponent<TextMeshProUGUI>().color == darkColor)
                {
                    child.GetComponent<TextMeshProUGUI>().color = lightColor;
                }
                else if (child.GetComponent<TextMeshProUGUI>().color == lightColor)
                {
                    child.GetComponent<TextMeshProUGUI>().color = darkColor;
                }
            }
        }
    }
    public void LoadTheRightTheme(string theme)
    {
        if (theme == "dark")
        {
            //loads the default - dont do anything
        }
        else if (theme == "light")
        {
            DarkToLight();
            DarkLightModeUniversal.SetColorChoice("light");
        }
    }
    public void ReplaceColors(string mode)
    {
        colorChoice = DarkLightModeUniversal.GetColorChoice();

        //if you click the light mode button and the current theme is dark
        if (mode == "light" && colorChoice == "dark")
        {
            DarkToLight();
            //also when everything is changed set the theme to the current one so it will behave right on the next button click
            DarkLightModeUniversal.SetColorChoice("light");
        }
        //if you click the dark mode button and the current theme is light
        else if (mode == "dark" && colorChoice == "light")
        {
            LightToDark();
            DarkLightModeUniversal.SetColorChoice("dark");
        }           
    }
}
