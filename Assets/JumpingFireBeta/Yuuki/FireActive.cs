using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireActive : InitializeVariable
{

    bool CountFlg = false;
    [SerializeField] private float Speed;

    [SerializeField] private GameObject FirePlayer;
    [SerializeField] private Material M_Fire;
    private Material Temp_M_Fire;

    private int SetFrameCount = 10;

    private Vector3 StartPos;
    private Vector3 MovePos;

    private GameObject Eff_Fire_Fix;

    // Use this for initialization
    void Start () {
        StartPos = FirePlayer.transform.position;
        MovePos = StartPos - this.transform.position;

        M_Fire.color = GetComponent<Renderer>().material.color;
        Temp_M_Fire = M_Fire;
        M_Fire.color = new Color(0, 0, 0, 0f);   //プレイヤーを透明化


        Eff_Fire_Fix = FirePlayer.transform.GetChild(1).gameObject;
        Eff_Fire_Fix.SetActive(false);
        Debug.Log(Eff_Fire_Fix);
	}

    // Update is called once per frame
    void Update()
    {
        DownFire();
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
            SetFlg();
            this.transform.position = new Vector3(transform.position.x, StartPos.y, transform.position.z);

            M_Fire.color = new Color32(255, 255, 255, 255);

            CountFlg = true;

            
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
}
