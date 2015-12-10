using UnityEngine;
using System.Collections;

public class EnemySecondMove : Enemy {
	public float moveSpeed = 0.1f, limitXmin = -35.0f, limitXmax = 35.0f, limitYmin = -5.0f, limitYmax = 30.0f;
	float delta, angleTrg;//X, deltaY;
	float[] posMM;
	int cpt = 0;
	// Use this for initialization
	void Start () {
		ResetMove ();
	}
	
	// Update is called once per frame
	public override void Update () {

		cpt--;

		transform.position = new Vector3 (transform.position.x + 2*delta, transform.position.y + delta , transform.position.z);
		base.Update ();
		

		if (cpt <= 0) {
			ResetMove();
		}

		posMM = MainMecha.GetPosition ();
		if (transform.position.x > limitXmax)
			transform.position = new Vector3 (transform.position.x - moveSpeed,
			                                  transform.position.y, 
			                                  transform.position.z);
		else if (transform.position.x < limitXmin)
			transform.position = new Vector3 (transform.position.x + moveSpeed,
			                                  transform.position.y, 
			                                  transform.position.z);
		else if (transform.position.y > limitYmin)
			transform.position = new Vector3 (transform.position.x
			                                  ,transform.position.y - moveSpeed, 
			                                  transform.position.z);
		else if (transform.position.y < limitYmin)
			transform.position = new Vector3 (transform.position.x
			                                  ,transform.position.y + moveSpeed, 
			                                  transform.position.z);
		else {
			if (posMM[0] - transform.position.x < 0)
				angleTrg = UnityEngine.Mathf.Atan2(posMM[1]-transform.position.y + 20,posMM[0] -transform.position.x +12);
			else
				angleTrg = UnityEngine.Mathf.Atan2(posMM[1]-transform.position.y + 20,posMM[0] -transform.position.x -12);

			transform.position = new Vector3 (transform.position.x + UnityEngine.Mathf.Cos(angleTrg) * moveSpeed
			                                  , transform.position.y + UnityEngine.Mathf.Sin(angleTrg) * moveSpeed, 
			                                  transform.position.z);
		}
	}

	void ResetMove() {
		cpt = UnityEngine.Random.Range (10, 400);
		delta = UnityEngine.Random.Range (- moveSpeed, moveSpeed); 
		if (delta < (moveSpeed / 3) && delta > (-moveSpeed / 3))
			delta *= 2;
		//deltaY = UnityEngine.Random.Range (- moveSpeed, 0.0f);

		if (resistance < 7) {
			cpt /= 3;
			delta *= 2;
		//	deltaY -= (moveSpeed / 3);
		}else if (resistance < 3) {
			cpt /= 10;
			delta *= 3;
		//	deltaY -= (moveSpeed / 2);
		}
	}
}
