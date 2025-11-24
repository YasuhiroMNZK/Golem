using JetBrains.Annotations;
using UnityEngine;

public class MotionChange : MonoBehaviour
{
    [SerializeField] bool Run;
    [SerializeField] bool Jump;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void run()
    {
        Run = true;
        animator.SetBool("Run", Run);
    }

    public void jump()
    {
        Jump = true;
        animator.SetBool("Jump", Jump);
    }

    public bool RunState()
    {
        return Run;
    }

    public bool JumpState()
    {
        return Jump;
    }
}