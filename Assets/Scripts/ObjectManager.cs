using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //This class helps identify if the correct number is pressed
    private void OnMouseDown()
    {
        string value = PlayerPrefs.GetString("cValue");

        if (transform.root.name.Contains(value))
        {
            PlayerPrefs.SetString("Text", "Congratulations");
            PlayerPrefs.SetInt("Next", 1); //1 is true, 0 is false (helps identify when to reset the playing field)
            Destroy(gameObject);
        }
        else
        {
            PlayerPrefs.SetString("Text", "Are you sure? Try again.");
        }
    }
}

