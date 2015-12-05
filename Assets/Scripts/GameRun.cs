using UnityEngine;
using System.Collections;
using System.Timers;
using System;

public class GameRun : MonoBehaviour {
    public GameObject starship;
    public float time;
	public static int counterShip = 0;
	// Use this for initialization
	void Start () {
        StartCoroutine("CreateStarship");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator CreateStarship()
    {
        while (true)
        {

			yield return new WaitForSeconds(UnityEngine.Mathf.Sqrt((float)GameRun.counterShip) * 0.8f); // todo incrémentation du temps au fur et a mesure
            
            Instantiate(starship,
                new Vector3(UnityEngine.Random.Range(-20, 20),
                50 + 1,
                -5), Quaternion.identity);

			counterShip++;
                           
        }
       
    }
}
