using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTriggerController : MonoBehaviour
{
    public int damage = 10;
    public int health = 100;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Trap")
        {
            health -= 10;
        } else if (other.tag == "Bullet")
        {
            health -= 15;
        }
    }
}
