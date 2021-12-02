using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    public bool inRangeOfNPC = false;
    public GameObject NPC;

    public GameObject overworldNPC1;
    public GameObject overworldNPC2;
    public GameObject overworldNPC3;
    public GameObject overworldNPC4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInput();
    }

    void playerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inRangeOfNPC == true && NPC != null)
            {
                NPC.gameObject.GetComponent<AIOverworld>().SayMessage();
            }
        }
    }
}
