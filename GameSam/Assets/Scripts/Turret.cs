using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update

    public Projectile PrefabProjectile;

    public bool Fire = false;

    float FireTime;

    public float FireRate;


    public Button ButtonParent;

    void Start()
    {
        ButtonParent.Turrets.Add(this);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position+transform.up * -1 * 5);
    }

    public void FireProjectile()
    {
        Projectile Bullet=Instantiate(PrefabProjectile).GetComponent<Projectile>();
        Bullet.transform.position = transform.position + transform.up * -1;
        Bullet.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Fire)
        {

        }
    }
}
