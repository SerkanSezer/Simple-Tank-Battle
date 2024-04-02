using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private ParticleSystem explosionVFX;
    [SerializeField] private AudioClip explosionSound;
    private SphereCollider sphereCollider;
    private Rigidbody rb;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, audioManager.soundVolume);
        explosionVFX.Play();
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        IDamagable damagable;
        if (other.transform.TryGetComponent<IDamagable>(out damagable))
        {
            damagable.Damage();
        }
        RemoveComponent();
    }

    private void RemoveComponent()
    {
        Destroy(sphereCollider);
    }

    
}
