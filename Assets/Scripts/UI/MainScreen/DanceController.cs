using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceController: MonoBehaviour
{
    private MainScreenCanvas mainScreen;
    [SerializeField] private MainScreenCharacter character;

    private void Start()
    {
        mainScreen = FindObjectOfType<MainScreenCanvas>();
    }

    //Animation parameter names:
    //House
    //Macarena
    //WaveHipHop
    public void SelectDance(string animationName)
    {
        mainScreen.SetDanceName(animationName);
        character.ChangeAnimation(animationName);
    }
}
