using UnityEngine;
using System.Collections;
using System.Timers;
using System;

public class GameRun : MonoBehaviour {
    private ClientServices clientServices = new ClientServices();
    public GameObject starship;
	public GameObject starship2;
	public GameObject starship3;
	GameObject ennemi;
    public float time;
	public static int counterShip = 0;
	int dice;
	float totalCpt;
	int maxRateBoss;
    public static Boolean gameOver = false;
    public Vector2 scrollPosition = new Vector2();
    public string longString = "";
    //ArrayList test;
    string resAllScores = ""; 

    // Use this for initialization
    void Start () {
        StartCoroutine("CreateStarship");
		totalCpt = 0;
		maxRateBoss = 30;

        clientServices.GetAllScoresCompleted += ClientServices_GetAllScoresCompleted;
        
    }

    private void ClientServices_GetAllScoresCompleted(object sender, GetAllScoresCompletedEventArgs e)
    {
        resAllScores = e.Result;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator CreateStarship()
    {
        while (GameRun.gameOver == false)
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
        clientServices.GetAllScoresAsync(ApplicationModel.idEvent);       
    }

    void OnGUI()
    {
        if (gameOver == true)
        {

            GUI.BeginGroup(new Rect(2 * Screen.width / 5, Screen.height / 5, Screen.width / 2, Screen.width / 2));
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width / 5), GUILayout.Height(Screen.width / 3));
            GUILayout.Label(longString);

            GUILayout.EndScrollView();

            if (resAllScores.Length > 0)
            {
                longString += resAllScores;
                resAllScores = "";
            }
            GUI.EndGroup();

            //gameOver = false;
        }
    }
}
