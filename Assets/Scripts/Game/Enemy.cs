﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {
    public GameObject projectilePref;
    public float fireInterval;
    public int score;
    public int resistance;
    //public Text finalScore; 
    public GameObject explosion;
    protected float timeWhenFire;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if (timeWhenFire >= fireInterval || timeWhenFire == 0)
        {
            Fire();
            timeWhenFire = 0;
        }
        timeWhenFire += Time.deltaTime;
    }

    public virtual void Fire()
    {
        Instantiate(projectilePref,
            new Vector3(transform.position.x,
            transform.position.y + 1,
            transform.position.z), Quaternion.identity);
    }

    public void GetDamage()
    {
        resistance--;
        if (resistance <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            MainMecha.UpdateScore(score);
            Destroy(gameObject);
			GameRun.counterShip--;
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "LimitLow") {
			Destroy (this.gameObject);
			GameRun.counterShip--;
		}
        else if (col.gameObject.name == "MainMecha")
        {
            col.GetComponent<MainMecha>().GetDamage();
            Destroy(this.gameObject);
			GameRun.counterShip--;
        }
    }
}
