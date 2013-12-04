using UnityEngine;
using System.Collections;

public class BoxControl : MonoBehaviour {

	
	Vector3 direction;
	public int acceleration; 
	bool speedup;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTrigerEnter(Collider other)
	{
		if(other.gameObject.tag=="Door")
		{
			rigidbody.mass=10000;
			
		}
	}
}
