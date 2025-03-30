using TMPro;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] float fadeRate = 1f;
    TextMeshProUGUI textMesh;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (textMesh.color.a <= 0)
        {
            return;
        }
        Color color = textMesh.color;
        color.a -= fadeRate * Time.deltaTime;
        textMesh.color = color;
    }
}
