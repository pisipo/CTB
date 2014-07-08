using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gatling :BasicGun
{
    

  public float _reloadTime;
    [SerializeField] private float _maxSpiningSpeed;
    [SerializeField] private float _accelerationSpeed;
    private float _breakingSpeed=5f;
    private int _currentBarrelIndex;
    public float _nextFireTime,_currentSpiningSpeed;

    public Gatling()
    {
        IsShooting = false;
    }

   // public bool IsShooting { get; set; }

    void Start()
    {
       
    }

    void Update()
    {
        if (IsShooting)
        {
           
            _currentSpiningSpeed += Time.deltaTime*_accelerationSpeed;//increasing rotation speed, then clamping it to max rotation speed
            _currentSpiningSpeed = Mathf.Clamp(_currentSpiningSpeed, 0, _maxSpiningSpeed);
            _reloadTime = 1/_currentSpiningSpeed;     //reload time inversely depends on rotation speed;
            _reloadTime=Mathf.Clamp(_reloadTime, 0, 1f);
            if (Time.time > _nextFireTime)
            {
                Debug.Log("GatlingTellsBarrel - SHOOT");
                _barrels[_currentBarrelIndex].Shoot(true);
                _currentBarrelIndex = (_currentBarrelIndex + 1) < _barrels.Length ? _currentBarrelIndex + 1 : 0;
                _nextFireTime = Time.time + _reloadTime;
            }
           
        }
        else
        {
          
            _currentSpiningSpeed -= Time.deltaTime * _breakingSpeed;//decreasing rotation speed, then clamping it to max rotation speed
            _currentSpiningSpeed = Mathf.Clamp(_currentSpiningSpeed, 0, _maxSpiningSpeed);
        }
        transform.Rotate(Vector3.up * _currentSpiningSpeed, Space.Self);
    }

    public override void Shoot()
    {
        if (IsShooting)
            return;
        IsShooting = true;
        
    }

    public override void Stop()
    {
        IsShooting = false;
    }

    


 
}
