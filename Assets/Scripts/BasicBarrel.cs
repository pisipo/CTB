using UnityEngine;
using System.Collections;

public class BasicBarrel : MonoBehaviour
{

    public bool IsReady = true;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private BasicProjectile _projectilePrefab;
    [SerializeField] private Transform _burst;
    public float RecoilTime=0.5f;
    public float RecoilAmount = 1;
    private Vector3 _initialPosition;
	void Start ()
	{
	    _initialPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool Shoot(bool withRecoil)
    {
        Debug.Log("BarrelShoot");
       /* if (!IsReady)
            return false;
        IsReady = false;*/
        var projectile=Instantiate(_projectilePrefab, _muzzle.position, _muzzle.rotation)as BasicProjectile;
        Instantiate(_burst, _muzzle.position, _muzzle.rotation);
        //iTween.PunchPosition(gameObject, iTween.Hash("y",RecoilAmount, "space", Space.Self, "time", RecoilTime,"oncomplete","ShootFinished","oncompletetarget",gameObject));
        if (withRecoil)
        {
            iTween.Stop(gameObject);
            transform.localPosition = _initialPosition;
            iTween.PunchPosition(gameObject,iTween.Hash("amount", _muzzle.forward, "space", Space.World, "time", RecoilTime, "oncomplete","ShootFinished", "oncompletetarget", gameObject));

        }
        /*else
            IsReady = true; */
        return true;
    }

    private void ShootFinished()
    {
        IsReady = true;
        transform.localPosition = _initialPosition;
    }
}
