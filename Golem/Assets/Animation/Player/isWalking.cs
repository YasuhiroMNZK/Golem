using UnityEngine;

public class isWalking : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        // WASDキーを押している間だけtrue
        bool isWalking = false;
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        
        animator.SetBool("isWalking", isWalking);
    }
}