using UnityEngine;
using System.Collections;

public class EnemySecondProjectile : MonoBehaviour {
	public float speed;
	//float angle = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{
		float[] tPosMM = MainMecha.GetPosition ();
		Vector3 vPosMM = new Vector3 (tPosMM [0], tPosMM [1], 0.0f);

		if (transform.position.x < tPosMM[0])
			transform.RotateAround (vPosMM - transform.position, Vector3.forward, -speed);
		else
			transform.RotateAround (vPosMM - transform.position, Vector3.forward, speed);

		//angle += speed;
		//if (angle > 360)
		//	speed -= 360;

		transform.Rotate (Vector3.forward, 30);

		//transform.RotateAround(MainMecha.
		transform.position = new Vector3(transform.position.x,
		                                 transform.position.y - speed,
		                                 transform.position.z);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "LimitLow")
			Destroy(this.gameObject);
		if (col.gameObject.name == "MainMecha")
		{
			col.GetComponent<MainMecha>().GetDamage();
			Destroy(this.gameObject);
		}
	}
}
