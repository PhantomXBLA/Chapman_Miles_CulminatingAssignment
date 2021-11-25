//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class PanelController : MonoBehaviour
//{
//    //Panels
//    private GameObject dialoguePanel, abilityPanel, fightPanel;



//    //Encounter UI Buttons
//    public GameObject fightButton, bagButton, cassetteButton, runButton;


//    // Start is called before the first frame update
//    void Start()
//    {
//        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

//        foreach (GameObject go in allObjects)
//        {
//            if (go.name == "DialoguePanel")
//                dialoguePanel = go;
//            else if (go.name == "AbilityPanel")
//                abilityPanel = go;
//            else if (go.name == "FightPanel")
//                fightPanel = go;
//            else if (go.name == "FightButton")
//                fightButton = go;


//        }

        
//        fightButton.GetComponent<Button>().onClick.AddListener(OnFightButtonPressed);





//        ChangeState(EncounterSceneState.EnemyEncountered);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }


//    public void OnFightButtonPressed()
//    {
//        ChangeState(EncounterSceneState.ChoseMove);
//    }




//    public void ChangeState(int newState)
//    {
//        dialoguePanel.SetActive(true); //dialogue panel will always be visible
//        abilityPanel.SetActive(false);
//        fightPanel.SetActive(false);

//        if ((newState == EncounterSceneState.EnemyEncountered))
//        {
//            dialoguePanel.SetActive(true);
//        }
//        else if ((newState == EncounterSceneState.ChoseAbility))
//        {
//            //What will Mourntooth do?
//            abilityPanel.SetActive(true);
//        }
//        else if ((newState == EncounterSceneState.ChoseMove))
//        {
//            fightPanel.SetActive(true);
//        }


//    }













//}

//static public class EncounterSceneState
//{
//    public const int EnemyEncountered = 1;
//    public const int ChoseAbility = 2;
//    public const int ChoseMove = 3;

//}
