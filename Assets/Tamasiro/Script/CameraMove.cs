using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField]
    private Transform pivot;       //プレイヤーのTransform取得
    private GameObject maincamera;
    private Transform sectionAreaLine;     //セクションエリアラインにアクセス用変数
    [SerializeField] private float CameraRiseSpeed = 20.0f;
    Camera cam;     //カメラコンポーネントにアクセス用変数
    RectTransform SectionAreaLine;     //  キャンバスコンポーネントにアクセス用変数

    private int Lookright   = 3;          //プレイヤーから右を見る
    private int Lookup      = -2;         //プレイヤーから上を見る



    //private GameObject player;

    // Use this for initialization
    void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");

        maincamera = GameObject.Find("Main Camera");
        sectionAreaLine = maincamera.transform.GetChild(1);
        cam = GetComponent<Camera>();       //カメラコンポーネントにアクセス
        //SectionAreaLine = sectionAreaLine.GetComponent<RectTransform>();       //キャンバスコンポーネントにアクセス

        this.transform.position = new Vector3(this.transform.position.x, pivot.position.y + 6.0f, this.transform.position.z);
    }

    // Update is called once per frame
    void Update () {
        Vector3 SectionAreaLinescreenPos = cam.WorldToScreenPoint(sectionAreaLine.position);
        //Debug.Log("SectionAreaLinescreen is" + SectionAreaLinescreenPos.x + "pixels from the left");
        Vector3 CamerascreenPos = cam.WorldToScreenPoint(pivot.position);
        //Debug.Log("pivot is" + CamerascreenPos.x + "pixels from the left");

        if (SectionAreaLinescreenPos.y <= CamerascreenPos.y)
        {
            this.transform.Translate(Vector3.up * CameraRiseSpeed * Time.deltaTime, Space.World);
        }
    }
    private void LateUpdate()
    {
        

        Vector3 LookPos = new Vector3(pivot.position.x, transform.position.y - 6.0f, pivot.position.z);
        transform.LookAt(LookPos + transform.right * Lookright + transform.up * Lookup);

        //transform.position = new Vector3(transform.position.x, pivot.position.y + 6.0f, transform.position.z);
        //transform.LookAt(pivot.position + transform.right * Lookright + transform.up * Lookup);
    }
}


/***************************************************************************************
        Vector3 toTargetVec = transform.position - pivot.position;
        float sqrLength = toTargetVec.sqrMagnitude;

         設定した範囲内なら更新しない。
         magnitudeはルート計算が重いので、二乗された値を利用しよう。
        if (sqrLength <= m_waitRange * m_waitRange)
        {
            return;
        }
         ターゲットの位置から指定範囲内ギリギリの位置を目指すようにするよ。
        Vector3 targetPos = pivot.position - toTargetVec.normalized * m_waitRange;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.5f);
*******************************************************************************************/

//float SectionDistance = Vector3.Distance(transform.position, pivot.position);
//float SectionDistance = transform.position.y - pivot.position.y;
//if (SectionDistance >= 5.0f && 6.0f >= SectionDistance)
//{
//    return;
//}
//else if (SectionDistance < 5.0f)
//{
//    transform.position = new Vector3(transform.position.x, pivot.position.y + SectionDistance, transform.position.z);
//}
//else if(SectionDistance > 6.0f)
//{
//    transform.position = new Vector3(transform.position.x, pivot.position.y + 6.0f, transform.position.z);
//}
//Debug.Log("pivot.position.y:" + pivot.position.y);
//Debug.Log("transform.position.y:" + transform.position.y);
//Debug.Log("transform.position.y - pivot.position.y :" + (transform.position.y - pivot.position.y));
//Debug.Log("SectionDistance:" + SectionDistance);