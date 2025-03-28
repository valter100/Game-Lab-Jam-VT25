using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public GameObject coinPrefab;
    public float chanceDouble = 1f;

    public void Start()
    {
        Instance = this;
    }

    public void SpawnCoin(Vector3 position)
    {
        Instantiate(coinPrefab);
    }
}
