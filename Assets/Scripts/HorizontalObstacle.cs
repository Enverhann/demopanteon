using System.Collections;
using UnityEngine;
using DG.Tweening;

public class HorizontalObstacle : MonoBehaviour
{
    Vector3 startPos;
    private float donutPosStartX;
    public float flipTime;
    public bool donut = false;
    private void Awake()
    {
        startPos = transform.position;
        donutPosStartX = transform.position.x;
    }
    private void Start()
    {
        StartCoroutine(DonutMovement());
    }
    void Update()
    {
        Movement();
    }
    private void Movement()
    {       
        transform.position = new Vector3(startPos.x - Mathf.PingPong(Time.time, flipTime * 2), transform.position.y, transform.position.z);       
    }
    public IEnumerator DonutMovement()
    {
        while (donut) {
            transform.DOMoveX(donutPosStartX, 1f);
            yield return new WaitForSeconds(Random.Range(0, 2));
            transform.DOMoveX(donutPosStartX-0.5f, 1f);
            yield return new WaitForSeconds(Random.Range(0, 2));
        }
    }
}
