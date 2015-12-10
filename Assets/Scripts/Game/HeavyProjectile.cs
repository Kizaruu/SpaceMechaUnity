using UnityEngine;
using System.Collections;

public class HeavyProjectile : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{
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
