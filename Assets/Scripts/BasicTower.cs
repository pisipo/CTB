using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicTower : MonoBehaviour
{
     [SerializeField]
    private Transform _tower;
    [SerializeField]
    private BasicGun[] _guns;

  //  public List<Transform> targets;
    public BasicVilan target;
    public float turn_speed, fire_pause_time, miss_amount;
    private float  next_move_time;
    public float radius = 19;
   // private Quaternion _desiredRotation;
    private Transform _transform;
    private float velocity;
    public float maximumRotateSpeed = 20;
    public float minimumTimeToReachRarget=1f;
    void Start()
    {

        _transform = _tower == null ? transform : _tower.transform;
    }


    void Update()
    {
        if (target != null && target.IsAlive)
        {
           var desiredRotation= Aim(target.transform.position);
            //_tower.transform.rotation = Quaternion.Lerp(_transform.rotation, _desiredRotation,1/* Time.deltaTime * turn_speed*/);
            var angles = _transform.rotation.eulerAngles;
            var diffAngle=Quaternion.Angle(desiredRotation, _transform.rotation);
            if (diffAngle <= 5)
                _transform.rotation = Quaternion.Euler(angles.x, desiredRotation.eulerAngles.y, angles.z);
            else
                _transform.rotation = Quaternion.Euler(angles.x,Mathf.SmoothDampAngle(angles.y, desiredRotation.eulerAngles.y, ref velocity, minimumTimeToReachRarget,maximumRotateSpeed), angles.z);
            foreach (var gun in _guns)
            {
                gun.Shoot();
            }          
        }
        else
        {
            foreach (var gun in _guns)
            {
                gun.Stop();
            }
            var vilan = GetClosestObject();
            if (vilan)
                target = vilan.GetComponent<BasicVilan>();
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            if (other == target.collider) target = null;
        }
    }
  
     Quaternion Aim(Vector3 target_pos)
    {
        Debug.DrawLine(transform.position, target_pos, Color.red);
        float aim_error = Random.Range(-miss_amount, miss_amount);
        Vector3 target_pos_with_error = new Vector3(target_pos.x + aim_error, target_pos.y, target_pos.z + aim_error);
        return Quaternion.LookRotation(target_pos-_transform.position, Vector3.up);
    }
    GameObject GetClosestObject()
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
                if (Vector3.Distance(transform.position, hit.transform.position) <= Vector3.Distance(transform.position, closestCollider.transform.position))
                {
                    closestCollider = hit;
                }
                return closestCollider.gameObject;
            }


        }

        return null;
    }
}
