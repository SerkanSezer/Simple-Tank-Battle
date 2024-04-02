using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour, IDamagable
{
    private EnemyMove enemyMove;
    [SerializeField] private Image healthBar;
    private float enemyHealth = 1;
    private bool enemyLive = true;
    private void Awake()
    {
        enemyMove = GetComponent<EnemyMove>();
    }
    public void Damage()
    {
        if (enemyLive)
        {
            
            enemyHealth -= 0.25f;
            healthBar.fillAmount = enemyHealth;
            if (enemyHealth <= 0)
            {
                enemyLive = false;
                enemyMove.RemoveFromList();
                Destroy(gameObject, 0.2f);
            }
        }
    }

}
