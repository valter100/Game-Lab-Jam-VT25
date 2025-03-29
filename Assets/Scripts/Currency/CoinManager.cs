using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    [SerializeField] private float currentCoins = 0;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float chanceDouble = 1f;

    public void Start()
    {
        Instance = this;
    }

    public void SpawnCoin(Vector3 position)
    {
        position.y = 0.5f;
        Instantiate(coinPrefab, position, Quaternion.identity);
    }

    public void PickUp(float value)
    {
        currentCoins += value;
    }

    public bool TakeCoins(float amount)
    {
        if (currentCoins - amount < 0)
        {
            return false;
        }
        currentCoins -= amount;
        return true;
    }
}
