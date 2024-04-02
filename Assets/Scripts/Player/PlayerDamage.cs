using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour, IDamagable
{
    [SerializeField] private Image healthBar;
    private float playerHealth = 1;
    public void Damage()
    {
        playerHealth -= 0.1f;
        healthBar.fillAmount = playerHealth;
        if (playerHealth <= 0)
        {
            //Debug.Log("Player is destroyed and waiting friend tanks");
        }
    }

}
