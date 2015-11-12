﻿using UnityEngine;
using System.Collections;
using System;

public class MainMecha : MonoBehaviour {
    public float speed;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public GameObject projectilesPref, explosion;
    public Animator anim;
    public int resistance ;
    public float fireInterval, timeWhenFire;
    private bool isFire ;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(
                transform.position.x + speed,
                transform.position.y,
                transform.position.z);
            spriteRenderer.sprite = sprites[2];
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(
                transform.position.x - speed,
                transform.position.y,
                transform.position.z);
            spriteRenderer.sprite = sprites[1];
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + speed,
                transform.position.z);
        }

        if (Input.GetKey(KeyCode.DownArrow))
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
    }

    internal void GetDamage()
    {
        resistance--;
        if (resistance <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            anim.SetTrigger("Blink");
        }
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