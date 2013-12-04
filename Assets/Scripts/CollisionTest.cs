using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        print("collision");
       print(other.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        print("trriger");
        print(other.name);
    }
}
