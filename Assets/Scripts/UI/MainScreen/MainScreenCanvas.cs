using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenCanvas : MonoBehaviour
{
    private MainScreenCharacter character;
    [SerializeField] private Text danceNameTxt;

    private void Start()
    {
        character = FindObjectOfType<MainScreenCharacter>();
    }

    private void SetDanceName(string danceName)
    {
        danceNameTxt.text = danceName;
    }

    //Dance names:
    //  House
    //  Macarena
    //  Wave Hip Hop
    public void SelectDance(string animationName)
    {
        GameManager.SelectedDance = animationName;
        SetDanceName(animationName);
        character.ChangeAnimation(animationName);
    }

    public void GoToCombatScene()
    {
        SceneController.Instance.ChanceScene("Combat");
    }
}
