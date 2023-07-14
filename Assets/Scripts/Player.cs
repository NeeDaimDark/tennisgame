using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   #region Variables
    bool hitting;
    public Transform aimTarget;
    public float speed = 6f;
    public float force = 13f;
    public Transform ball;
    Animator animator;
    Vector3 aimTargetInitialPosition;
    ShotManager shotManager;
    public Shot CurrentShot;

    [SerializeField] Transform serveRight;
    [SerializeField] Transform serveLeft;

    bool servedRight= true;

    #endregion

    #region functions

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        aimTargetInitialPosition =aimTarget.position;
        shotManager = GetComponent<ShotManager>();
        Shot currentShot = shotManager.topSpin;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        
        if (Input.GetKeyDown(KeyCode.F))
        { 
            hitting = true;
            CurrentShot =shotManager.topSpin;
          

        }else if (Input.GetKeyUp(KeyCode.F))
                {
            hitting = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            hitting = true;
            CurrentShot = shotManager.flat;
            
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            hitting = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            hitting = true;
            ball.transform.position = transform.position + new Vector3(0, 1, -0.5f);
            animator.Play("backhand");
            ball.GetComponent<ball>().hitter = "player";
            ball.GetComponent<ball>().playing = true;




        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            hitting = false;
            CurrentShot = shotManager.flatServe;


        }


        if (hitting)
        {
            aimTarget.Translate(new Vector3(-h, 0, 0) * speed * 2 * Time.deltaTime);
           
        }
        if ((h!=0 || v !=0 ) && !hitting)
        {
           

            Vector3 movement = new Vector3(v, 0f, -h).normalized;
            transform.Translate(movement * speed * Time.deltaTime);
        }
        
        
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Ball"))
    //    {
    //        Vector3 dir = aimTarget.position - transform.position;
    //        other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 8, 0) ;

    //    }
    //}



 

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                Vector3 dir = aimTarget.position - transform.position;
                other.gameObject.GetComponent<Rigidbody>().velocity = dir.normalized * CurrentShot.hitForce + new Vector3(0, CurrentShot.upForce, 0);
                Vector3 balldir = ball.position - transform.position;
                if (balldir.x >= 0)
                {
                    animator.Play("backhand");
                }
                else
                {
                    animator.Play("forehand");
                }
            ball.GetComponent<ball>().hitter = "player";
            aimTarget.position = aimTargetInitialPosition;
            }    
            
            
        }


        public void Reset()
    {   if(servedRight)
            transform.position = serveLeft.position;
        else 
            transform.position = serveRight.position;

        servedRight = !servedRight;

    }


    #endregion
}
