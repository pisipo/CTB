using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour {

	public bool opening=false,open=false,closing=false,closed=true,stuck=false;
	public int length;
	//enum OpenDirection{Up,Down,Left,Right};
	
	//public OpenDirection direction=OpenDirection.Right;
	void Start () {
		length=8;
	
	}
	

	void Update () {
		if(opening){
			closing=false;
		
			transform.localScale-=new Vector3(2f*Time.deltaTime,0f,0f);
			transform.Translate(1f*Time.deltaTime,0f,0f);
			
		}
		if(transform.localScale.x<=0){opening=false;open=true;}
		if(transform.localScale.x>=8){closing=false;closed=true;}
		if(closing==true && stuck==false){
			opening=false;
		
			transform.localScale+=new Vector3(2f*Time.deltaTime,0f,0f);
			transform.Translate(-1f*Time.deltaTime,0f,0f);
		}
	
	}
	public void Open ()
	{
		opening=true;
		closing=false;
	}
	public void Close ()
	{
		opening=false;
		closing=true;
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="Player")
			
		{
			//print ("yay");
			StartCoroutine(other.gameObject.GetComponent<Speedable>().SpeedUp(2,"my"));
			
		}
		if(other.gameObject.tag=="Box")
			
		{
			//print ("yay");
			//StartCoroutine(other.gameObject.GetComponent<Speedable>().SpeedUp(2,"my"));
		//	other.gameObject.rigidbody.mass=1000;
			stuck=true;
		}
	}
		void OnTriggerExit(Collider other)
	{
		
		if(other.gameObject.tag=="Box")
			
		{
			//print ("yay");
			//StartCoroutine(other.gameObject.GetComponent<Speedable>().SpeedUp(2,"my"));
		//	other.gameObject.rigidbody.mass=1000;
			stuck=false;
		}
	}
}

