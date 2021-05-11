using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject phraise_01, phraise_02, phraise_03, phraise_04, phraise_05, phraise_06, win_msg;
    public GameObject phraise_07, phraise_08, phraise_09, phraise_10, phraise_11; 
    public PhraseBrickCtrl brick_01;
    public PhraiseBrick02 brick_02;
    public PhraiseBrick03 brick_03;
    public PhraiseBrick04 brick_04;
    public PhraiseBrick05 brick_05;
    public PhraiseBrick06 brick_06;
    public PhraiseBrick07 brick_07;
    public PhraiseBrick08 brick_08;
    public PhraiseBrick09 brick_09;
    public PhraiseBrick10 brick_10;
    public PhraiseBrick11 brick_11;

    //public bool isTouched_Brick_01;

    /*
    public bool[] orderChecker = new bool[2] {false, false};
       for(int i = 0; i<testArray.Length;i++)
       {
           if(testArray[i] == val)
               count++;
       }
       */


    // 순서를 글로벌 하게 체크 하는 로직. 2020.11.03
    // 후보군 2~3개를 점프하게 만들어 줘서, 다음 구절을 찾을 수 있게 가이드 하게 하기 위해
    enum B_State {Off, Candidate, On};
    List<B_State> brickState = new List<B_State>();



    // 1106
    private int numOfCandidate = 3; // 점프하는 후보들의 갯수. 배열 노가다를 줄이기 위해. 


    // 배경음과, 피날레 음악을 다르게 하기 위해. 

    private AudioSource musicPlayer; // 스피커로 생각?
    public AudioClip bgmMusic; // 음원으로 생각?
    public AudioClip finaleMusic;

    //public AudioClip[] bgmMusic;

    // Start is called before the first frame update
    void Start()
    {
        
        phraise_01 = GameObject.Find("P_01");
        phraise_02 = GameObject.Find("P_02");
        //brick_01 = GameObject.Find("PhraseBrick");

        phraise_03 = GameObject.Find("P_03");
        phraise_04 = GameObject.Find("P_04");
        phraise_05 = GameObject.Find("P_05");
        phraise_06 = GameObject.Find("P_06");
        phraise_07 = GameObject.Find("P_07");
        phraise_08 = GameObject.Find("P_08");
        phraise_09 = GameObject.Find("P_09");
        phraise_10 = GameObject.Find("P_10");
        phraise_11 = GameObject.Find("P_11");

        win_msg = GameObject.Find("P_Clear");

        phraise_01.SetActive(false);
        phraise_02.SetActive(false);
        //brick_01.SetActive(true);

        phraise_03.SetActive(false);
        phraise_04.SetActive(false);
        phraise_05.SetActive(false);
        phraise_06.SetActive(false);
        phraise_07.SetActive(false);
        phraise_08.SetActive(false);
        phraise_09.SetActive(false);
        phraise_10.SetActive(false);
        phraise_11.SetActive(false);

        win_msg.SetActive(false);

        //Debug.Log(brick_01.isTouched);


        // 2020.10.08

        // Function name, time, repeat time.
        InvokeRepeating("InvokeTimer", 1f, 1.5f);


        // 2020.11.03 Order checking logic. 
        //List<B_State> brickState = new List<B_State>();

        for(int idx=0; idx < 11; idx++)
        {
            brickState.Add(B_State.Off);
        }

        brickState[0] = B_State.Candidate;
        brickState[1] = B_State.Candidate;
        brickState[2] = B_State.Candidate;
/*        brickState[3] = B_State.Candidate;
        brickState[4] = B_State.Candidate;
        brickState[5] = B_State.Candidate;
        brickState[6] = B_State.Candidate;
        brickState[7] = B_State.Candidate;
        brickState[8] = B_State.Candidate;
        brickState[9] = B_State.Candidate;
        brickState[10] = B_State.Candidate;
 */   


        // BGM during the play.
        //this.gmSpeaker = bgmMusic;
        //gmSpeaker.Play();
        //gmSpeaker.loop = true;

        musicPlayer = GetComponent<AudioSource>();

        musicPlayer.Stop();
        musicPlayer.clip = bgmMusic; //finaleMusic;
        musicPlayer.loop = true;
        musicPlayer.time = 3; // 재생 위치 초. 
        musicPlayer.volume = 0.5f; // 최대값 1
        musicPlayer.Play();





    }

    // Update is called once per frame
    void Update()
    {

        //# 종료 기능 처리. 
        // #if UNITY_ANDROID
        // #if UNITY_IOS
        if( Application.platform == RuntimePlatform.Android )
        {
            if( Input.GetKey(KeyCode.Home) )
            {
                ;
            }else if( Input.GetKey(KeyCode.Escape) ) // Back button
            {

                Application.Quit();

            }else if( Input.GetKey(KeyCode.Menu) )
            {
                ;
            }

        }


        // 2020.11.03 점프하며 글로벌 로직 체크. // 1103
        if( brick_01.isTouched )  
        {
            phraise_01.SetActive(true);

            
            brickState[0] = B_State.On;

            brickState[0+numOfCandidate] = B_State.Candidate;
            /*brickState[4] = B_State.Candidate;
            brickState[5] = B_State.Candidate;
            brickState[6] = B_State.Candidate;
            brickState[7] = B_State.Candidate;
            brickState[8] = B_State.Candidate;
            brickState[9] = B_State.Candidate;
            brickState[10] = B_State.Candidate;
                */

            brick_01.ProcHit(); // 이부분 떄문에 크기가 항상 On 크기로 고정됨.. 계속 불리니.. 1106
        }else
        {
            phraise_01.SetActive(false);
        }

        // 브릭 02
        if( brick_02.isTouched )
        {
            //FinishAction(); // test 

            phraise_02.SetActive(true);

            // 이전 순서의 브릭이 켜져 있을 떄만 유효 충돌 처리.
            if( brickState[0] == B_State.On )
            {
                brick_02.ProcHit();

                brickState[1] = B_State.On;

                brickState[1+numOfCandidate] = B_State.Candidate;
                /*brickState[5] = B_State.Candidate;
                brickState[6] = B_State.Candidate;
                brickState[7] = B_State.Candidate;
                brickState[8] = B_State.Candidate;
                brickState[9] = B_State.Candidate;
                brickState[10] = B_State.Candidate;
                */
                
            }

        }else
        {
            phraise_02.SetActive(false);
        }

        if( brick_03.isTouched )  
        {
            phraise_03.SetActive(true);

            // 이전 순서의 브릭이 켜져 있을 떄만 유효 충돌 처리.
            if( brickState[0] == B_State.On 
                && brickState[1] == B_State.On )
            {
                brick_03.ProcHit();

                brickState[2] = B_State.On;
                brickState[2+numOfCandidate] = B_State.Candidate;                
            }

        }
        else phraise_03.SetActive(false);

        if( brick_04.isTouched )  
        {
            phraise_04.SetActive(true);
        
            // 이전 순서의 브릭이 켜져 있을 떄만 유효 충돌 처리.
            if( brickState[0] == B_State.On 
                && brickState[1] == B_State.On
                && brickState[2] == B_State.On )
            {
                brick_04.ProcHit();

                brickState[3] = B_State.On;
                brickState[3+numOfCandidate] = B_State.Candidate;                
            }
        }else phraise_04.SetActive(false);

        if( brick_05.isTouched )  
        {
            phraise_05.SetActive(true);
        
            // 이전 순서의 브릭이 켜져 있을 떄만 유효 충돌 처리.
            if( brickState[0] == B_State.On 
                && brickState[1] == B_State.On
                && brickState[2] == B_State.On
                && brickState[3] == B_State.On )
            {
                brick_05.ProcHit();

                brickState[4] = B_State.On;
                brickState[4+numOfCandidate] = B_State.Candidate;                
            }
        } else phraise_05.SetActive(false);

        if( brick_06.isTouched )  
        {
            phraise_06.SetActive(true);
        
        // 이전 순서의 브릭이 켜져 있을 떄만 유효 충돌 처리.
            if( brickState[0] == B_State.On 
                && brickState[1] == B_State.On
                && brickState[2] == B_State.On
                && brickState[3] == B_State.On
                && brickState[4] == B_State.On )
            {
                brick_06.ProcHit();

                brickState[5] = B_State.On;
                brickState[5+numOfCandidate] = B_State.Candidate;                
            }
        } else phraise_06.SetActive(false);

        if( brick_07.isTouched )  
        {
            phraise_07.SetActive(true);
            
                // 이전 순서의 브릭이 켜져 있을 떄만 유효 충돌 처리.
            if( brickState[0] == B_State.On 
                && brickState[1] == B_State.On
                && brickState[2] == B_State.On
                && brickState[3] == B_State.On
                && brickState[4] == B_State.On
                && brickState[5] == B_State.On )
            {
                brick_07.ProcHit();

                brickState[6] = B_State.On;
                brickState[6+numOfCandidate] = B_State.Candidate;                
            }
        } else phraise_07.SetActive(false);

        if( brick_08.isTouched )  
        { 
            phraise_08.SetActive(true);
            // 이전 순서의 브릭이 켜져 있을 떄만 유효 충돌 처리.
            if( brickState[0] == B_State.On 
                && brickState[1] == B_State.On
                && brickState[2] == B_State.On
                && brickState[3] == B_State.On
                && brickState[4] == B_State.On
                && brickState[5] == B_State.On
                && brickState[6] == B_State.On )
            {
                brick_08.ProcHit();

                brickState[7] = B_State.On;
                brickState[7+numOfCandidate] = B_State.Candidate;                
            }
        } else phraise_08.SetActive(false);

        if( brick_09.isTouched )  
        {
            phraise_09.SetActive(true);
        // 이전 순서의 브릭이 켜져 있을 떄만 유효 충돌 처리.
            if( brickState[0] == B_State.On 
                && brickState[1] == B_State.On
                && brickState[2] == B_State.On
                && brickState[3] == B_State.On
                && brickState[4] == B_State.On
                && brickState[5] == B_State.On
                && brickState[6] == B_State.On
                && brickState[7] == B_State.On )
            {
                brick_09.ProcHit();

                brickState[8] = B_State.On;
                //brickState[8+numOfCandidate] = B_State.Candidate;                
            }

        } else phraise_09.SetActive(false);

        if( brick_10.isTouched )  
        {
            phraise_10.SetActive(true);
        // 이전 순서의 브릭이 켜져 있을 떄만 유효 충돌 처리.
            if( brickState[0] == B_State.On 
                && brickState[1] == B_State.On
                && brickState[2] == B_State.On
                && brickState[3] == B_State.On
                && brickState[4] == B_State.On
                && brickState[5] == B_State.On
                && brickState[6] == B_State.On
                && brickState[7] == B_State.On
                && brickState[8] == B_State.On )
            {
                brick_10.ProcHit();

                brickState[9] = B_State.On;
                //brickState[9+numOfCandidate] = B_State.Candidate;                
            }
        } else phraise_10.SetActive(false);

        if( brick_11.isTouched )  
        {
            phraise_11.SetActive(true);
        // 이전 순서의 브릭이 켜져 있을 떄만 유효 충돌 처리.
            if( brickState[0] == B_State.On 
                && brickState[1] == B_State.On
                && brickState[2] == B_State.On
                && brickState[3] == B_State.On
                && brickState[4] == B_State.On
                && brickState[5] == B_State.On
                && brickState[6] == B_State.On
                && brickState[7] == B_State.On
                && brickState[8] == B_State.On
                && brickState[9] == B_State.On )
            {
                brick_11.ProcHit();

                brickState[10] = B_State.On;
                //brickState[10+numOfCandidate] = B_State.Candidate;                
            }
        } else phraise_11.SetActive(false);


    }

    // 잠깐 동안 보이는 건 괜찮지만, 
    void InvokeTimer()
    {
        //Debug.Log("Invoke!");

        if( brickState[0] == B_State.Candidate ) brick_01.ProcJump();
        if( brickState[1] == B_State.Candidate ) brick_02.ProcJump();
        if( brickState[2] == B_State.Candidate ) brick_03.ProcJump(); 
        if( brickState[3] == B_State.Candidate ) brick_04.ProcJump();
        if( brickState[4] == B_State.Candidate ) brick_05.ProcJump();
        if( brickState[5] == B_State.Candidate ) brick_06.ProcJump(); 
        if( brickState[6] == B_State.Candidate ) brick_07.ProcJump();
        if( brickState[7] == B_State.Candidate ) brick_08.ProcJump(); 
        if( brickState[8] == B_State.Candidate ) brick_09.ProcJump();
        if( brickState[9] == B_State.Candidate ) brick_10.ProcJump();
        if( brickState[10] == B_State.Candidate ) brick_11.ProcJump(); 

        // 순서 맞추지 않고 켜진 것들은 꺼줘야지. 
        if( brick_01.isTouched == false )
        {
            brick_02.isTouched = false;
            brick_02.ProcHit();
            //brick_02.ProcJump();

            brick_03.isTouched = false;
            brick_03.ProcHit();

            brick_04.isTouched = false;
            brick_04.ProcHit();

            brick_05.isTouched = false;
            brick_05.ProcHit();

            brick_06.isTouched = false;
            brick_06.ProcHit();

            brick_07.isTouched = false;
            brick_07.ProcHit();

            brick_08.isTouched = false;
            brick_08.ProcHit();

            brick_09.isTouched = false;
            brick_09.ProcHit();

            brick_10.isTouched = false;
            brick_10.ProcHit();

            brick_11.isTouched = false;
            brick_11.ProcHit();
        }

        if( brick_02.isTouched == false )
        {
            brick_03.isTouched = false;
            brick_03.ProcHit();

            brick_04.isTouched = false;
            brick_04.ProcHit();

            brick_05.isTouched = false;
            brick_05.ProcHit();

            brick_06.isTouched = false;
            brick_06.ProcHit();

            brick_07.isTouched = false;
            brick_07.ProcHit();

            brick_08.isTouched = false;
            brick_08.ProcHit();

            brick_09.isTouched = false;
            brick_09.ProcHit();

            brick_10.isTouched = false;
            brick_10.ProcHit();

            brick_11.isTouched = false;
            brick_11.ProcHit();            
        }

        if( brick_03.isTouched == false ) // 3
        {
            //brick_03.isTouched = false;
            //brick_03.ProcHit();
            
            brick_04.isTouched = false;
            brick_04.ProcHit();

            brick_05.isTouched = false;
            brick_05.ProcHit();

            brick_06.isTouched = false;
            brick_06.ProcHit();

            brick_07.isTouched = false;
            brick_07.ProcHit();

            brick_08.isTouched = false;
            brick_08.ProcHit();

            brick_09.isTouched = false;
            brick_09.ProcHit();

            brick_10.isTouched = false;
            brick_10.ProcHit();

            brick_11.isTouched = false;
            brick_11.ProcHit();            
        }

        if( brick_04.isTouched == false ) //4
        {
            
            brick_05.isTouched = false;
            brick_05.ProcHit();

            brick_06.isTouched = false;
            brick_06.ProcHit();

            brick_07.isTouched = false;
            brick_07.ProcHit();

            brick_08.isTouched = false;
            brick_08.ProcHit();

            brick_09.isTouched = false;
            brick_09.ProcHit();

            brick_10.isTouched = false;
            brick_10.ProcHit();

            brick_11.isTouched = false;
            brick_11.ProcHit();            
        }

        if( brick_05.isTouched == false ) // 5
        {

            brick_06.isTouched = false;
            brick_06.ProcHit();

            brick_07.isTouched = false;
            brick_07.ProcHit();

            brick_08.isTouched = false;
            brick_08.ProcHit();

            brick_09.isTouched = false;
            brick_09.ProcHit();

            brick_10.isTouched = false;
            brick_10.ProcHit();

            brick_11.isTouched = false;
            brick_11.ProcHit();            
        }

        if( brick_06.isTouched == false ) // 6
        {

            brick_07.isTouched = false;
            brick_07.ProcHit();

            brick_08.isTouched = false;
            brick_08.ProcHit();

            brick_09.isTouched = false;
            brick_09.ProcHit();

            brick_10.isTouched = false;
            brick_10.ProcHit();

            brick_11.isTouched = false;
            brick_11.ProcHit();            
        }        

        if( brick_07.isTouched == false ) // 7
        {

            brick_08.isTouched = false;
            brick_08.ProcHit();

            brick_09.isTouched = false;
            brick_09.ProcHit();

            brick_10.isTouched = false;
            brick_10.ProcHit();

            brick_11.isTouched = false;
            brick_11.ProcHit();            
        }

        if( brick_08.isTouched == false ) // 8
        {

            brick_09.isTouched = false;
            brick_09.ProcHit();

            brick_10.isTouched = false;
            brick_10.ProcHit();

            brick_11.isTouched = false;
            brick_11.ProcHit();            
        }

        if( brick_09.isTouched == false ) // 9
        {

            brick_10.isTouched = false;
            brick_10.ProcHit();

            brick_11.isTouched = false;
            brick_11.ProcHit();            
        }

        if( brick_10.isTouched == false ) // 10
        {

            brick_11.isTouched = false;
            brick_11.ProcHit();            
        }       

  

        // Clear 메시지 보이게!
        //if( brick_01.isTouched && brick_02.isTouched && brick_03.isTouched)
        if( brick_01.isTouched && brick_02.isTouched && brick_03.isTouched && 
            brick_04.isTouched && brick_05.isTouched && brick_06.isTouched && 
            brick_07.isTouched && brick_08.isTouched && brick_09.isTouched && 
            brick_10.isTouched && brick_11.isTouched 
        )
        {
            win_msg.SetActive(true);

            Invoke("FinaleMusic", 0.5f);

             
            CancelInvoke("InvokeTimer");
            
            InvokeRepeating("FinishAction", 3f, 0.05f);
        }else
        {
            win_msg.SetActive(false);
        }


    }

    private void FinaleMusic()
    {
            //musicPlayer.clip = finaleMusic;


            musicPlayer.Stop();
            musicPlayer.clip = finaleMusic;
            musicPlayer.loop = true;
            musicPlayer.time = 2; // 재생 위치 초. 
            musicPlayer.volume = 1f; // 최대값 1
            musicPlayer.Play();

            Debug.Log("Test Music");


        CancelInvoke("FinaleMusic");
    }
    private void FinishAction() // 1109
    {

        //Debug.Log("Final Action!");
        float upSpeed = 24700f; //25000f; // 계속 호출 당하니까, 이것 만으로도.. 

        // 점프 타이며 멈추고, 애드포스 0으로 한번 주고, 
        // 새로운 떠오르기 타이머 작동 시켜서, 서서히 떠오르게 해야. 
        // 여유되면 원래 구절 순서대로 하늘로 상승. 
        // 연주 찬양도 바꾸어 주고. 
        

        // 하늘로 올라가는 피니쉬 액션.
        brick_01.rb.AddForce(new Vector3( 0f, upSpeed, 0f)); 
        brick_02.rb.AddForce(new Vector3( 0f, upSpeed, 0f));  
        brick_03.rb.AddForce(new Vector3( 0f, upSpeed, 0f));  
        brick_04.rb.AddForce(new Vector3( 0f, upSpeed, 0f));  
        brick_05.rb.AddForce(new Vector3( 0f, upSpeed, 0f));  
        brick_06.rb.AddForce(new Vector3( 0f, upSpeed, 0f));  
        brick_07.rb.AddForce(new Vector3( 0f, upSpeed, 0f));  
        brick_08.rb.AddForce(new Vector3( 0f, upSpeed, 0f));  
        brick_09.rb.AddForce(new Vector3( 0f, upSpeed, 0f));  
        brick_10.rb.AddForce(new Vector3( 0f, upSpeed, 0f));  
        brick_11.rb.AddForce(new Vector3( 0f, upSpeed, 0f));  
          
    }

}
