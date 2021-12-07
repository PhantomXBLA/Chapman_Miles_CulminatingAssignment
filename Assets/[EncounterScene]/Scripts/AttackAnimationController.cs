using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackAnimationController : MonoBehaviour
{

    public GameObject leafBlastAnim;
    public GameObject slapAnim;
    public GameObject scratchAnim;

    // Start is called before the first frame update
    void Start()
    {
        leafBlastAnim.SetActive(false);
        slapAnim.SetActive(false);
        scratchAnim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnLeafBlastAttackAnim()
    {
        StartCoroutine(LeafBlastAnim());
    }

    public void OnSlapButtonAttackAnim()
    {
        StartCoroutine(SlapAnim());
    }

    public void OnScratchAttackAnim()
    {
        StartCoroutine(ScratchAnim());
    }




    IEnumerator LeafBlastAnim()
    {
        yield return new WaitForSeconds(1.0f); //for text to print
        leafBlastAnim.SetActive(true); //Play animation
        yield return new WaitForSeconds(4.0f); //wait for animation to finish
        leafBlastAnim.SetActive(false); // deactivate animation
    }

    IEnumerator SlapAnim()
    {
        yield return new WaitForSeconds(1.0f); //for text to print
        slapAnim.SetActive(true); //Play animation
        yield return new WaitForSeconds(4.0f); //wait for animation to finish
        slapAnim.SetActive(false); // deactivate animation
    }

    IEnumerator ScratchAnim()
    {
        yield return new WaitForSeconds(1.0f); //for text to print
        scratchAnim.SetActive(true); //Play animation
        yield return new WaitForSeconds(4.0f); //wait for animation to finish
        scratchAnim.SetActive(false); // deactivate animation
    }
}
