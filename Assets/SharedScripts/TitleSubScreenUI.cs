using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSubScreenUI : MonoBehaviour
{
    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
