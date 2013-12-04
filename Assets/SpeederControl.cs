using UnityEngine;

public class SpeederControl : MonoBehaviour {
	//public GameObject door;
  
	void OnTriggerEnter(Collider other)
    {
		StartCoroutine(other.gameObject.GetComponent<Speedable>().SpeedUp(8,"y"));
        gameObject.renderer.material.color=new Color(1,0,0);
		//gameObject.transform.localScale= new Vector3(2f,2f,0.05f);
		transform.Translate(new Vector3(0,0,0.2f));
    }

    void OnTriggerExit(Collider other)
    {
		
        gameObject.renderer.material.color=new Color(0,1,0);
		//gameObject.transform.localScale= new Vector3(2f,2f,0.2f);
		transform.Translate(new Vector3(0,0,-0.2f));
    }
}