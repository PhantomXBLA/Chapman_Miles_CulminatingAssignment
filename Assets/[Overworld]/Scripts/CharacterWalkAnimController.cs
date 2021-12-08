using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CardinalDirection
{
    North   = 0,
    East    = 1,
    South   = 2,
    West    = 3
}

public class CharacterWalkAnimController : MonoBehaviour
{

    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private CardinalDirection facing = CardinalDirection.South;

    [SerializeField]
    private Rigidbody2D rigidbody;

    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If the character is mostly walking up, set direction to up
        //if not walking, set is walking to false
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");


        Vector2 velocity = new Vector2(inputX, inputY);

        bool isWalking = velocity.sqrMagnitude > float.Epsilon;
        bool isMovingHorizontally = Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y);

        if(isMovingHorizontally)
        {
            if (velocity.x < 0)
            {
                facing = CardinalDirection.West;
            }
            else
            {
                facing = CardinalDirection.East;
            }

            isMoving = true;
        }
        else
        {
            if (velocity.y < 0)
            {
                facing = CardinalDirection.South;
            }
            else
            {
                facing = CardinalDirection.North;
            }
            isMoving = true;
        }

        if ((velocity.y == 0) && (velocity.x == 0))
        {
            isMoving = false;
        }


        animator.SetBool("isWalking", isWalking);
        animator.SetInteger("walkDirection", (int)facing); //walk up
    }



    private void OnTriggerStay2D(Collider2D other)
    {
        //Player cannot encounter something unless that are moving
        if ((other.tag == "Tall Grass") && (isMoving == true))
        {
            //A 1/100 chance to encounter something if the player stays in the Tall Grass
            //for any amount of time.
            if (Random.Range(1, 101) <= 1)
            {
                Debug.Log("A wild scendo appeared");

                PlayerPrefs.SetInt("EncounterCheck", 0); // 0 = wild encounter, 1 = trainer encounter


                if (Random.Range(1, 11) <= 1)
                {
                    PlayerPrefs.SetString("RandomEncounter", "Parchpaw");
                }
                else if (Random.Range(1, 11) >= 2)
                {
                    PlayerPrefs.SetString("RandomEncounter", "Dampurr");
                }
                Debug.Log(PlayerPrefs.GetString("RandomEncounter"));
                SceneManager.LoadScene("EncounterScene");
            }
        }
    }


    
}
