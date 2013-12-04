using UnityEngine;
using System.Collections;
using System;


public class PlayerControl : MonoBehaviour {

	
	Vector3 direction;
Vector3 rotation;
	public int acceleration; 
	bool speedup;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*Sphere moving*/
		direction = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0); 
		rigidbody.AddForce(acceleration*direction);
	    rotation=Vector3.Cross(direction,Vector3.forward);
		//Debug.DrawRay(transform.position,rotation,Color.red);
		transform.Rotate(rotation*4,Space.World);
		
		/*Magnetico*/
		if(Input.GetButton("Jump")){
			var big = Physics.OverlapSphere(transform.position, 2.1f);
	        var small = Physics.OverlapSphere(transform.position, 0.6f);
			foreach (var body in big)
	        if (System.Array.IndexOf(small, body) == -1 && body.tag == "Box")
	        body.rigidbody.AddForce((transform.position - body.transform.position) * 20);
		}
	
		
	
	}
	
}
