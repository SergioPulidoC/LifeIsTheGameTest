using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainScreenCharacter : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnimation(string animationName)
    {
        bool animationExists = anim.parameters.Any(a => a.name == animationName);
        if (!animationExists)
        {
            Debug.LogError("Animation with name: " + animationName + " doesn't exist.");
            return;
        }
        anim.SetTrigger(animationName);
    }
}
