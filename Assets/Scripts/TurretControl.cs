using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretControl : MonoBehaviour
{
    [SerializeField] private Transform _tower;
    [SerializeField] private Gatling _gatling;
    [SerializeField] private BasicBarrel[] _barrels;
	public List<Transform> targets;
	public Transform target;
	public float reload_time,turn_speed,fire_pause_time,miss_amount;
	private float next_fire_time,next_move_time;
    public float radius = 19;
	private Quaternion desired_rotation;
	void Start () {
		
	
	}
	

	void Update () {
		if(target!=null)
		{ 
            Aim (target.position);
		    //print(Quaternion.Dot(_tower.transform.rotation, desired_rotation));
			_tower.transform.rotation=Quaternion.Lerp (_tower.transform.rotation,desired_rotation,1/*Time.deltaTime*turn_speed*/);
			

			if(Time.time>=next_fire_time)Shoot();
		}
		else{
          //  Debug.LogError("GettingClosestObject");
			GameObject vilan;
			vilan=GetClosestObject();
			if(vilan)
                target=vilan.transform;
			if(target)
                next_fire_time=Time.time+(reload_time);
		}
		
	
	}
	/*void OnTriggerEnter(Collider other){
		//if(other.gameObject.tag=="enemy")targets.Add(other.transform);
		print ("enter");
		
		if(target==null && other.tag=="enemy"){

			target=other.gameObject.transform;
			next_fire_time=Time.time+(reload_time);
		}
	}*/
	void OnTriggerExit(Collider other){
	 if(other.gameObject.tag=="enemy")
		{
			if(other==target.collider)target=null;
		}
	}
	void Shoot()
	{
		next_move_time=Time.time+fire_pause_time;
		next_fire_time=Time.time+reload_time;
		/*foreach(Transform muzzle in muzzles){
			 Instantiate(projectile,muzzle.position,muzzle.rotation);
			// projectile.GetComponent<Rocket>().target=target;
			 Instantiate(burst,muzzle.position,muzzle.rotation);
		}
	    foreach (var barrel in _barrels)
	    {
	        iTween.PunchPosition(barrel.gameObject,iTween.Hash("y",-1f,"space",Space.Self,"time",reload_time-0.1f));
	    }*/
        foreach (var barrel in _barrels)
        {
            barrel.Shoot(true);
        }
	}
	void Aim(Vector3 target_pos)
	{
		Debug.DrawLine(transform.position,target_pos,Color.red);
		float aim_error=Random.Range(-miss_amount,miss_amount);
		Vector3 target_pos_with_error=new Vector3(target_pos.x+aim_error,target_pos.y,target_pos.z+aim_error);
		desired_rotation=Quaternion.LookRotation(target_pos-transform.position,Vector3.up);
       
	}
	GameObject GetClosestObject()
  	{
	
	    Collider[] colliders= Physics.OverlapSphere (transform.position, radius);
	    Collider closestCollider=null ;
	    foreach (Collider hit in colliders) {
	        //checks if it's hitting itself
				
	        if(hit.tag == "enemy")
	        {
	          // continue;
	        
			        if(!closestCollider)
			        {
			           closestCollider = hit;
			        }
			        //compares distances
			        if(Vector3.Distance(transform.position, hit.transform.position) <= Vector3.Distance(transform.position, closestCollider.transform.position))
			        {
			           closestCollider = hit;
			        }
				   return closestCollider.gameObject;
			}
	   		 
	     	
		}
				
		return null;
	}
}
