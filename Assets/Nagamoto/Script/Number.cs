using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour {

    public Sprite[] numimg;                             //ナンバーイメージ格納用
    public List<int> num = new List<int>();             //数値を入れる配列
    private Image img;                                  //数値の画像
    private RectTransform nummove;                      //Numberの移動
    private int Candlestick;                            //進んだ燭台の数
    private bool candlestickflag;                       //燭台用フラグ
    private bool numflag = false;
    private int numten = 0;                             //number10出現数制限

    // Use this for initialization
    void Start () {
        //num = Resources.LoadAll<Sprite>("Image/number");//ナンバーイメージをすべて読み込む
        img = GameObject.Find("Number").GetComponent<Image>();              //ナンバーイメージの取得
        nummove = GameObject.Find("Number").GetComponent<RectTransform>();  //Numberの移動
        Candlestick = GameObject.Find("Fire").GetComponent<Stairscollision>().getCandlestick();//進んだ燭台の数
        //View(Candlestick);
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
            RectTransform numberimage = (RectTransform)Instantiate(GameObject.Find("Number")).transform;
            if(i == 1){
                numberimage.name = "Number10";
                numberimage.localPosition += new Vector3(50, 0, 0);
                numten += 1;
            }
            else if(i == 2){
                numberimage.name = "Number100";
                numberimage.localPosition += new Vector3(50, 0, 0);
            }
            numberimage.SetParent(this.transform, false);
            numberimage.localPosition = new Vector2(
                numberimage.localPosition.x - numberimage.sizeDelta.x * i,
                numberimage.localPosition.y);
            numberimage.GetComponent<Image>().sprite = numimg[num[i]];
            //移動
            if (numflag == false){
                nummove.localPosition += new Vector3(50, 0, 0);
                numflag = true;
            }
            RectTransform a = GameObject.Find("Number10").GetComponent<RectTransform>();
            //名前がNumber100だった場合
            if(numberimage.name == "Number100"){
            a.localPosition += new Vector3(50, 0, 0);
            }
        }
    }

    // Update is called once per frame
    void Update () {
        Candlestick = GameObject.Find("Fire").GetComponent<Stairscollision>().getCandlestick();//進んだ燭台の数
        //View(Candlestick);
    }
}
