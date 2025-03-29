using TMPro;
using UnityEngine;

public class UpdateCoinText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = CoinManager.Instance.CurrentCoins.ToString();
    }
}
