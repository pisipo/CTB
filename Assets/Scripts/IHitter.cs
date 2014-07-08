using UnityEngine;

public interface IHitter
{
    ProjectileStatus Status { get; set; }
    float Damage { get; set; }
    float PiercingFactor { get; set; }

    Transform Transform { get;}
}