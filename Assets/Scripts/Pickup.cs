using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType
    {
        Coin,
        Health
    }

    [field: SerializeField] public PickupType Type { get; private set; }
    [SerializeField] int value = 1;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        animator.SetBool("PickedUp", true);
    }


    private void DestroyAfterPickup()
    {
        Destroy(transform.parent.gameObject);
    }
}
