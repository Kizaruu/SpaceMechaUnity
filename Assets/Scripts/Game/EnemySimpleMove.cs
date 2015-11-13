using UnityEngine;
using System.Collections;

public class EnemySimpleMove : Enemy {
    
    public float moveSpeed;
    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    public override void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
        base.Update();
	}
}
