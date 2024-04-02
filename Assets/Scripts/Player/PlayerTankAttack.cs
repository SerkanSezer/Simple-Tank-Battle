using UnityEngine;

public class PlayerTankAttack : MonoBehaviour, IAttackable
{
    private GameManager gameManager;
    private PlayerInputActions playerInputActions;

    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Transform muzzleVFX;
    [SerializeField] private Transform explosionVFX;
    [SerializeField] private LayerMask layerMask;

    public Transform MuzzlePoint { get => muzzlePoint; set => muzzlePoint = value;}
    public Transform MuzzleVFX { get => muzzleVFX; set => muzzleVFX = value; }
    public Transform ExplosionVFX { get => explosionVFX; set => explosionVFX = value; }
    public LayerMask LayerMask { get => layerMask; set => layerMask = value; }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameFinished += GameManager_OnGameFinished;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Fire.performed += Fire_performed; ;
    }

    private void GameManager_OnGameFinished(bool obj)
    {
        playerInputActions.Player.Disable();
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Attack();
    }

    public void Attack()
    {
        CreateMuzzleImpact();
        CreateExplosionUsingRaycast();
    }

    public void CreateMuzzleImpact()
    {
        Instantiate(MuzzleVFX,MuzzlePoint.transform.position,transform.rotation,MuzzlePoint);
        
    }

    public void CreateExplosionUsingRaycast()
    {
        RaycastHit hit;
        if (Physics.SphereCast(MuzzlePoint.transform.position,1, MuzzlePoint.transform.forward,out hit,Mathf.Infinity,LayerMask))
        {
            Instantiate(ExplosionVFX, hit.point, transform.rotation);
        }
    }
}
