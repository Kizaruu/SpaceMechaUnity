using UnityEngine;
using System.Collections;

public class EnemySimpleMove : Enemy {
    
    public float moveSpeed = 0.1f;
	float deltaMM;
    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    public override void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
        base.Update();

		//deltaMM = Mathf.Abs (MainMecha.GetPosition () [0] - transform.position.x);

		//if (deltaMM < 1)
		//	this.timeWhenFire /= deltaMM;
	}
}
