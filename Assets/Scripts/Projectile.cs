using UnityEngine;
using System.Collections;

public class Projectile : BasicProjectile {

	//private GameObject _ricoñhetParticle;

    void Start()
    {
        /*Damage = 100;
        PiercingFactor = 20;*/
        transform.rotation.Set(transform.rotation.x, transform.rotation.y, transform.rotation.z, 0);
    }
	protected override void Update () {
        base.Update();
		transform.Translate(Vector3.forward*Time.deltaTime*_speed);
       // transform.RotateAround(transform.position+Vector3.right*0.1f,transform.forward,20*Time.deltaTime);
       /* var xPos = Mathf.Sin(Time.time * 0.1f) * 1;
        var yPos = Mathf.Cos(Time.time * 0.1f) * 1;
        var zPos = transform.position.z+_speed * Time.deltaTime;
        transform.position=new Vector3(xPos,yPos,zPos);*/
		_odometr+=Time.deltaTime*_speed;
		/*if(distance>=_range){
			Instantiate(burst,transform.position,transform.rotation);
			Destroy(gameObject);
		}*/
			
	}
}
