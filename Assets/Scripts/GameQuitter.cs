using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    private void OnEnable()
    {
        Application.Quit();
    }
}
