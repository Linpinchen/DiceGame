using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class PlayerControl 
{

    public Button[] BT_BigSmall;
    public Button[] BT_Double;
    public Button[] BT_Single;
    public Button[] BT_Triple;
    public Button[] BT_DoubleNumbers;
    public Button[] BT_Totals;
    public Button BT_Start;
    public Button BT_AllTriple;
    public Button BT_BetPlus;
    public Button BT_BetReduce;
    public Text bet_Text;
    public Text CoinText;
    public List<Imges> _Imgs;
    public UnityAction StartAction;
    public InputField NumberIPT;
    public Button InputOK;
    public Button OKCheck;
    public bool SetNumber;
    public Button BT_Menu;
    public Image Warring_Img;
    public void Init_Control(Date date)
    {

        SetNumber = false;

        for (int i=0;i < BT_BigSmall.Length;i++)
        {
            int index = i;
            BT_BigSmall[index].onClick.AddListener(delegate
            {
                int tempi = date.PlayerCoin % date.PlayerBet;
                int tempcoin = date.PlayerCoin - tempi;
                if (tempcoin>=date.PlayerBet)
                {
                    date.BetDisk[0].BetTypes[index] += date.PlayerBet;
                    date.PlayerCoin -= date.PlayerBet;
                    CoinText.text = "玩家金額 ：" + date.PlayerCoin;
                    _Imgs[0].Txts[index].text = date.BetDisk[0].BetTypes[index].ToString();

                }
                
                
	        });

        }

        for (int i=0;i<BT_Double.Length;i++)
        {
            int index = i;
            BT_Double[index].onClick.AddListener(delegate
            {
                int tempi = date.PlayerCoin % date.PlayerBet;
                int tempcoin = date.PlayerCoin - tempi;
                if (tempcoin >= date.PlayerBet)
                {
                    date.BetDisk[1].BetTypes[index] += date.PlayerBet;
                    date.PlayerCoin -= date.PlayerBet;
                    CoinText.text = "玩家金額 ：" + date.PlayerCoin;
                    _Imgs[1].Txts[index].text = date.BetDisk[1].BetTypes[index].ToString();
                }

            });

            BT_Single[index].onClick.AddListener(delegate
            {
                int tempi = date.PlayerCoin % date.PlayerBet;
                int tempcoin = date.PlayerCoin - tempi;
                if (tempcoin >= date.PlayerBet)
                {
                    date.BetDisk[3].BetTypes[index] += date.PlayerBet;
                    date.PlayerCoin -= date.PlayerBet;
                    CoinText.text = "玩家金額 ：" + date.PlayerCoin;
                    _Imgs[2].Txts[index].text = date.BetDisk[3].BetTypes[index].ToString();
                }

            });

            BT_Triple[index].onClick.AddListener(delegate
            {
                int tempi = date.PlayerCoin % date.PlayerBet;
                int tempcoin = date.PlayerCoin - tempi;
                if (tempcoin >= date.PlayerBet)
                {
                    date.BetDisk[2].BetTypes[index] += date.PlayerBet;
                    date.PlayerCoin -= date.PlayerBet;
                    CoinText.text = "玩家金額 ：" + date.PlayerCoin;
                    _Imgs[3].Txts[index].text = date.BetDisk[2].BetTypes[index].ToString();
                }

            });


        }

        for (int i=0;i<BT_DoubleNumbers.Length;i++)
        {
            int index = i;
            BT_DoubleNumbers[index].onClick.AddListener(delegate
            {
                int tempi = date.PlayerCoin % date.PlayerBet;
                int tempcoin = date.PlayerCoin - tempi;
                if (tempcoin >= date.PlayerBet)
                {
                    date.BetDisk[4].BetTypes[index] += date.PlayerBet;
                    date.PlayerCoin -= date.PlayerBet;
                    CoinText.text = "玩家金額 ：" + date.PlayerCoin;
                    _Imgs[4].Txts[index].text = date.BetDisk[4].BetTypes[index].ToString();
                }
            });

        }

        for (int i=0;i<BT_Totals.Length;i++)
        {
            int index = i;
            BT_Totals[index].onClick.AddListener(delegate
            {

                int tempi = date.PlayerCoin % date.PlayerBet;
                int tempcoin = date.PlayerCoin - tempi;
                if (tempcoin >= date.PlayerBet)
                {
                    date.BetDisk[5].BetTypes[index] += date.PlayerBet;
                    date.PlayerCoin -= date.PlayerBet;
                    CoinText.text = "玩家金額 ：" + date.PlayerCoin;
                    _Imgs[5].Txts[index].text = date.BetDisk[5].BetTypes[index].ToString();
                }

            });

        }

        BT_AllTriple.onClick.AddListener(delegate
        {
            int tempi = date.PlayerCoin % date.PlayerBet;
            int tempcoin = date.PlayerCoin - tempi;
            if (tempcoin >= date.PlayerBet)
            {
                date.AllTriple += date.PlayerBet;
                date.PlayerCoin -= date.PlayerBet;
                CoinText.text = "玩家金額 ：" + date.PlayerCoin;
                _Imgs[6].Txts[0].text = date.AllTriple.ToString();
            }

        });


        BT_Start.onClick.AddListener(StartAction);

        BT_BetPlus.onClick.AddListener(delegate
        {
            

            date.PlayerBet += date.BetIncrease;
            bet_Text.text = date.PlayerBet.ToString();


	    });

        BT_BetReduce.onClick.AddListener(delegate
        {
           
               
            if (date.PlayerBet-date.BetIncrease>0)
            {
                date.PlayerBet -= date.BetIncrease;
                bet_Text.text = date.PlayerBet.ToString();
            }
           

        });


        InputOK.onClick.AddListener(delegate()
        {
           

            if (NumberIPT.text!=null&&NumberIPT.text.Length==3)
            {

                SetNumber = true;
                int iptNum;
                bool isNumber = int.TryParse(NumberIPT.text, out iptNum);
                Debug.Log("數入得是不是數字" + isNumber);
                Debug.Log("NumberIPT.text.Length :" + NumberIPT.text.Length);

                if (isNumber)
                {
                    for (int i = 0; i < NumberIPT.text.Length; i++)
                    {
                        int index = int.Parse(NumberIPT.text.Substring(i, 1));
                        if (index > 0 && index < 7)
                        {

                            date.ShowNumbers[i] = index;

                        }
                        else
                        {

                            SetNumber = false;
                            NumberIPT.text = "";
                            Debug.Log(NumberIPT.transform.GetChild(2).name);
                            Warring_Img.gameObject.SetActive(true);
                            InputOK.gameObject.SetActive(false);
                            break;
                        }

                    }


                }


            }
            else
            {
                SetNumber = false;
                NumberIPT.text = "";
                Debug.Log(NumberIPT.transform.GetChild(2).name);
                Warring_Img.gameObject.SetActive(true);
                InputOK.gameObject.SetActive(false);


            }

	    });

        OKCheck.onClick.AddListener(delegate()
        {
            
            InputOK.gameObject.SetActive(true);
            Warring_Img.gameObject.SetActive(false);


        });

      
        BT_Menu.onClick.AddListener(delegate
        {
            bool GetActive = !NumberIPT.gameObject.activeInHierarchy;

            NumberIPT.gameObject.SetActive(GetActive);


        });



    }

}
[System.Serializable]
public class Imges
{

    public List<Text>Txts;


}
