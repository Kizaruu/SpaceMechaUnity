using UnityEngine;
using System.Collections;

public class EnemySecondProjectile : MonoBehaviour {
	public float speed;
	public GameObject explosion;
	float angleTrg;
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

		transform.Rotate (Vector3.forward, 20);

		angleTrg = UnityEngine.Mathf.Atan2(tPosMM[1]-transform.position.y,tPosMM[0] -transform.position.x);
		
		transform.position = new Vector3 (transform.position.x + UnityEngine.Mathf.Cos(angleTrg) * speed/2
		                                  , transform.position.y + UnityEngine.Mathf.Sin(angleTrg) * speed/2, 
		                                  transform.position.z);

		//transform.RotateAround(MainMecha.
		transform.position = new Vector3(transform.position.x,
		                                 transform.position.y - 3*speed/4,
		                                 transform.position.z);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "LimitLow" || col.gameObject.name == "Projectile(Clone)") {
			if (col.gameObject.name == "Projectile(Clone)") 
				Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
		if (col.gameObject.name == "MainMecha")
		{
			col.GetComponent<MainMecha>().GetDamage();
			Destroy(this.gameObject);
		}
	}
}
