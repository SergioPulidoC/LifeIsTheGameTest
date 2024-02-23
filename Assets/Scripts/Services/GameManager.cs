using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static string selectedDance;
    public static string SelectedDance
    {
        get
        {
            return selectedDance;
        }
        set
        {
            selectedDance = value;
        }
    }
}
