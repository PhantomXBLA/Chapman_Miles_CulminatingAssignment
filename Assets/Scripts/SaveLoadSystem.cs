using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadSystem : MonoBehaviour
{
    GameObject player;

    public GameObject SaveButton;
    public GameObject LoadButton;
    // Start is called before the first frame update
    void Start()
    {
        SaveButton.GetComponent<Button>().onClick.AddListener(OnSaveButtonPressed);
        LoadButton.GetComponent<Button>().onClick.AddListener(OnLoadButtonPressed);

        if (PlayerPrefs.GetInt("NewGameFlag") == 0)
        {
            OnLoadButtonPressed();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSaveButtonPressed()
    {
        player = GameObject.Find("Noah");
        PlayerPrefs.SetFloat("xPos", player.transform.position.x);
        PlayerPrefs.SetFloat("yPos", player.transform.position.y);
        Debug.Log("the game has been saved");

    }

    void OnLoadButtonPressed()
    {
        player = GameObject.Find("Noah");

        if(PlayerPrefs.HasKey("xPos") && PlayerPrefs.HasKey("yPos")) // do stuff if playerprefs has data to load
        {
            player.transform.position = new Vector2(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"));
            Debug.Log("the game has been loaded");
        }
        else
        {
            Debug.Log("no save game found");
        }
    }
}
