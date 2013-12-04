using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
	public Transform vilan;
	public float respawn_time;
	float next_vilan_time;
	// Use this for initialization
	void Start () {
	next_vilan_time=respawn_time+Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float  xcoord =Random.Range(-30,30);
		if(Time.time>=next_vilan_time){
			Instantiate(vilan,new Vector3(xcoord,vilan.localPosition.y,-20f),vilan.rotation);
			next_vilan_time=respawn_time+Time.time;
		}
	
	}
}
