using UnityEngine;

public class ButtonControl : MonoBehaviour {
	public GameObject door;
	int collidersCount=0;
   
	void Update()
	{ 
		
	}
	void OnTriggerEnter(Collider other)
    {
		print ("Enter");
		if (collidersCount==0){
			print ("Enterinif");
			door.GetComponent<DoorControl>().Open();
	        gameObject.renderer.material.color=new Color(1,0,0);
			//gameObject.transform.localScale= new Vector3(2f,2f,0.05f);
			transform.Translate(new Vector3(0,0,0.2f));
		}
		collidersCount++;
    }

    void OnTriggerExit(Collider other)
    {
		print ("Close");
		collidersCount--;
		if (collidersCount==0){
			print ("closeinif");
			door.GetComponent<DoorControl>().Close();
	        gameObject.renderer.material.color=new Color(0,1,0);
			//gameObject.transform.localScale= new Vector3(2f,2f,0.2f);
			transform.Translate(new Vector3(0,0,-0.2f));
		}
    }
}