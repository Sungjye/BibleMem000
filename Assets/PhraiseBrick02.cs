using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhraiseBrick02 : MonoBehaviour
{
    //1109 private Rigidbody rb;
    public Rigidbody rb;

    public bool isTouched; // For the initial collision. // 1103

    public Color onColor, offColor;
    private Renderer myRenderer;

    private MeshRenderer cubeRenderer;

    public int brickID = 1;

    // 2020.10.29. Hit 시 크기 변경을 위해서.
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
        cubeRenderer = GameObject.Find("PhraseBrick02").GetComponent<MeshRenderer>();
        cubeRenderer.material.mainTexture = Resources.Load<Texture>("Textures/m2820_2");

        // 2020.10.29. Hit 시 크기 변경을 위해서.   // 1103     
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
                transform.localScale *= 0.95f; // On 되어 있는데 자꾸 치면 커져서 못지나가게.

                Debug.Log("Brick 2: * 1.1f "+ isTouched);
                    
            }else
            {
                isTouched = true;

                myRenderer.material.color = onColor;

                Debug.Log("Brick 2: * OnColor "+ isTouched);
            }
            //ProcHit();
        }


        /*
        GetComponent<AudioSource>().Play();

        isTouched = !isTouched;


        ProcHit();
        */
        //Debug.Log("Brick # 2 is hit!");
    }



    public void ProcHit()
    {

        // In order to show it is touched or not.
        if( isTouched )
        {

            //GetComponent<AudioSource>().Play(); // 1103
            //transform.localScale = onSize; // 1103 // 이부분 떄문에 크기가 항상 On 크기로 고정됨.. 계속 불리니.. 1106

            //myRenderer.material.color = onColor;
            //transform.localScale *= 0.95f; 

            //cubeRenderer.material.mainTexture = Resources.Load<Texture>("Textures/a2_kr");


        }else
        {
            myRenderer.material.color = offColor;
            //transform.localScale *= 1.05f; // 웃긴 효과!!!
            //cubeRenderer.material.mainTexture = Resources.Load<Texture>("Textures/a2");

        }


    }

    public void ProcJump()
    {
            // transform.localScale *= 0.98f; // new Vector3(1f, 0.5f, 0.5f);
            //transform.localScale = onSize;
            rb.AddForce(new Vector3( 0f, 100000f, 0f)); // 귀엽게 살짝 튀어 오르는 정도. 
            // 1000000f 하늘로 날아감. 좀더 운치 있게 뛰기. 150000f       
    }




}
