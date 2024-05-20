using UnityEngine;
using TMPro;

public class ElapsedTimeDisplay : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    private float elapsedTime;

    void Start()
    {
        elapsedTime = 0f;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);

        textMeshPro.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
