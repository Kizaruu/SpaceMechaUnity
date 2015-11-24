﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class MainMecha : MonoBehaviour {
    public float speed;
    public Sprite[] sprites;
    private static int resultat = 0;
    public SpriteRenderer spriteRenderer;
    public GameObject projectilesPref, explosion;
    public GameObject gameOver; 
    public Animator anim;
    public Text scorePoints;
    public int limitXMin, limitYMin, limitXMax, limitYMax;
    public int resistance ;
    public float fireInterval, timeWhenFire;
    private bool isFire ;

    public List <UnityEngine.Object> projectilesPrefList;

    // Use this for initialization
    void Start () {
        scorePoints.text = "-10";
    }
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < limitXMax)
        {
            transform.position = new Vector3(
                transform.position.x + speed,
                transform.position.y,
                transform.position.z);
            spriteRenderer.sprite = sprites[2];
        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > limitXMin)
        {
            transform.position = new Vector3(
                transform.position.x - speed,
                transform.position.y,
                transform.position.z);
            spriteRenderer.sprite = sprites[1];
        }

        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < limitYMax)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + speed,
                transform.position.z);
        }

        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > limitYMin)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - speed,
                transform.position.z);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            spriteRenderer.sprite = sprites[0];
        }

        if (Input.GetKeyUp(KeyCode.Space) && isFire)
        {
            isFire = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isFire)
        {
            isFire = true;
            timeWhenFire = 0;
        }

       
        if (isFire)
        {
            if (timeWhenFire >= fireInterval  || timeWhenFire == 0)
            {
                Fire();
                timeWhenFire = 0;
            }
            timeWhenFire += Time.deltaTime;
        }
                
        if (resultat != int.Parse(scorePoints.text))
        {
            scorePoints.text = resultat.ToString();
            
        }
    }

    internal void GetDamage()
    {
        resistance--;
        if (resistance <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            gameOver.SetActive(true);
           
            Destroy(gameObject);
        }
        else
        {
            anim.SetTrigger("Blink");
        }
    }
    static public void UpdateScore(int points)    //, int score)
    {
        //string newScore;
        //resultat = score + points;
        //newScore = resultat.ToString();
        resultat += points;
        //return newScore;
    }
    public void Fire()
    {
        Instantiate(projectilesPref,
            new Vector3(transform.position.x,
            transform.position.y + 1,
            transform.position.z), Quaternion.identity);
    }
    

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
