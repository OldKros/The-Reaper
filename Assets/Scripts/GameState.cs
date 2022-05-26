using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameState : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeDisplay;

    KnightController player;

    float timePlayed = 0.0f;
    int frameCount = 0;

    private void Awake()
    {
        player = FindObjectOfType<KnightController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timePlayed += Time.deltaTime;
        frameCount++;
        if (frameCount % 10 == 0)
        {
            UpdateTimePlayed();
        }
    }

    void UpdateTimePlayed()
    {
        timeDisplay.text = $"{(timePlayed / 60):00}:{(timePlayed % 60):00}";
    }


}
