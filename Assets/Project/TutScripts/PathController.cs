using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField]
   

        float WaitDelay = 0.75f;


    public Animator animator;
    bool isWalking;


    

    void moveForward()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
            moveForward();
        
    }

    private void OnTriggerStay(Collider other)
    {
        WaitDelay -= Time.deltaTime;
        Debug.Log(WaitDelay);
        if (WaitDelay <= 0f)
        {
            animator.SetTrigger("Trigger");
        }
    }


}
