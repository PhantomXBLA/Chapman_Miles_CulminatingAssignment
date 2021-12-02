using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private Rigidbody2D rigidbody = null;

    public static bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        PlayerInput();
    }

    void PlayerInput()
    {
        if (isMoving == true)
        {
            rigidbody.velocity = new Vector2(0, 0);
        }

        if (isMoving == false)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            rigidbody.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
            //isMoving = true;
        }
    }



    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.tag == "Tall Grass") && (moveSpeed < 1.5f)) //try new standingStill bool
        {
            //A 1/1000 chance to encounter something if the player stays in the Tall Grass
            //for any amount of time.
            if (Random.Range(1, 101) <= 10)
            {
                Debug.Log("A wild something something appeared");
                //Freeze the players movement until battle state is false.
                //Play encounter animation and music
                //Need to trigger battle state
                //SceneManager.LoadScene("Battle Scene");
            }
            //if battle/random encounter scene is active, cannot encounter anything
        }


        

    }




}