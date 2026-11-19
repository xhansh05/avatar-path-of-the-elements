using UnityEngine;

public class CollectibleScroll : MonoBehaviour
{
    [SerializeField] private CollectibleStatsData stats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<PlayerController>() != null)
        {
            int value = stats != null ? stats.scrollScoreValue : 1;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(value);
            }

            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.RegisterScrollCollected(value);
            }

            Destroy(gameObject);
        }
    }
}