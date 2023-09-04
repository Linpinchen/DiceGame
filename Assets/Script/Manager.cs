using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    int Runindex;
    public LightMove[] LightMoves;
    public PlayerControl _PlayerControl;
    public Date _Date;
    public Show _Show;
    IEnumerator CoRunLight;
    IEnumerator _Coshow;
    public bool B_startgame;
    public bool B_COShow;
    public Image Showimage;
   

    void Start()
    {
        init();

        _PlayerControl.Init_Control(_Date);
        
    }

    
    void Update()
    {

        if (B_COShow&& LightMoves[LightMoves.Length - 1].MoveEnd == true)
        {
            B_COShow = false;
            _Coshow = CoShow();
            StartCoroutine(_Coshow);

        }


    }


    public void init()
    {
        B_startgame = true;
        B_COShow = false; 
        Runindex = 3;
      
        for (int i=0;i<LightMoves.Length;i++)
        {

            LightMoves[i].init(Runindex);


        }

        int BigSmall = _PlayerControl.BT_BigSmall.Length;
        int Double = _PlayerControl.BT_Double.Length;
        int Triple = _PlayerControl.BT_Triple.Length;
        int DoubleNumbers = _PlayerControl.BT_DoubleNumbers.Length;
        int Single = _PlayerControl.BT_Single.Length;
        int Totles = _PlayerControl.BT_Totals.Length;

        _Date = new Date(LightMoves.Length, BigSmall, Double, Triple, Single,DoubleNumbers, Totles,100) ;

        _PlayerControl.StartAction = StartGame;
        _PlayerControl.CoinText.text = "玩家金額 :" + _Date.PlayerCoin;
        _PlayerControl.bet_Text.text = _Date.PlayerBet.ToString();

        _Show = new Show(Showimage);

    }

    public IEnumerator CoRunLights(LightMove[] lights,int [] Theindex)
    {
        B_startgame = false;

        for (int i =0;i<lights.Length;i++)
        {
            
            lights[i].MoveEnd = false;
            lights[i].rest();
        }

        yield return new WaitForSeconds(0.2f);

        foreach (var i in lights)
        {
            int index = System.Array.IndexOf(lights,i);
            int Runnumber = Theindex[index];
            IEnumerator Ie;
            Ie = i.Move(Runnumber);
            StartCoroutine(Ie);

            yield return new WaitForSeconds(1);

        }


    }

    public void StartGame()
    {

        if (B_startgame)
        {
            if (!_PlayerControl.SetNumber)
            {
                _Date.ChangeNumbers();
                
            }
            _Date.DiceTotles = _Date.DiceTotal();

            _Date.NumberJudge();

            CoRunLight = CoRunLights(LightMoves, _Date.ShowNumbers);

            StartCoroutine(CoRunLight);

            //_Coshow = CoShow();
            //StartCoroutine(_Coshow);
            B_COShow = true;
            _PlayerControl.SetNumber = false;
        }


    }


    public IEnumerator CoShow()
    {
        IEnumerator _Coshow = _Show.Show_bulletin(_Date);
        //StartCoroutine(_Coshow);

        yield return StartCoroutine(_Coshow);
        //yield return new WaitForSeconds(2);

        _Show.ShowImage.gameObject.SetActive(false);
        _Date.PlayerCoin += _Date.WinCoin;
        _PlayerControl.CoinText.text = "玩家金額 :" + _Date.PlayerCoin;


        B_startgame = true;
        _Date.WinCoin = 0;


        for (int i=0;i<_Date.BetDisk.Count;i++)
        {
            for (int j=0;j<_Date.BetDisk[i].BetTypes.Length;j++)
            {

                _Date.BetDisk[i].BetTypes[j] = 0;


            }

        }

        for (int i =0;i<_PlayerControl._Imgs.Count;i++)
        {

            for (int j=0;j<_PlayerControl._Imgs[i].Txts.Count;j++)
            {

                _PlayerControl._Imgs[i].Txts[j].text = "0";

            }



        }

        yield return null;

    }
    

}
