using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : InitializeVariable{

    public Sprite[] numimg;                             //ナンバーイメージ格納用
    public List<int> num = new List<int>();             //数値を入れる配列
    private Image img;                                  //数値の画像
    private RectTransform nummove;                      //Numberの移動
    private bool candlestickflag;                       //燭台用フラグ
    private bool numflag = false;
    private GameObject num10;                           //10桁目オブジェクト
    private GameObject num100;                          //100桁目オブジェクト
    private Stairscollision sta;                        //階段スクリプト

    // Use this for initialization
    void Start () {
        img = GameObject.Find("Number").GetComponent<Image>();              //ナンバーイメージの取得
        nummove = GameObject.Find("Number").GetComponent<RectTransform>();  //Numberの移動
        sta = GameObject.Find("Fire").GetComponent<Stairscollision>();
        numberobject = GameObject.Find("Number");       //ナンバーのオブジェクト取得
    }

    public void View(int move){
        var digit = move;
        //要素数0には１桁目の値が格納
        num = new List<int>();
        if (digit == 0){
            num.Add(move);
        }
        while (digit != 0){ 
            move = digit % 10;
            digit = digit / 10;
            num.Add(move);
        }
        img.sprite = numimg[num[0]];
        for (int i = 1; i < num.Count; i++){
            //複製
            if (numflag == true){
                Destroy(num10);
                if(i == 2){
                    Destroy(num100);
                }
            }
            RectTransform numberimage = (RectTransform)Instantiate(GameObject.Find("Number")).transform;
            numberimage.SetParent(this.transform, false);
            if(i == 1){
                numberimage.localPosition += new Vector3(-30, 0, 0);
                numdigit = 1;
                numberimage.name = "Num10";
            }
            if(i == 2){
                numberimage.localPosition += new Vector3(-60, 0, 0);
                numdigit = 2;
                numberimage.name = "Num100";

            }
            numberimage.GetComponent<Image>().sprite = numimg[num[i]];
            numflag = true;
        }
    }

    //スコアの移動
    public void Result(){
        //10の位
        if (numdigit == 1){
            numberobject.transform.localScale = new Vector3(0.75f, 0.75f, 0);           //数字の大きさを変える
            numberobject.transform.localPosition = new Vector3(50, 95, 0);               //数字の位置を変える
            num10.transform.localScale = new Vector3(0.75f, 0.75f, 0);
            num10.transform.localPosition = new Vector3(-50, 95, 0);
        }
        //100の位
        else if (numdigit == 2){
            numberobject.transform.localScale = new Vector3(0.75f, 0.75f, 0);           //数字の大きさを変える
            numberobject.transform.localPosition = new Vector3(80, 95, 0);               //数字の位置を変える
            num10.transform.localScale = new Vector3(0.75f, 0.75f, 0);
            num10.transform.localPosition = new Vector3(0, 95, 0);
            num100.transform.localScale = new Vector3(0.75f, 0.75f, 0);
            num100.transform.localPosition = new Vector3(-80, 95, 0);
        }
        else{
            numberobject.transform.localScale = new Vector3(0.75f, 0.75f, 0);           //数字の大きさを変える
            numberobject.transform.localPosition = new Vector3(0, 95, 0);               //数字の位置を変える
        }
    }

    // Update is called once per frame
    void Update () {
        num10 = GameObject.Find("Num10");
        num100 = GameObject.Find("Num100");
        if (sta.getmouseflag() == false){
            Destroy(num10);
            Destroy(num100);
        }
    }
}
