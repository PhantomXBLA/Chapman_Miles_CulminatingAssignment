using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will prompt the user what to do, and enable/disable UI elements at different times
public class EncounterUI : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI encounterText;

    [SerializeField]
    private GameObject abilityPanel;

    [SerializeField]
    float timeBetweenCharacters = 0.0001f;


    //This is a reference to keep track of our coroutine
    private IEnumerator animateTextCoroutineRef = null;
    
    // Start is called before the first frame update
    void Start()
    {
        animateTextCoroutineRef = AnimateTextCoroutine("You have encountered an opponent!");
        //Animate some text to say what you encountered
        StartCoroutine(animateTextCoroutineRef);

        //StopCoroutine((animateTextCoroutineRef));
    }

    IEnumerator AnimateTextCoroutine(string message)
    {
        //disable ability panel
        abilityPanel.SetActive(false);

        encounterText.text = "";
        for (int currentCharater = 0; currentCharater < message.Length; currentCharater++)
        {
            encounterText.text += message[currentCharater];
            yield return new WaitForSeconds(timeBetweenCharacters);
        }

        //Enable ability panel
        abilityPanel.SetActive(true);
        animateTextCoroutineRef = null;
    }


}
