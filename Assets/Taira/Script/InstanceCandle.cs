using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceCandle : MonoBehaviour {

    private GameObject Player;

    [SerializeField]
    private GameObject createObject; // 生成するオブジェクト

    [SerializeField]
    private int itemCount = 100; // 生成するオブジェクトの数

    [SerializeField]
    private float radius = 5f; // 半径 *プレイヤーとカメラの距離

    [SerializeField]
    private float repeat = 5f; // 何周期するか

    [SerializeField]
    private float length = 50f; // y軸の長さ

    void Start()
    {
        Player = GameObject.Find("Fire");
        radius = Player.transform.position.magnitude - 0.5f;
        Debug.Log(radius);


        var oneCycle = 2.0f * Mathf.PI; // sin の周期は 2π
        var oneLength = length / itemCount; // y軸の1単位
        var y = transform.position.y - oneLength; // y軸初期位置 (生成前に足しこみをしているので、一回分引いておく)


        for (var i = 0; i < itemCount; ++i)
        {

            var point = ((float)i / itemCount) * oneCycle; // 周期の位置 (1.0 = 100% の時 2π となる)
            var repeatPoint = point * repeat; // 繰り返し位置

            var x = Mathf.Sin(repeatPoint) * radius;
            var z = Mathf.Cos(repeatPoint) * radius;
            y += oneLength;

            var position = new Vector3(x, y, z);

            Instantiate(
                createObject,
                position,
                Quaternion.identity,
                transform
            );
        }

    }

    //[SerializeField]
    //public GameObject createObject; // 生成するオブジェクト

    //[SerializeField]
    //private int itemCount = 10; // 生成するオブジェクトの数

    //[SerializeField]
    //private float radius = 5f; // 半径

    //[SerializeField]
    //private float repeat = 2f; // 何周期するか

    //void Start()
    //{

    //    var oneCycle = 2.0f * Mathf.PI; // sin の周期は 2π

    //    for (var i = 0; i < itemCount; ++i)
    //    {

    //        var point = ((float)i / itemCount) * oneCycle; // 周期の位置 (1.0 = 100% の時 2π となる)
    //        var repeatPoint = point * repeat; // 繰り返し位置

    //        var x = Mathf.Cos(repeatPoint) * radius;
    //        var y = Mathf.Sin(repeatPoint) * radius;

    //        var position = new Vector3(x, y);

    //        Instantiate(
    //            createObject,
    //            position,
    //            Quaternion.identity,
    //            transform
    //        );

    //    }


    //}

    //private GameObject[] InstanceCandleStick;//生成するオブジェクトを代入
    //private float _x, _y, _z;//生成位置の変数
    //private float Difference;//中心からの距離  *Playerとカメラの距離を利用
    //private float Length;//距離
    //private float Angle;//角度    *燭台と燭台の間隔


}
