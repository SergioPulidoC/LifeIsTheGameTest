using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceCharacter : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().SetTrigger(GameManager.SelectedDance);
    }
}
