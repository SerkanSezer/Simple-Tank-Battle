using UnityEngine;

interface IAttackable
{
    abstract Transform MuzzlePoint { get; set; }
    abstract Transform MuzzleVFX { get; set; }
    abstract Transform ExplosionVFX { get; set; }
    abstract LayerMask LayerMask { get; set; }
    void Attack();
}
