using UnityEngine;
using System.Collections;

public enum ProjectileStatus
{
    Moving,
    Stoped
}

public class BasicProjectile : MonoBehaviour,IHitter
{
    public string forward;
    public string rotation;
    private float _damage=2;
    private float _piercingFactor=2;
    private ProjectileStatus _status;
    protected float _speed=35;
    protected float _maxRange=100;
    protected float _odometr;
    public ProjectileStatus Status
    {
        set { _status = value; }
        get { return _status; }
        
    }
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public float PiercingFactor
    {
        get { return _piercingFactor; }
        set { _piercingFactor = value; }
    }

    private Transform _transform;
    public Transform Transform
    {
        get { return _transform; }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BasicVilan>()!=null)
        {
         //  Debug.LogError("HIT");
            other.GetComponent<BasicVilan>().Hit(this);

        }
    }

    void Awake()
    {
        _transform = transform;
    }
    protected virtual void Update()
    {
        forward = transform.forward.ToString();
        rotation = transform.rotation.ToString();
        if(_odometr>=_maxRange)
            Destroy(gameObject);
    }
}
