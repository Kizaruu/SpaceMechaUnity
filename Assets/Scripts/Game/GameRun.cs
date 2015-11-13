using UnityEngine;
using System.Collections;
using System.Timers;
using System;

public class GameRun : MonoBehaviour {
    public GameObject starship;
    public float time;
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
            time -= 0.5F;
            if (time < 0)
                time = 0.5F;
            yield return new WaitForSeconds(time); // todo incrémentation du temps au fur et a mesure
            Instantiate(starship,
                new Vector3(UnityEngine.Random.Range(-20, 20),
                50 + 1,
                -5), Quaternion.identity);
                           
        }
       
    }
}
