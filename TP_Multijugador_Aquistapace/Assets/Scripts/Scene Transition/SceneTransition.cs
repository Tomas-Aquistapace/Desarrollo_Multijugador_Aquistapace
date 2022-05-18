using UnityEngine;
using System;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private bool awakeWithAnimation = false;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        if(awakeWithAnimation)
        {
            anim.SetTrigger("FirstAwake");
        }
    }

    public Animator GetAnimator()
    {
        return anim;
    }

    public void ChangeAnimation(Action function)
    {
        if (!awakeWithAnimation)
        {
            anim.SetTrigger("FirstChange");
        }
        else
        {
            anim.SetTrigger("Change");
        }

        function?.Invoke();
    }
}