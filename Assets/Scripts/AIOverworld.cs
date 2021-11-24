using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOverworld : MonoBehaviour
{
    public int DialougeIndex = 0;
    public string[] Dialouge = { "The dialouge for this character has not been set." , "This is a test dialouge p1"};
    public string name = "Placeholder";

    GameObject border;
    TMPro.TextMeshProUGUI messageInstantiate;

    public Canvas canvas;


    [SerializeField]
    TMPro.TextMeshProUGUI messagePrefab;
    public GameObject borderPrefab;

    [SerializeField]
    private GameObject abilityPanel;

    [SerializeField]
    float timeBetweenCharacters = 0.0001f;

    private IEnumerator animateTextCoroutineRef = null;

    bool canInstantiate = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator AnimateTextCoroutine(string message)
    {
        //disable ability panel
        //abilityPanel.SetActive(false);

        messageInstantiate.text = "";
        for (int currentCharater = 0; currentCharater < message.Length; currentCharater++)
        {
            messageInstantiate.text += message[currentCharater];
            yield return new WaitForSecondsRealtime(timeBetweenCharacters);
        }

        //Enable ability panel
        //abilityPanel.SetActive(true);
        animateTextCoroutineRef = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Noah")
        {
            Debug.Log(collision.gameObject.name);
            SayMessage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Noah")
        {
            Debug.Log("no");
            cleanMessage();
        }
    }

    void SayMessage()
    {
        if (canInstantiate == true)
        {
            border = Instantiate(borderPrefab, canvas.transform);
            messageInstantiate = Instantiate(messagePrefab, canvas.transform);
            animateTextCoroutineRef = AnimateTextCoroutine(Dialouge[DialougeIndex]);
            //Animate some text to say what you encountered
            StartCoroutine(animateTextCoroutineRef);

            //StopCoroutine((animateTextCoroutineRef));
        }

    }

    void cleanMessage()
    {
        canInstantiate = false;
        Destroy(border);
        Destroy(messageInstantiate);
        canInstantiate = true;
    }
}
