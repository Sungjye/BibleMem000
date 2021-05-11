using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhraiseBrick05 : MonoBehaviour // @@
{
    public Rigidbody rb;

    public bool isTouched;// = true; // For the initial collision.

    public Color onColor, offColor;
    private Renderer myRenderer;

    private MeshRenderer cubeRenderer;

    public int brickID = 4; // @@

// 2020.10.29. Hit 시 크기 변경을 위해서. 1103
    private Vector3 onSize, offSize;

    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
        //rb.SetActive(false);
        //rb.enabled = false;
        myRenderer = GetComponent<Renderer>();

        offColor = myRenderer.material.color;
        onColor = Color.yellow;

        // Set the initial texture image. 2020.09.28
        cubeRenderer = GameObject.Find("PhraseBrick05").GetComponent<MeshRenderer>();
        cubeRenderer.material.mainTexture = Resources.Load<Texture>("Textures/m2820_5");     // @@  

                        // 2020.10.29. Hit 시 크기 변경을 위해서. 1103
        offSize = transform.lossyScale;
        onSize = 0.5f*offSize;
        isTouched = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {

        if( collision.gameObject.name == "Seeker" ) // 1103
        {
            GetComponent<AudioSource>().Play();

            if( isTouched == true )
            {
                ; // 뭔가 켜져 있는데, 계속 치면... 
                transform.localScale *= 0.95f;
            }else
            {
                isTouched = true; // 켜주고. 
                myRenderer.material.color = onColor;
            }            
        }

    }

    public void ProcHit()
    {
        // In order to show it is touched or not.
        if( isTouched )
        {
            ;//myRenderer.material.color = onColor;

            //cubeRenderer.material.mainTexture = Resources.Load<Texture>("Textures/a3_kr");


        }else
        {
            myRenderer.material.color = offColor;

            //cubeRenderer.material.mainTexture = Resources.Load<Texture>("Textures/a3");

        }


    }

    public void ProcJump()
    {
            // transform.localScale *= 0.98f; // new Vector3(1f, 0.5f, 0.5f);
            //transform.localScale = onSize;
            rb.AddForce(new Vector3( 0f, 100000f, 0f)); // 귀엽게 살짝 튀어 오르는 정도.        
    }


}
