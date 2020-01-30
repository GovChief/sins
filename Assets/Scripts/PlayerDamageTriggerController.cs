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
            TakeDamage(10);
        } else if (other.tag == "Bullet")
        {
            TakeDamage(15);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(TransparentTakingDamage());
    }

    IEnumerator TransparentTakingDamage()
    {
        int i = 0;
        while (i < 4)
        {
            gameObject.GetComponent<Renderer>().enabled = !gameObject.GetComponent<Renderer>().enabled;
            i++;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
