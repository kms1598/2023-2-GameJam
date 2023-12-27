using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Citrus : MonoBehaviour
{
    public float speed;
    public float deltaAlpha;
    public float destroyTime;
    SpriteRenderer image;
    Color alpha;
    void Start()
    {
        image = gameObject.GetComponent<SpriteRenderer>();
        alpha = image.color;
        Invoke("DestoryText", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * deltaAlpha);
        image.color = alpha;
    }

    void DestoryText()
    {
        Destroy(gameObject);
    }
}
