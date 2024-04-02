using UnityEngine;
using UnityEngine.UI;

public class FriendlyDamage : MonoBehaviour,IDamagable
{
    private FriendlyMove friendlyMove;
    [SerializeField] private Image healthBar;
    private float friendHealth = 1;

    private void Awake()
    {
        friendlyMove = GetComponent<FriendlyMove>();
    }
    public void Damage()
    {
        friendHealth -= 0.25f;
        healthBar.fillAmount = friendHealth;
        if (friendHealth <= 0)
        {
            friendlyMove.RemoveFromList();
            Destroy(gameObject, 1);
        }
    }
}
