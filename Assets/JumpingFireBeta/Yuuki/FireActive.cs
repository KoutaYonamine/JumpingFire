using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireActive : InitializeVariable
{
    private Rigidbody Rigid;

    bool CountFlg = false;
    [SerializeField] private float Speed;

    [SerializeField] private GameObject FirePlayer;
    [SerializeField] private Material M_Fire;
    private Material Temp_M_Fire;

    private int SetFrameCount = 10;

    private Vector3 StartPos;
    private Vector3 MovePos;

    private GameObject Eff_Fire_Fix;

    private float BoundForce = 20.0f;
    private bool CheckBound = false;

    // Use this for initialization
    void Start () {
        Rigid = GetComponent<Rigidbody>();

        StartPos = FirePlayer.transform.position;
        MovePos = StartPos - this.transform.position;

        M_Fire.color = GetComponent<Renderer>().material.color;
        Temp_M_Fire = M_Fire;
        M_Fire.color = new Color(0, 0, 0, 0f);   //プレイヤーを透明化


        Eff_Fire_Fix = FirePlayer.transform.GetChild(1).gameObject;
        Eff_Fire_Fix.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        //    DownFire();
        if(!Rigid.useGravity)//重力がFalseなら
            BoundForce = BoundForce - 4.0f;

        if (CheckBound) {
            SetFlg();
            M_Fire.color = new Color32(255, 255, 255, 255);
            CountFlg = true;
        }

        if(CountFlg == true)
        {
            SetActive();
        }
    }

    void DownFire()
    {
        if (StartPos.y < this.transform.position.y)
        {
            transform.Translate(Vector3.down * Speed);
        }
        else if (StartPos.y >= this.transform.position.y)
        {
            //Rigid.AddForce(Vector3.up * BoundForce, ForceMode.Impulse);
            //BoundForce = BoundForce - 4.0f;
            //if (BoundForce < 0)
            //   CheckBound = true;
        //SetFlg();
        //this.transform.position = new Vector3(transform.position.x, StartPos.y, transform.position.z);//プレイヤーのポジションに移動

        //M_Fire.color = new Color32(255, 255, 255, 255);

        //CountFlg = true;
        }

    }

    void SetFlg()
    {
        Eff_Fire_Fix.SetActive(true);
    }

    void SetActive()
    {
        if (SetFrameCount == 0)
        {
            moveflag = true;
            Staflag = false;
            this.gameObject.SetActive(false);
        }
        SetFrameCount--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StartCandle") {
            Rigid.useGravity = false;//AddForceで上にあげるために重力を解除
            Rigid.AddForce(Vector3.up * BoundForce, ForceMode.Impulse);

            if (BoundForce < 0) {//BoundForceが0以下になるとバウンド処理を終了
                CheckBound = true;
                Rigid.isKinematic = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "StartCandle") {
            Rigid.useGravity = true;//AddForceで上げたFireを下に落とすために重力を設定
        }
    }
}
