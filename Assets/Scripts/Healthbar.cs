using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{
    [SerializeField] Health healthTracker;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] int healthPerHeart;

    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite halfHeart;
    [SerializeField] Sprite emptyHeart;


    List<GameObject> hearts = new();

    // Start is called before the first frame update
    void Start()
    {
        healthTracker.healthRemoved += UpdateBar;
        int heartCount = healthTracker.MaximumHealth / healthPerHeart;
        // destroy all current children
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < heartCount; i++)
        {
            hearts.Add(Instantiate(heartPrefab, transform));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateBar()
    {
        // healthBar.transform.localScale = new Vector3(healthTracker.HealthPercent / 100, 1f, 1f);
        print("Updating Hearts");
        for (int i = 0; i < hearts.Count; i++)
        {
            int fullHearts = healthTracker.CurrentHealth / healthPerHeart;
            int remainingHealth = healthTracker.CurrentHealth - (fullHearts * healthPerHeart);

            int minHPThisHeart = healthPerHeart * i;
            int maxHPThisHeart = healthPerHeart * (i + 1);

            if (i < fullHearts)
            {
                hearts[i].GetComponent<Image>().sprite = fullHeart;
            }
            else if (remainingHealth != 0
                    && healthTracker.CurrentHealth < maxHPThisHeart
                    && healthTracker.CurrentHealth > minHPThisHeart)
            {
                hearts[i].GetComponent<Image>().sprite = halfHeart;
            }
            else
            {
                hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }

}
