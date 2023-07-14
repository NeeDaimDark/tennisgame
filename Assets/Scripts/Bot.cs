using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    #region Variables
    public float speed = 5f;
    public float force = 13f;
    Animator animator;
    public Transform ball;
    public Transform aimTargetBot;
    public Transform[] targets;
    Vector3 targetPosition;


    public ShotManager shotManager;

    #endregion


    #region functions
    // Start is called before the first frame update
    
    void Start()
    {
       targetPosition = transform.position;
        animator = GetComponent<Animator>();
        shotManager = GetComponent<ShotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }
    void Move()
    {
        targetPosition.x = ball.position.x;
        transform.position = Vector3.MoveTowards(transform.position , targetPosition , speed * Time.deltaTime);
    }
    Vector3 PickTarget()
    {
        int randomValue = Random.Range(0, targets.Length);
        return targets[randomValue].position;
    }

    Shot PickShot()
    {
        int randomValue = Random.Range(0, 2);
        if( randomValue == 0 )
            return shotManager.topSpin;
        else 
            return shotManager.flat;
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Shot currentShot = PickShot();
            Vector3 dir = PickTarget() - transform.position;
            other.gameObject.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
            Vector3 balldir = ball.position - transform.position;
            if (balldir.x >= 0)
            {
               animator.Play("backhand");
            }
            else
               animator.Play("forehand");
        }
        ball.GetComponent<ball>().hitter = "bot";
    }
    #endregion
}
