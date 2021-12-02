using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }


        animator.SetBool("isWalking", isWalking);
        animator.SetInteger("walkDirection", (int)facing); //walk up
    }
}
