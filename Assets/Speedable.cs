using UnityEngine;
using System.Collections;

public class Speedable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public IEnumerator SpeedUp(int length,string dir)
	{
		float step=0.4f, delay=0.01f;
		Vector3 startPos=transform.position;
		switch(dir){
			case "x":{
				while(transform.position.x<startPos.x+length){
					transform.Translate(step,0,0);
					yield return new WaitForSeconds(delay);
				}
			   break;
			}
			case "mx":{
				while(transform.position.x>startPos.x-length){
					transform.Translate(-step,0,0);
					yield return new WaitForSeconds(delay);
				}
				break;
			}
			case "y":{
				while(transform.position.y<startPos.y+length){
					transform.Translate(0,step,0);
					yield return new WaitForSeconds(delay);
				}
				break;
			}		
			case "my":
				{
				while(transform.position.y>startPos.y-length){
				 //   print ("-y");
					transform.Translate(0,-step,0);
					yield return new WaitForSeconds(delay);
				}	
				break;
			}
		}
	}
}
