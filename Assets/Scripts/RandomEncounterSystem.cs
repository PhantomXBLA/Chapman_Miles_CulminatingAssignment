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
        if (other.tag == "Tall Grass")
        {
            if (Random.Range(1, 101) <= 1)
            {
                Debug.Log("A wild something something appeared");
                //Need to trigger battle state
                //Freeze the players movement until battle state is false.
                SceneManager.LoadScene("Battle Scene");
            }
        }
    }
}
