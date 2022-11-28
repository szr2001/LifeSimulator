using UnityEngine;
using UnityEngine.UI;

public class FpsReader : MonoBehaviour
{
    public Text FpsText;

    private float deltaTime;
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        FpsText.text = Mathf.Ceil(fps).ToString();
    }
}
