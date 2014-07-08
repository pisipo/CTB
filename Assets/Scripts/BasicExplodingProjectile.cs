using UnityEngine;
using System.Collections;

public class BasicExplodingProjectile : BasicProjectile {
    private float _damageArea=15;
    public float DamageArea
    {
        get { return _damageArea; }
        set { _damageArea = value; }
    }

    void Start()
    {
        Damage = 10;
        PiercingFactor = 10;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy" || other.tag=="ground")
        {
            Debug.LogError("HIT");
            Explode();
        }
    }
    void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, DamageArea);
        foreach (Collider hit in colliders)
        {
            if (hit && hit.rigidbody && hit.tag == "enemy")
            {
                hit.enabled = false;
                hit.rigidbody.useGravity = true;
                hit.rigidbody.AddExplosionForce(500f, explosionPos, DamageArea, 30.0F);
                hit.GetComponent<BasicVilan>().Hit(this);
            }

        }
    }
}
