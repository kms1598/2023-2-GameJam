using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    bool isDigging = false;
    bool isRadar = false;
    float inputX = 0;
    float inputY = 0;
    int moveSpeed = 2;
    Animator anim;

    public Transform leftTop;
    public Transform rightBottom;

    public Sprite digedGround;
    public AudioSource audioSource;
    public GameObject distanceText;
    public GameObject citrus;
    public GameObject shell;
    public GameObject radar;
    public AudioClip[] audios;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Dig();
        Radar();
    }

    void Move()
    {
        if (!isDigging && !isRadar)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");

            if (inputX != 0 || inputY != 0)
            {
                anim.SetBool("isMove", true);
            }
            else
            {
                anim.SetBool("isMove", false);
            }

            anim.SetFloat("inputX", inputX);
            anim.SetFloat("inputY", inputY);

            if ((inputX < 0 && transform.position.x <= leftTop.position.x + transform.localScale.x / 2) || (inputX > 0 && transform.position.x >= rightBottom.position.x - transform.localScale.x / 2))
                inputX = 0;
            if ((inputY > 0 && transform.position.y >= leftTop.position.y - transform.localScale.y / 2) || (inputY < 0 && transform.position.y <= rightBottom.position.y + transform.localScale.y / 2))
                inputY = 0;

            transform.Translate(new Vector2(inputX, inputY).normalized * Time.deltaTime * moveSpeed);
        }
    }

    void Dig()
    {
        if(Input.GetMouseButtonDown(0) && !isDigging && !isRadar)
        {
            anim.SetBool("isMove", false);
            anim.SetFloat("inputX", 0);
            anim.SetFloat("inputY", 0);

            MouseMove mm = GetComponentInChildren<MouseMove>();
            if(mm.GetSpriteRenderer.color == Color.green)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

                if(hit.collider.gameObject.tag == "ground" || hit.collider.gameObject.tag == "gold")
                {
                    isDigging = true;
                    anim.SetBool("isDig", true);
                    StartCoroutine(Digging(hit.collider.gameObject));
                }
                else
                {
                    return;
                }
            }
        }
    }

    IEnumerator Digging(GameObject hole)
    {
        audioSource.clip = audios[0];
        audioSource.Play();
        yield return new WaitForSeconds(2.666f);
        audioSource.Stop();
        anim.SetBool("isDig", false);
        hole.GetComponent<SpriteRenderer>().sprite = digedGround;
        if(hole.tag == "gold")
        {
            GameManager.instance.nowScore++;
            GameObject goldCitrus = Instantiate(citrus, hole.transform);
            audioSource.clip = audios[2];
            audioSource.Play();
        }
        if(hole.tag == "ground" && Random.Range(0, 5) == 3)
        {
            AchiveManager.instance.UnlockShell();
            GameObject shellObject = Instantiate(shell, hole.transform);
            audioSource.clip = audios[2];
            audioSource.Play();
        }
        hole.tag = "hole";
        isDigging = false;
    }

    void Radar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDigging && !isRadar)
        {
            anim.SetBool("isMove", false);
            anim.SetFloat("inputX", 0);
            anim.SetFloat("inputY", 0);

            isRadar = true;
            Vector3 pivot = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - gameObject.transform.localScale.y / 2);
            List<GameObject> goldGrounds = new List<GameObject>(GameObject.FindGameObjectsWithTag("gold"));
            GameObject closestGoldGround = goldGrounds[0];
            float minDistance = Vector3.Distance(pivot, goldGrounds[0].transform.position);

            foreach (GameObject goldGround in goldGrounds)
            {
                float Distance = Vector3.Distance(pivot, goldGround.transform.position);

                if (Distance < minDistance)
                {
                    closestGoldGround = goldGround;
                    minDistance = Distance;
                }
            }
            minDistance = Mathf.Floor(minDistance * 100f) / 100f;
            StartCoroutine(UseRadar(minDistance));
        }
    }

    IEnumerator UseRadar(float timer)
    {
        radar.GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(timer / 2);
        radar.GetComponent<Image>().color = Color.white;
        GameObject dText = Instantiate(distanceText, gameObject.transform);
        dText.GetComponent<DistanceText>().distance = timer;
        audioSource.clip = audios[1];
        audioSource.Play();
        isRadar = false;
    }
}
