using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhraseBrickCtrl : MonoBehaviour
{
    public Rigidbody rb;

    public bool isTouched;// = true; // For the initial collision.

    public Color onColor, offColor;
    private Renderer myRenderer;

    private MeshRenderer cubeRenderer;

    private AudioSource snd;
    //private AudioClip hitSound;
    public AudioClip hitSound;

    public int brickID = 0;


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
        cubeRenderer = GameObject.Find("PhraseBrick").GetComponent<MeshRenderer>();
        cubeRenderer.material.mainTexture = Resources.Load<Texture>("Textures/m2820_1");


        // Sound Effect
        this.snd = this.gameObject.AddComponent<AudioSource>();
        this.snd.clip = this.hitSound;
        this.snd.loop = false;
        //컴포넌트에 Jump Sound가 나타나는데요, 어셋에 임포트한 음원파일을 드래그&드롭 하시면 끝!! 

        // 2020.10.29. Hit 시 크기 변경을 위해서.        
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
            this.snd.Play();

            //Debug.Log("Seeker hit the birck #1!");

            //isTouched = true;
            //ProcHit();
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

        //isTouched = !isTouched;

        //ProcHit();

        //Debug.Log("Brick # 1 is hit!");
    }

    public void ProcHit()
    {

        //this.snd.Play();

        // In order to show it is touched or not.
        if( isTouched )
        {
            
            // 1103 myRenderer.material.color = onColor;
            //transform.localScale *= 0.95f; // new Vector3(1f, 0.5f, 0.5f);
            //transform.localScale = onSize;
            //rb.AddForce(new Vector3( 0f, 100000f, 0f)); // 귀엽게 살짝 튀어 오르는 정도. 

            //cubeRenderer.material.mainTexture = Resources.Load<Texture>("Textures/a1_kr");


        }else
        {
            myRenderer.material.color = offColor;
            //transform.localScale = offSize;

            //ProcJump();

            //cubeRenderer.material.mainTexture = Resources.Load<Texture>("Textures/a1");

        }


    }

    
    // 점프를 시키면 자연스럽게 콜리전도 일어나고, 색도 원복 되고, 크기도 작아진다. 
    public void ProcJump()
    {
            // transform.localScale *= 0.98f; // new Vector3(1f, 0.5f, 0.5f);
            //transform.localScale = onSize;
            rb.AddForce(new Vector3( 0f, 100000f, 0f)); // 귀엽게 살짝 튀어 오르는 정도.        
    }

}
