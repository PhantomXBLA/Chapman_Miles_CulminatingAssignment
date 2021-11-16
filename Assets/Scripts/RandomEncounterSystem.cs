using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://www.youtube.com/watch?v=fePYYZaesSM&t=25s - Inspiration for Random Encounter System

public class RandomEncounterSystem : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Tall Grass") //and is moving
        {
            //A 1/1000 chance to encounter something if the player stays in the Tall Grass
            //for any amount of time.
            if (Random.Range(1, 1001) <= 1)
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
