using UnityEngine;
using System.Collections;
using System.Timers;
using System;

public class GameRun : MonoBehaviour {
    public GameObject starship;
	public GameObject starship2;
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

			yield return new WaitForSeconds(UnityEngine.Mathf.Sqrt((float)GameRun.counterShip) * (1.0f - (totalCpt/100))); // todo incrémentation du temps au fur et a mesure
            
			dice = UnityEngine.Random.Range(0, maxRateBoss);

			if (dice == 1 && totalCpt > 10) {
				ennemi = starship2;
			} else {
				ennemi = starship;
			}
			counterShip++;

			if (totalCpt < 40.00f) {
				totalCpt+=2.00f;
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
//			else if (totalCpt < 99.90f) {
//				maxRateBoss = 10;
//				//totalCpt+=0.10f;
//				totalCpt+=0.03f;
//			} 
			//else if (totalCpt < 99.99f) {
			//	maxRateBoss = 5;
			//	totalCpt+=0.01f;
			//} 

			Instantiate(ennemi,
			            new Vector3(UnityEngine.Random.Range(-20, 20),
                50 + 1,
                -5), Quaternion.identity);


                           
        }
       
    }
}
