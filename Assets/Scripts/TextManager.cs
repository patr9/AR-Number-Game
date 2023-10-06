using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TextManager : MonoBehaviour
{
    //Variables
    private List<string> permNumbers = new List<string> { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
    private List<string> numbers = new List<string>();
    private int index;
    private int temp;
    private bool check = false;
    private GameObject tempObject;
    private GameObject[] tempObjects;
    private List<int> bNumbers = new List<int>();

    public TMP_Text text;
    public Button nextButton;
    public Button exitButton;
    public GameObject camera;
    public GameObject[] objects;
    public AudioSource audioSayNumber;

    System.Random rand = new System.Random();


    //Methods
    void Start()
    {
        nextButton.onClick.AddListener(() => NextClicked());
        PlayerPrefs.SetString("Text", "");
        LoadNumber();
    }

    void Update()
    {
        //Constantly updating the text and checking when to run certain functions
        if (numbers.Count <= 0)
        {
            Debug.Log("Game Over");
        }
        if (PlayerPrefs.GetInt("Next") == 1)
        {
            NextLevel();
        }
        text.text = PlayerPrefs.GetString("Text");
    }

    void LoadNumber()
    {
        //If numbers array is less than permNumbers, that means numbers are still up for play
        if (numbers.Count < permNumbers.Count && check == false)
        {
            //While loop checks for any numbers that have not been played
            index = rand.Next(0, permNumbers.Count);
            while (check == false)
            {
                if (numbers.Contains(permNumbers[index]))
                {
                    index = rand.Next(0, permNumbers.Count);
                }
                else
                {
                    check = true;
                }
            }
            //Changes the text and current number value for this round
            PlayerPrefs.SetString("cValue", (index + 1).ToString());
            PlayerPrefs.SetString("Text", permNumbers[index]);
            nextButton.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            audioSayNumber.Play();
            numbers.Add(permNumbers[index]);
            check = true;
        }
        //If all numbers have been played, the game ends
        else if (numbers.Count >= permNumbers.Count)
        {
            EndGame();
        }
    }

    void OptionsGeneration()
    {
        //Generates array of random size 3-5 alongside the correct answer (actual size of 4-6)
        int size = rand.Next(3, 5);
        bNumbers.Clear();
        bNumbers.Add(index);
        for (int i = 0; i < size; i++)
        {
            temp = rand.Next(0, 9);
            if (temp != index && (bNumbers.Contains(temp) == false))
            {
                bNumbers.Add(temp);
            }
            else
            {
                i--;
            }
        }
        //Instantiates each object according to the randomly generated array, with random colours and positions
        foreach (int i in bNumbers)
        {
            tempObject = Instantiate(objects[i], new Vector3(rand.Next(-50, 50) / 100f, rand.Next(50) / 100f, rand.Next(50) / 100f), Quaternion.Euler(0, camera.transform.rotation.y - 180, 0));
            Color randomColor = new Color(rand.Next(0, 256) / 256f, rand.Next(0, 256) / 256f, rand.Next(0, 256) / 256f);
            tempObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", randomColor);
        }

        PlayerPrefs.SetString("Text", "Press the correct number");
    }

    public void NextLevel()
    {
        //When the correct number is picked, it resets the AR field to generate new numbers for the next round
        PlayerPrefs.SetInt("Next", 0);
        foreach (int i in bNumbers)
        {
            tempObjects = GameObject.FindGameObjectsWithTag("Number");
            foreach (GameObject obj in tempObjects)
            {
                Destroy(obj);
            }
        }
        check = false;
        //Pauses on "Congratulations" to keep a timely manner 
        Invoke("LoadNumber", 3);
    }

    void EndGame()
    {
        PlayerPrefs.SetString("Text", "Hooray! You matched all of the numbers correctly!");
        text.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }

    //Button methods
    //Function that occurs when "Next" button is pressed
    public void NextClicked()
    {
        nextButton.gameObject.SetActive(false);
        OptionsGeneration();
    }
}
