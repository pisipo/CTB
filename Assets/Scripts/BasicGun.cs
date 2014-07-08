using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;

public class BasicGun:MonoBehaviour
{
    [SerializeField]
    protected BasicBarrel[] _barrels;
    public virtual bool IsShooting
    {
        get;
        set;
    }

   public  virtual void  Shoot()
    {
        
    }

   public virtual void Stop()
    {
        
    }





}
