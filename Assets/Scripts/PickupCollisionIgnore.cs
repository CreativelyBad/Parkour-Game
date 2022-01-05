using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollisionIgnore : MonoBehaviour
{
    public GameObject player;
    public GameObject groundCheck;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        groundCheck = GameObject.FindGameObjectWithTag("GroundChecker");
    }

    private void Update()
    {
        Physics2D.IgnoreCollision(player.GetComponent<EdgeCollider2D>(), GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(groundCheck.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }
}
