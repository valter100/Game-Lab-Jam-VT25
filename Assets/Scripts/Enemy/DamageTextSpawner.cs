using TMPro;
using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    public static DamageTextSpawner Instance;
    [SerializeField] GameObject textPrefab;
    [SerializeField] Color colorAir;
    [SerializeField] Color colorEarth;
    [SerializeField] Color colorFire;
    [SerializeField] Color colorWater;

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

    public void SpawnText(float damage, Vector3 position, DamageType damageType) // ADD COLOR FOR CRIT
    {
        position.y -= 0.25f;
        GameObject go = Instantiate(textPrefab, position, Quaternion.identity);
        var textMesh = go.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = ((int)damage).ToString();
        switch (damageType)
        {
            case DamageType.None:
                break;
            case DamageType.Fire:
                textMesh.color = colorFire;
                break;
            case DamageType.Air:
                textMesh.color = colorAir;
                break;
            case DamageType.Water:
                textMesh.color = colorWater;
                break;
            case DamageType.Earth:
                textMesh.color = colorEarth;
                break;
        }

        Destroy(go, 2f);
    }
}
