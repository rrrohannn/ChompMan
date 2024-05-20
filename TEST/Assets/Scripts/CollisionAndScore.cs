using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollisionAndScore : MonoBehaviour
{
    public TextMeshPro textMeshPro;  
    private int score;
    public float minScale = 0.8f;       
    public float maxScale = 1.0f;
    public float speed = 2.0f;           

    void Start()
    {
        textMeshPro.text = "0";
        score = 0;
    }

    private void ScoreUpdate()
    {
        score += 1;
        textMeshPro.text = score.ToString();
        StartCoroutine(BeatingHeartEffect());
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("collide"))
        {
            ScoreUpdate();
            Destroy(collider.gameObject);
        }

        else if (collider.gameObject.CompareTag("enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        else if (collider.gameObject.CompareTag("reward"))
        {
            score += 50;
            Destroy(collider.gameObject);
        }
    }

    private IEnumerator BeatingHeartEffect()
    {
        float currentScale = textMeshPro.transform.localScale.x;
        float targetScale = maxScale;
        bool isScalingUp = true;

        while (true)
        {
            float scale = textMeshPro.transform.localScale.x;

            if (isScalingUp)
            {
                // Scale up
                scale += Time.deltaTime * speed;
                if (scale >= maxScale)
                {
                    scale = maxScale;
                    isScalingUp = false;
                }
            }
            else
            {
                // Scale down
                scale -= Time.deltaTime * speed;
                if (scale <= minScale)
                {
                    scale = minScale;
                    break;
                }
            }

            textMeshPro.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        textMeshPro.transform.localScale = new Vector3(minScale, minScale, minScale);
    }
}
