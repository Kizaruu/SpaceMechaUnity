using UnityEngine;
using System.Collections;
using System.Timers;
using System;

public class GameRun : MonoBehaviour {
    public GameObject starship;
	public GameObject starship2;
	public GameObject starship3;
	GameObject ennemi;
    public float time;
	public static int counterShip = 0;
	int dice;
	float totalCpt;
	int maxRateBoss;
	// Use this for initialization
	void Start () {
        StartCoroutine("CreateStarship");
		totalCpt = 0;
		maxRateBoss = 30;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator CreateStarship()
    {
        while (true)
        {

		//yield return new WaitForSeconds(UnityEngine.Mathf.Sqrt((float)GameRun.counterShip) * (1.0f - (totalCpt/100)));
        
		dice = UnityEngine.Random.Range(0, maxRateBoss);

		if (dice == 1 && totalCpt > 10) {
			ennemi = starship2;
		} else if (dice == 2 || dice == 3) {
			ennemi = starship3;
		} else {
			ennemi = starship;
		}
		counterShip++;

			if (totalCpt < 15.00f) {
				totalCpt+=1.00f;
			} else if (totalCpt < 40.00f) {
				totalCpt+=1.50f;
			} else if (totalCpt < 65.00f) {
				maxRateBoss = 25;
				totalCpt+=0.70f;
			} else if (totalCpt < 80.00f) {
				maxRateBoss = 20;
				totalCpt+=0.40f;
			} else if (totalCpt < 99.00f) {
				maxRateBoss = 10;
				totalCpt+=0.10f;
				//totalCpt++;
			} 
		if (counterShip > 3) {
			yield return new WaitForSeconds (1.0f);
		} else if (counterShip > 10) {
			yield return new WaitForSeconds (0.6f);
		} else {
			yield return new WaitForSeconds (UnityEngine.Mathf.Sqrt ((float)GameRun.counterShip) * (1.0f - (totalCpt / 100)));
		}
		Instantiate(ennemi,
		            new Vector3(UnityEngine.Random.Range(-20, 20),
		            35,
		            -5), 
		            Quaternion.identity);
		}
       
    }
}
