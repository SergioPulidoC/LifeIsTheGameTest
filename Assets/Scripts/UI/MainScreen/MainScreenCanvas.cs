using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenCanvas : MonoBehaviour
{
    [SerializeField] private Text danceNameTxt;

    public void SetDanceName(string danceName)
    {
        danceNameTxt.text = danceName;
    }
}
