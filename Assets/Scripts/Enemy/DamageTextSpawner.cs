using TMPro;
using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    public static DamageTextSpawner Instance;
    [SerializeField] GameObject textPrefab;

    public void Start()
    {
        Instance = this;
    }

    public void SpawnText(float damage, Transform parent) // ADD COLOR FOR CRIT
    {
        GameObject go = Instantiate(textPrefab, parent.transform.position, Quaternion.identity, parent);
        go.GetComponentInChildren<TextMeshProUGUI>().text = ((int)damage).ToString();
        Destroy(go, 2f);
    }
}
