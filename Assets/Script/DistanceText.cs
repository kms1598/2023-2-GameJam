using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceText : MonoBehaviour
{
    public float speed;
    public float deltaAlpha;
    public float destroyTime;
    public float distance;
    TextMeshPro text;
    Color alpha;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = distance.ToString("F2");
        alpha = text.color;
        Invoke("DestoryText", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * deltaAlpha);
        text.color = alpha;
    }

    void DestoryText()
    {
        Destroy(gameObject);
    }
}
