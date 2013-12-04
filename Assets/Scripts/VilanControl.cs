using UnityEngine;
using System.Collections;

public class VilanControl : MonoBehaviour {
	public Transform explosion;
	public float speed=3;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	transform.Translate(Vector3.forward*Time.deltaTime*speed);
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="projectile" )
		{
		 Instantiate(explosion,new Vector3( transform.position.x,other.transform.position.y,transform.position.z),other.transform.rotation);
         //Destroy (gameObject);
		}
	}
}
