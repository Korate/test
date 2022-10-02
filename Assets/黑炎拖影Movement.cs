using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 黑炎拖影Movement : MonoBehaviour
{
    public Transform Player;
    private SpriteRenderer SR;
    private SpriteRenderer PlayerSR;
    private Color color;
    [SerializeField]
    private float activetime=0.2f;
    private float alpha;
    private float alphaset=0.8f;
    private float startTime;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        PlayerSR =Player.GetComponent<SpriteRenderer>();
        alpha = alphaset;
        SR.sprite = PlayerSR.sprite;
        transform.position = Player.position; 
        transform.rotation = Player.rotation;
        startTime = Time.time;
    }
    private void Update()
    {
        alpha *= 0.85f;
        color = new Color(1, 1, 1, alpha);
        SR.color = color;
        if (Time.time > startTime + activetime)
        {
            残影pool.Instance.AddToPool(gameObject);
        }
    }
}
