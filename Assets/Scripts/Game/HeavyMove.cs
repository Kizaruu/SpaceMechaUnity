using UnityEngine;
using System.Collections;

public class HeavyMove : Enemy {
	
	public float moveSpeed = 0.01f;
	float ActualmoveSpeed = 0.01f;
	float deltaMM, newShakeX, newShakeY, denXY;
	int locked;
	// Use this for initialization
	void Start () {
		locked = 0;
	}
	
	// Update is called once per frame
	public override void Update () {
		
		deltaMM = Mathf.Abs (MainMecha.GetPosition () [0] - transform.position.x);
		
		if (deltaMM < 2.0f) {
			if (ActualmoveSpeed > 0)
				ActualmoveSpeed -= (2.0f - deltaMM)/1000;

			if (ActualmoveSpeed <= 0) {
				locked = 10;
				denXY = deltaMM;
				if (denXY < 0.1)
					denXY = 0.1f;
				newShakeX = UnityEngine.Random.Range (- moveSpeed / denXY, moveSpeed / denXY);
				if (newShakeX < 0.1f && newShakeX > 0)
					newShakeX = 0.1f;
				else if (newShakeX > -0.1f && newShakeX < 0)
					newShakeX = -0.1f;
				newShakeY = UnityEngine.Random.Range (- moveSpeed / denXY, moveSpeed / denXY);
				if (newShakeY < 0.1f && newShakeX > 0)
					newShakeY = 0.1f;
				else if (newShakeY > -0.1f && newShakeX < 0)
					newShakeY = -0.1f;
				transform.position = new Vector3(transform.position.x + newShakeX, transform.position.y + newShakeY, transform.position.z);
				base.Update();
			}
		} else {
			if (ActualmoveSpeed < moveSpeed)
				ActualmoveSpeed += 0.0001f;
		}
		if (locked > 0) {
			if (deltaMM < 0.5) 
				this.timeWhenFire /= deltaMM;
			else
				this.timeWhenFire *=2;
			locked --;
		}

		transform.position = new Vector3(transform.position.x, transform.position.y - ActualmoveSpeed, transform.position.z);
		base.Update();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "LimitLow") {
			Destroy (this.gameObject);
			GameRun.counterShip--;
		}
		else if (col.gameObject.name == "MainMecha")
		{
			col.GetComponent<MainMecha>().GetDamage();
		}
	}
}
