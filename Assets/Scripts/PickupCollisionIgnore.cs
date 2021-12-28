using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollisionIgnore : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Physics2D.IgnoreCollision(player.GetComponent<EdgeCollider2D>(), GetComponent<BoxCollider2D>());
    }
}
