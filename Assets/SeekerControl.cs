using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerControl : MonoBehaviour
{
    private Rigidbody rb;

    //private float inputX, inputY, inputZ;

    private float fSpeed = 9f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        Vector3 vel = new Vector3(inputX, 0, inputZ);

        vel *= nSpeed;

        vel.y = rb.velocity.y; // restore the original y value. 

        rb.velocity = vel;
        */

        //transform.Translate(Input.GetTouch(0).deltaPosition * Time.deltaTime * 1.0f);

// 플랫폼별 컴파일. Platform Dependent Compilation

    // Seeker 공이 하늘에서 새로 떨어지게. 
    if( rb.transform.position.y < -10 )
    {
       Vector3 pos = new Vector3( 0f, 5f, 0f);
       rb.transform.position = pos; 
    }
    //Debug.Log( rb.transform.position.y );



// 위치 조작 및 이동.
    float mvx, mvz; // mvy;

#if UNITY_EDITOR
        mvx = Input.GetAxis("Horizontal");
        mvz = Input.GetAxis("Vertical");
#else
        Touch touch = Input.GetTouch(0);

        mvx = touch.deltaPosition.x * Time.deltaTime * 1.0f;
        mvz = touch.deltaPosition.y * Time.deltaTime * 1.0f; // touch.. y...

#endif


        Vector3 vel = new Vector3( mvx, 0, mvz);

        vel *= fSpeed;

        // It is disabled to slow down the falling speed.
        vel.y = rb.velocity.y; // restore the original y value. 

        rb.velocity = vel;

    // 위치 확인해서 판에서 올라올 수 없는 위치로 떨어졌으면 다시 중앙으로 떨어뜨리기. 

/*
#if UNITY_EDITOR
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        Vector3 vel = new Vector3(inputX, 0, inputZ);

        vel *= fSpeed;

        vel.y = rb.velocity.y; // restore the original y value. 

        rb.velocity = vel;
#endif

#if UNITY_ANDROID
        Touch touch = Input.GetTouch(0);

        float mvx, mvz;

        mvx = touch.deltaPosition.x * Time.deltaTime * 1.0f;
        mvz = touch.deltaPosition.y * Time.deltaTime * 1.0f; // touch.. y...

        Vector3 vel = new Vector3( mvx, 0, mvz);

        vel *= fSpeed;

        rb.velocity = vel;
#endif
*/

    }

    /*
    void OnTriggerEnter(Collider others)
    {
        if(others.tag == "MovingSeeker")
        {
            Debug.Log("Hi!");
        }

    }
    */

/*
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit!");
    }
*/
}
