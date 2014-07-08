using UnityEngine;
using System.Collections;

public interface IHittable
{

    void Hit(IHitter projectile);
   

}
