using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseMove : MonoBehaviour
{
    float offset = 0.2f;
    public Transform leftTop;
    public Transform rightBottom;
    SpriteRenderer spriteRenderer;
    public SpriteRenderer GetSpriteRenderer
    {
        get { return spriteRenderer; }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MouseMoving();
    }
    void MouseMoving()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mousePosition.x >= leftTop.position.x + offset && mousePosition.x <= rightBottom.position.x - offset && mousePosition.y >= rightBottom.position.y + offset && mousePosition.y <= leftTop.position.y - offset)
        {
            spriteRenderer.enabled = true;
            mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));

            transform.position = mousePosition;
        }
        else
        {
            spriteRenderer.enabled = false;
        }

        if(Mathf.Abs(transform.localPosition.x) > 1.5f || Mathf.Abs(transform.localPosition.y) > 1.5f)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.green;
        }
    }
}
