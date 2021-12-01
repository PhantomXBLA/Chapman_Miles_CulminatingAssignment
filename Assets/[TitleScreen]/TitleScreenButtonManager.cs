using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenButtonManager : MonoBehaviour
{
    GameObject NewGameButton;
    GameObject ContinueButton;
    GameObject OptionsButton;
    GameObject HelpButton;
    GameObject CreditsButton;
    GameObject QuitButton;

    public AudioSource CassetteButton;
    public AudioSource CassetteStart;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allButtons = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allButtons)
        {
            if (go.name == "NewGameButton")
            {
                NewGameButton = go;
            }

            else if (go.name == "ContinueButton")
            {
                ContinueButton = go;
            }


            else if (go.name == "OptionsButton")
            {
                OptionsButton = go;
            }

            else if (go.name == "HelpButton")
            {
                HelpButton = go;
            }


            else if (go.name == "CreditsButton")
            {
                CreditsButton = go;
            }

            else if (go.name == "QuitButton")
            {
                QuitButton = go;
            }
        }

        NewGameButton.GetComponent<Button>().onClick.AddListener(NewGameButtonPressed);
        ContinueButton.GetComponent<Button>().onClick.AddListener(ContinueButtonPressed);
        OptionsButton.GetComponent<Button>().onClick.AddListener(OptionsButtonPressed);
        HelpButton.GetComponent<Button>().onClick.AddListener(HelpButtonPressed);
        CreditsButton.GetComponent<Button>().onClick.AddListener(CreditsButtonPressed);
        QuitButton.GetComponent<Button>().onClick.AddListener(QuitButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewGameButtonPressed()
    {
        Debug.Log("1");
        CassetteButton.Play();
        StartCoroutine(WaitToPlayOtherSFX());
        StartCoroutine(WaitToStartGame());
    }

    void ContinueButtonPressed()
    {
        Debug.Log("2");
        CassetteButton.Play();
        StartCoroutine(WaitToPlayOtherSFX());
        StartCoroutine(WaitToLoadGame());

    }

    void OptionsButtonPressed()
    {
        Debug.Log("3");
        CassetteButton.Play();

    }

    void HelpButtonPressed()
    {
        Debug.Log("4");
        CassetteButton.Play();

    }

    void CreditsButtonPressed()
    {
        Debug.Log("5");
        CassetteButton.Play();

    }

    void QuitButtonPressed()
    {
        Debug.Log("6");
        CassetteButton.Play();

    }

    IEnumerator WaitToPlayOtherSFX()
    {
        yield return new WaitForSeconds(.4f);
        CassetteStart.Play();
    }

    IEnumerator WaitToStartGame()
    {
        PlayerPrefs.SetInt("NewGameFlag", 1);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);

    }

    IEnumerator WaitToLoadGame()
    {

        if (PlayerPrefs.HasKey("xPos") && PlayerPrefs.HasKey("yPos")) // do stuff if playerprefs has data to load
        {
            PlayerPrefs.SetInt("NewGameFlag", 0);
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("no saved data found!");
        }


    }
}
