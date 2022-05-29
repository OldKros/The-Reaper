using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameState : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeDisplay;
    [SerializeField] TextMeshProUGUI coinDisplay;

    [SerializeField] Canvas deathCanvas;

    KnightController player;

    public float TimePlayed { get; private set; } = 0.0f;

    int frameCount = 0;

    private void Awake()
    {
        player = FindObjectOfType<KnightController>();
        player.pickupCoin += UpdateCoinCount;
        player.GetComponent<Health>().onDeath += ShowDeathCanvas;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinCount();
    }

    // Update is called once per frame
    void Update()
    {
        TimePlayed += Time.deltaTime;
        frameCount++;
        if (frameCount % 10 == 0)
        {
            UpdateTimePlayed();
        }
    }

    void UpdateTimePlayed()
    {
        timeDisplay.text = $"{(TimePlayed / 60):00}:{(TimePlayed % 60):00}";
    }

    void UpdateCoinCount()
    {
        coinDisplay.text = $"{player.CoinCount}";
    }

    private void ShowDeathCanvas()
    {
        deathCanvas.gameObject.SetActive(true);
    }


}
