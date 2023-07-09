using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ducks : MonoBehaviour
{
    [SerializeField] private int health, points, moveSpeed;
    [SerializeField] private bool orientation;
    [SerializeField] private AudioSource hitAudio, cuackAudio;
    [SerializeField] private TMP_Text scoreDuckText;

    private float timeLifeDuck = 8f;
    DuckHealthBar healthBar;

    private void Awake() 
    {
        healthBar = GetComponentInChildren<DuckHealthBar>();
        scoreDuckText.enabled = false;
    }

    private void Start() 
    {
        healthBar.StartHealthBar(health);
        healthBar.UpdateHealthBar(health);
    }

    private void Update() 
    {
        if(orientation)
        {
            transform.position += new Vector3(-moveSpeed, 0f, 0f) * Time.deltaTime;
        } else {
            transform.position += new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime;
        }

        timeLifeDuck -= Time.deltaTime;
        if(timeLifeDuck <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void DucksDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health);
        if(health == 0)
        {   
            scoreDuckText.enabled = true;
            cuackAudio.Play();
            GameManager.Instance.SumScore(points);
            Destroy(gameObject, cuackAudio.clip.length);
        } else {
            hitAudio.Play();
        }
    }



}
