using UnityEngine;

public class PlayerTank : Tank
{
    private GameManager gameManager;
    private const float PLAYER_TANK_MOVESPEED = 5;
    private const float PLAYER_TANK_ROTATESPEED = 50;
    [SerializeField] private LayerMask layerMask;

    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameFinished += GameManager_OnGameFinished;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        SubscribeToList();
    }

    private void GameManager_OnGameFinished(bool obj)
    {
        playerInputActions.Player.Disable();
    }

    public override void Move()
    {
        Vector2 moveInput = playerInputActions.Player.Move.ReadValue<Vector2>();
        RaycastHit hit;
        Vector3 bodyTank = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if (moveInput.y > 0 && !Physics.SphereCast(bodyTank, 1, transform.forward, out hit, 0.3f, layerMask))
        {
            transform.Translate(new Vector3(0, 0, moveInput.y) * PLAYER_TANK_MOVESPEED * Time.deltaTime);
        }else if(moveInput.y < 0 && !Physics.SphereCast(bodyTank, 1, -transform.forward, out hit, 0.3f, layerMask))
        {
            transform.Translate(new Vector3(0, 0, moveInput.y) * PLAYER_TANK_MOVESPEED * Time.deltaTime);
        }
        transform.Rotate(new Vector3(0, moveInput.x, 0) * PLAYER_TANK_ROTATESPEED * Time.deltaTime);
    }

    void Update()
    {
        Move();
    }

    public override void SubscribeToList()
    {
        gameManager.AddFriendlyList(gameObject);
    }

    public override void RemoveFromList()
    {
        gameManager.RemoveFriendTank(gameObject);
    }
}
