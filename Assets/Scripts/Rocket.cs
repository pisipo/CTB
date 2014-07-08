using UnityEngine;
using System.Collections;
using System;

public class Rocket : BasicExplodingProjectile {

	// Use this for initialization
	public Transform target;
	//public float speed=3;
	bool aim=false;
	
	
	void Start()
	{
	    collider.enabled = false;
		iTween.RotateAdd(gameObject,iTween.Hash("x",-90,"time",2,"space",Space.Self,"oncomplete","Aim"));
		//iTween.MoveAdd(gameObject,iTween.Hash("z",20,"time",5,"space",Space.Self));
			
	}
	

	void Update () {
		transform.Translate(0,0,_speed*Time.deltaTime);
		if(aim)
		{
		    collider.enabled = true;
			iTween.LookUpdate (gameObject,iTween.Hash("looktarget",target,"time",10));
		}
	}
	void Aim()
	{
		aim=true;
	}
	
	

}
