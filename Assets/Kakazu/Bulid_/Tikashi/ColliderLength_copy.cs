using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLength_copy : InitializeVariable
{
    AudioSource[] audiosource; //オーディオソースサウンド
    AudioClip HitSounds; //中心に当たった時のサウンド
    AudioClip NotSounds; //中心を外した時のサウンド
    AudioClip BoneFireSounds;   //ファイヤーサウンド

    private Vector3 P_Position; //プレイヤーのポジション
    private Vector3 Difference; //プレイヤーと燭台の差分

    private GameObject PlayerObj; //プレイヤーオブジェクト格納

    private CS_Player_copy CsPlayer; //CS_Playerをゲットコンポーネント
    private Rigidbody RigidPlayer;//Rigidbodyをゲットコンポーネント

    private float Length = 2.0f; //範囲チェック(inspectorで変更可能)
    private float Magnitude; //プレイヤーと燭台の距離

    private GameObject targetObject; //カメラを格納

    [SerializeField] float DifferenceY;

    private ParticleSystem BoneFire;
    [SerializeField] private float ParticleStartDilay = 0.1f;

    private bool BoneFireflag;
    private bool Fireflag = false;

    // Use this for initialization
    void Start () {
        audiosource = GetComponents<AudioSource>();  //サウンド
        HitSounds = audiosource[0].clip;    //サウンド
        NotSounds = audiosource[1].clip;    //サウンド
        //BoneFireSounds = audiosource[2].clip; //サウンド

        BoneFire = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        BoneFire.Stop();
        BoneFire.startDelay = ParticleStartDilay;

        targetObject = GameObject.Find("Main Camera");
        this.transform.LookAt(new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z)); //燭台をカメラに向ける

        PlayerObj = GameObject.Find("Fire"); //プレイヤーを格納
        CsPlayer = PlayerObj.GetComponent<CS_Player_copy>();
        RigidPlayer = PlayerObj.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update () {
        if (StopBoneFire)
            BoneFire.Stop();//燭台の炎を止める

        
    }

    public bool getBoneFire(){
        return BoneFire;
    }
    public bool getFireflag(bool a){
        Fireflag = a;
        return Fireflag;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") //プレイヤーが触れたら
        {
            BoneFire.Play();//炎のParticleをアクティブに
            //audiosource[2].Play();//サウンド

            P_Position = collision.transform.position; //プレイヤーの座標を代入
            Difference = P_Position - transform.position; //差分
            Magnitude = Difference.magnitude;

            LengthCheck(); //Playerが燭台の中心に乗ったかどうか
            //PlayerObj.GetComponent<CS_Player_copy>().UpSpeedCandleCenterHit();//PlayerのスピードをUp /***今は使われていない***/
        }
        if (P_Position.y > transform.position.y + DifferenceY) {//燭台より上
            if (PlayerObj.GetComponent<CS_Player_copy>().clickflg != 99 && PlayerObj.GetComponent<CS_Player_copy>().clickcount > 10) {
                PlayerObj.GetComponent<CS_Player_copy>().initialize = true; //燭台に乗ったらtrue
                Debug.Log("燭台に乗ったら");
            } else if (PlayerObj.GetComponent<CS_Player_copy>().clickflg == 99)


            RigidPlayer.velocity = Vector3.zero;//velocityをゼロに
            Fireflag = true;
            //getFireflag(true);
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        
        Invoke("AudioSourceStop", 4);
    }

    private void AudioSourceStop()
    {
        //audiosource[2].Stop();//サウンド
    }

    private void LengthCheck() //フラグ切り替え
    {
        if(Magnitude <= Length)
        {
            audiosource[0].PlayOneShot(HitSounds); //サウンド

            CsPlayer.addspeed = true;
        }
        else if(Magnitude > Length)
        {
            audiosource[1].PlayOneShot(NotSounds); //サウンド 端っこ

            CsPlayer.addspeed = false;
        }
    }
}
