using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Health healthTracker;
    [SerializeField] GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthTracker.healthChanged += UpdateBar;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateBar()
    {
        healthBar.transform.localScale = new Vector3(healthTracker.HealthPercent / 100, 1f, 1f);
    }
}
