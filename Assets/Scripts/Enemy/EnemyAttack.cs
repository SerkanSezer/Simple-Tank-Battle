using UnityEngine;

public class EnemyAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Transform muzzleVFX;
    [SerializeField] private Transform explosionVFX;
    [SerializeField] private LayerMask layerMask;

    private EnemyMove enemyMove;

    public Transform MuzzlePoint { get => muzzlePoint; set => muzzlePoint = value; }
    public Transform MuzzleVFX { get => muzzleVFX; set => muzzleVFX = value; }
    public Transform ExplosionVFX { get => explosionVFX; set => explosionVFX = value; }
    public LayerMask LayerMask { get => layerMask; set => layerMask = value; }

    private void Awake()
    {
        enemyMove  = GetComponent<EnemyMove>();
        enemyMove.OnEnemyAttack += Attack;
    }
    public void Attack()
    {
        CreateMuzzleImpact();
        CreateExplosionUsingRaycast();
    }

    public void CreateMuzzleImpact()
    {
        Instantiate(MuzzleVFX, MuzzlePoint.transform.position, transform.rotation, MuzzlePoint);

    }

    public void CreateExplosionUsingRaycast()
    {
        RaycastHit hit;
        if (Physics.SphereCast(MuzzlePoint.transform.position, 1, MuzzlePoint.transform.forward, out hit, Mathf.Infinity, LayerMask))
        {
            Instantiate(ExplosionVFX, hit.point, transform.rotation);
        }
    }
}
