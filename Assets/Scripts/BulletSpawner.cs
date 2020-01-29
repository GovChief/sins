using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletController bulletPrefab;
    public float bulletSpeed;
    private float time = 0;
    public float bulletDelay;

    public bool shoot;

    private Vector2 dir2;
    // Start is called before the first frame update
    void Start()
    {
        dir2 = new Vector2(bulletSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(shoot == true)
        {
            if(time< Time.time)
            {
                Vector2 position = new Vector2(transform.position.x, transform.position.y+1);
                BulletController bullet = Instantiate(bulletPrefab, position, transform.rotation) as BulletController;
                bullet.dir = dir2;
                time = Time.time + bulletDelay;
            }
        }
    }
}
