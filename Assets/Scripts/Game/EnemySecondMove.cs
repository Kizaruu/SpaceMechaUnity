using UnityEngine;
using System.Collections;

public class EnemySecondMove : Enemy {
	public int limitXMax, limitXMin;
	public float moveSpeed = 0.1f;
	float deltaX, deltaY;
	int cpt = 0;
	// Use this for initialization
	void Start () {
		ResetMove ();
	}
	
	// Update is called once per frame
	public override void Update () {

		cpt--;

		if (transform.position.x >= limitXMax) {

			transform.position = new Vector3 (transform.position.x - moveSpeed, transform.position.y , transform.position.z);
			ResetMove();
			base.Update ();

		} else if (transform.position.x <= limitXMin) {

			transform.position = new Vector3 (transform.position.x + moveSpeed, transform.position.y , transform.position.z);
			ResetMove();
			base.Update ();

		} else {

			transform.position = new Vector3 (transform.position.x + deltaX, transform.position.y + deltaY , transform.position.z);
			base.Update ();
		}

		if (cpt <= 0) {
			ResetMove();
		}
	}

	void ResetMove() {
		cpt = UnityEngine.Random.Range (10, 500);
		deltaX = UnityEngine.Random.Range (- moveSpeed/2, moveSpeed/2); 
		if (deltaX < (moveSpeed / 4) && deltaX > (-moveSpeed / 4))
			deltaX *= 2;
		deltaY = UnityEngine.Random.Range (- moveSpeed, 0.0f);

		if (resistance == 2) {
			cpt /= 3;
			deltaX *= 2;
			deltaY -= (moveSpeed / 3);
		}
		if (resistance == 1) {
			cpt /= 10;
			deltaX *= 3;
			deltaY -= (moveSpeed / 2);
		}
	}
}
