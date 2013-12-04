using UnityEngine;
using System.Collections;
using System;

public class Rocket : MonoBehaviour {

	// Use this for initialization
	public Transform target;
	public float speed=3;
	bool aim=false;
	
	
	void Start(){
		
		iTween.RotateAdd(gameObject,iTween.Hash("x",-90,"time",5,"space",Space.Self,"oncomplete","Aim"));
		//iTween.MoveAdd(gameObject,iTween.Hash("z",20,"time",5,"space",Space.Self));
			
	}
	

	void Update () {
		transform.Translate(0,0,speed*Time.deltaTime);
		if(aim){
			iTween.LookUpdate (gameObject,iTween.Hash("looktarget",target,"time",10));
		}
	}
	void Aim()
	{
		aim=true;
	}
	
	

}
