using UnityEngine;

public class Pilha : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onCollected;

    [SerializeField] private float _energyRestore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onCollected.Raise(this, _energyRestore);
            Destroy(gameObject);
        }
    }
}