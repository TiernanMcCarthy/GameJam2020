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


    public bool Projectile;


    public GameObject DeathZone;

    void Start()
    {
        if (ButtonParent != null)
        {
            ButtonParent.Turrets.Add(this);
        }
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
        if(Fire&& !Projectile)
        {
            DeathZone.SetActive(true);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<boomerangAbility>())
        {
            Destroy(DeathZone);
            Destroy(gameObject);
        }
    }
}
