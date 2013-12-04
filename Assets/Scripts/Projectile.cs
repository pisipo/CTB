using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float speed,range;
	public GameObject burst;
	private float distance;
	void Update () {
		transform.Translate(Vector3.forward*Time.deltaTime*speed);
		distance+=Time.deltaTime*speed;
		if(distance>=range){
			Instantiate(burst,transform.position,transform.rotation);
			Destroy(gameObject);
		}
			
	}
}
