using UnityEngine;
using System.Collections;

public class SAMControl : MonoBehaviour
{
    [SerializeField]
    private Transform[] muzzles;
    [SerializeField]
    private Transform launcher;

    [SerializeField] private GameObject _projectilePrefab;
    public Transform burst, target;
    public float rotate_speed = 5, next_fire_time = 0;
    public float reload_time = 5;
    float radius = 40;
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        launcher.Rotate(new Vector3(0, rotate_speed*Time.deltaTime, 0), Space.World);
        if (target != null)
        {
            if (Time.time >= next_fire_time)
            {
                print("gocha");
                Shoot();
            }
        }
        else
        {
            GameObject vilan;
            vilan = GetClosestObject();
            if (vilan) target = vilan.transform;
           // if (target) next_fire_time = Time.time + reload_time;
        }
    }

    private void Shoot()
    {
        next_fire_time = Time.time + reload_time;
        foreach (Transform muzzle in muzzles)
        {
            print("launch");
            var projectileClone= Instantiate(_projectilePrefab, muzzle.position, muzzle.rotation) as GameObject;
            projectileClone.GetComponent<Rocket>().target = target;
            Instantiate(burst, muzzle.position, muzzle.rotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            print("exit");
            if (other == target.collider) target = null;
        }
    }

    private GameObject GetClosestObject()
    {
      
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        Collider closestCollider = null;
        foreach (Collider hit in colliders)
        {
            //checks if it's hitting itself

            if (hit.tag == "enemy")
            {
                // continue;

                if (!closestCollider)
                {
                    closestCollider = hit;
                }
                //compares distances
                if (Vector3.Distance(transform.position, hit.transform.position) <=
                    Vector3.Distance(transform.position, closestCollider.transform.position))
                {
                    closestCollider = hit;
                }
                return closestCollider.gameObject;
            }
        }

        return null;
    }
}