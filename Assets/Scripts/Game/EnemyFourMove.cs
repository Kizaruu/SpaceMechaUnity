using UnityEngine;
using System.Collections;

public class EnemyFourMove : Enemy {
    public int limitXMax = 16, limitXMin = -16;
    public float moveSpeed = 0.1f;
    float delta, angleTrg;//X, deltaY;
    float[] posMM;
    int cpt = 0;
    // Use this for initialization
    void Start()
    {
        ResetMove();
    }

    // Update is called once per frame
    public override void Update()
    {

        cpt--;

        if (transform.position.x >= limitXMax)
        {

            transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.z);
            ResetMove();
            base.Update();

        }
        else if (transform.position.x <= limitXMin)
        {

            transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
            ResetMove();
            base.Update();

        }
        else
        {

            transform.position = new Vector3(transform.position.x + 2 * delta, transform.position.y + delta, transform.position.z);
            base.Update();
        }

        if (cpt <= 0)
        {
            ResetMove();
        }

        transform.position = new Vector3(transform.position.x, transform.position.y - (moveSpeed / 2), transform.position.z);
        base.Update();
        
    }

    void ResetMove()
    {
        cpt = UnityEngine.Random.Range(10, 500);
        delta = UnityEngine.Random.Range(-moveSpeed / 2, moveSpeed / 2);
        if (delta < (moveSpeed / 4) && delta > (-moveSpeed / 4))
            delta *= 2;
        //deltaY = UnityEngine.Random.Range (- moveSpeed, 0.0f);

        if (resistance == 2)
        {
            cpt /= 3;
            delta *= 2;
            //	deltaY -= (moveSpeed / 3);
        }
        else if (resistance == 1)
        {
            cpt /= 10;
            delta *= 3;
            //	deltaY -= (moveSpeed / 2);
        }
    }

    public override void Fire()
    {
        Instantiate(projectilePref,
            new Vector3(transform.position.x,
            transform.position.y + 1,
            transform.position.z), Quaternion.identity);

        Instantiate(projectilePref,
            new Vector3(transform.position.x + 1,
            transform.position.y + 1,
            transform.position.z), Quaternion.identity);

        Instantiate(projectilePref,
            new Vector3(transform.position.x - 1,
            transform.position.y + 1,
            transform.position.z), Quaternion.identity);
    }
}
