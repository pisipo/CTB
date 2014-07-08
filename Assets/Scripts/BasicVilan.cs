using UnityEngine;
using System.Collections;



public class BasicVilan : MonoBehaviour,IHittable {
    [SerializeField]
	private Transform _bleedParticle;
    [SerializeField]
    private Transform _ricoñhetParticle;

	public float speed=30;
    [SerializeField]
    private float _health=25;
    [SerializeField]
    private float _armor=50;

    [SerializeField] private GameObject _idle;
    [SerializeField] private GameObject _dead;
    public float Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public bool IsAlive { get; set; }
    public float Armor
    {
        get { return _armor; }
        set { _armor = value; }
    }

 
   
    // Use this for initialization
	void Start ()
	{
	    IsAlive = true;
        rigidbody.AddForce(speed * transform.forward);
	}
	
	// Update is called once per frame
	void Update () {
	  //transform.Translate(Vector3.forward*Time.deltaTime*speed);
     
	}

    public virtual void Hit(IHitter projectile)
    {
        var projectileTransform = projectile.Transform;

        Armor -= projectile.PiercingFactor;
        if (Armor <= 0)
        {
            Health -= projectile.Damage;
            Instantiate(_bleedParticle,
                new Vector3(transform.position.x, projectileTransform.position.y, transform.position.z),
                projectileTransform.rotation);
            if (Health <= 0)
            {
                Die();
            }

        }
        else
        {
            var richochet=Instantiate(_ricoñhetParticle,
                projectileTransform.position,
               Quaternion.Euler( projectileTransform.forward*-1))as Transform;
            richochet.transform.forward = -1 * projectileTransform.forward;
            projectile.Transform.forward=Vector3.Reflect(projectileTransform.forward,transform.forward);
        }

    }

    protected virtual void  Die()
    {
        IsAlive = false;
        _idle.SetActive(false);
        _dead.SetActive(true);
        foreach (var part in GetComponentsInChildren<Rigidbody>())
        {
            part.AddExplosionForce(80,_dead.transform.position,0);
        }
        Destroy(gameObject,2f);
    }
	/*void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag=="projectile" )
		{
		 Instantiate(_bleedParticle,new Vector3( transform.position.x,other.transform.position.y,transform.position.z),other.transform.rotation);
         //Destroy (gameObject);
		}
	}*/
}
