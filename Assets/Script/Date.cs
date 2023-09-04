using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Date
{
    public int PlayerCoin;
    public int PlayerBet;//玩家下注金額
    public int BetIncrease;//下注增額
    public int WinCoin;
    public int index;//紀錄雙骰或圍骰相同的點數是幾點 , 單骰為0
    public int DiceTotles;//骰子點數總和
    public DiceType _DiceType;

    public int[] ShowNumbers;//三個骰子的值
    public int AllTriple;//全圍

    public List<intcount> BetDisk;//下注資料



    public Date(int ShowNumbers, int BigSmall, int Double, int Triple, int Single, int DoubleNumbers, int Totals, int BetIncrease)
    {
        BetDisk = new List<intcount>();
       
        this.ShowNumbers = new int[ShowNumbers];

        
        BetDisk.Add(new intcount(BigSmall, "BigSmall"));
        //this.BigSmall = new int[BigSmall];
        BetDisk.Add(new intcount(Double, "Double"));
        //this.Double = new int[Double];
        BetDisk.Add(new intcount(Triple, "Triple"));
        //this.Triple = new int[Triple];
        BetDisk.Add(new intcount(Single, "Single"));
        //this.Single = new int[Single];
        BetDisk.Add(new intcount(DoubleNumbers, "DoubleNumbers"));
        //this.DoubleNumbers = new int[DoubleNumbers];
        BetDisk.Add(new intcount(Totals, "Total"));
        //this.Totals = new int[Totals];

        this.BetIncrease = BetIncrease;
        PlayerBet = BetIncrease;
        PlayerCoin = 9999;

    }


    /// <summary>
    /// 隨機給三個骰子值
    /// </summary>
    /// <param name="Count"></param>
    public void ChangeNumbers()
    {
        
        for (int i = 0; i < ShowNumbers.Length; i++)
        {
            int index = Random.Range(1, 7);
            ShowNumbers[i] = index;
            //DiceTotles += index;
            Debug.Log(string.Format("ShowNumbers[{0}] : {1}", i, index));
        }

        //Debug.Log("骰子點數總和為：" + DiceTotles);


    }

    public int DiceTotal()
    {
        DiceTotles = 0;
        int ret=0;
        foreach (int i in ShowNumbers)
        {
            ret += i;

        }
        Debug.Log("骰子點數總和為：" + DiceTotles);
        return ret;
    }

    /// <summary>
    /// 三顆骰子內容判斷
    /// </summary>
    public void NumberJudge()
    {
        DiceType _DiceType = Judge(out index);

        if (_DiceType == DiceType.圍骰)
        {

            //PlayerCoin += Triple[index - 1];
            WinCoin += BetDisk[2].BetTypes[index - 1]*180;
            Debug.Log(string.Format("當前中獎金額 ：{0} ,圍骰押注：{1} 塊", WinCoin, BetDisk[2].BetTypes[index - 1]));
            WinCoin += AllTriple*30;
            Debug.Log(string.Format("當前中獎金額 ：{0} ,全圍押注：{1} 塊", WinCoin, AllTriple));

            //處理圍骰中單骰得部分
            WinCoin += BetDisk[3].BetTypes[index-1]*3;
            Debug.Log(string.Format("當前中獎金額 ：{0} ,中圍骰時的單骰押注：{1} 塊", WinCoin, BetDisk[3].BetTypes[index - 1]));

        }
        else if (_DiceType == DiceType.雙骰)
        {

            //PlayerCoin += Double[index - 1];
            WinCoin += BetDisk[1].BetTypes[index - 1]*10;
            Debug.Log(string.Format("當前中獎金額 ：{0} ,雙骰押注：{1} 塊", WinCoin, BetDisk[1].BetTypes[index - 1]));

            //處理押單骰的部分
            WinCoin += BetDisk[3].BetTypes[index - 1]*2;
            Debug.Log(string.Format("當前中獎金額 ：{0} ,中雙骰時的單骰押注：{1} 塊", WinCoin, BetDisk[3].BetTypes[index - 1]));
            foreach (int i in ShowNumbers)
            {
                if (i!=index)
                {
                    WinCoin += BetDisk[3].BetTypes[i - 1]*2;
                    Debug.Log(string.Format("當前中獎金額 ：{0} ,中雙骰時的單骰押注：{1} 塊", WinCoin, BetDisk[3].BetTypes[i - 1]));

                }

            }


        }
        else//單骰
        {

            foreach (int i in ShowNumbers)
            {

                WinCoin += BetDisk[3].BetTypes[i - 1];
                Debug.Log(string.Format("當前中獎金額 ：{0} ,單骰押注：{1} 塊", WinCoin, BetDisk[3].BetTypes[i - 1]));
            }

        }

        //猜大小
        if (3 < DiceTotles && DiceTotles < 11)
        {
            WinCoin += BetDisk[0].BetTypes[0];
            Debug.Log( string.Format("猜大小 ：（小）,當前中獎金額{0},押注金額{1}：", WinCoin, BetDisk[0].BetTypes[0]));

        }
        else
        {
            WinCoin += BetDisk[0].BetTypes[1];
            Debug.Log(string.Format("猜大小 ：（大）,當前中獎金額{0},押注金額{1}：", WinCoin, BetDisk[0].BetTypes[1]));

        }

        //押總和
        if (DiceTotles<18&&DiceTotles>3)
        {
            if (DiceTotles == 4 || DiceTotles == 17)
            {

                WinCoin += BetDisk[5].BetTypes[DiceTotles - 4] * 60;
                Debug.Log(string.Format("押總和 ：總和數為：{0},當前中獎金額{1},押多少錢{2}", DiceTotles, WinCoin, BetDisk[5].BetTypes[DiceTotles - 4]));

            }
            else if (DiceTotles == 5 || DiceTotles == 16)
            {

                WinCoin += BetDisk[5].BetTypes[DiceTotles - 4] * 30;
                Debug.Log(string.Format("押總和 ：總和數為：{0},當前中獎金額{1},押多少錢{2}", DiceTotles, WinCoin, BetDisk[5].BetTypes[DiceTotles - 4]));

            }
            else if (DiceTotles == 6 || DiceTotles == 15)
            {

                WinCoin += BetDisk[5].BetTypes[DiceTotles - 4] * 18;
                Debug.Log(string.Format("押總和 ：總和數為：{0},當前中獎金額{1},押多少錢{2}", DiceTotles, WinCoin, BetDisk[5].BetTypes[DiceTotles - 4]));

            }
            else if (DiceTotles == 7 || DiceTotles == 14)
            {

                WinCoin += BetDisk[5].BetTypes[DiceTotles - 4] * 12;
                Debug.Log(string.Format("押總和 ：總和數為：{0},當前中獎金額{1},押多少錢{2}", DiceTotles, WinCoin, BetDisk[5].BetTypes[DiceTotles - 4]));

            }
            else if (DiceTotles == 8 || DiceTotles == 13)
            {

                WinCoin += BetDisk[5].BetTypes[DiceTotles - 4] * 8;
                Debug.Log(string.Format("押總和 ：總和數為：{0},當前中獎金額{1},押多少錢{2}", DiceTotles, WinCoin, BetDisk[5].BetTypes[DiceTotles - 4]));

            }
            else if (DiceTotles == 9 || DiceTotles == 12)
            {

                WinCoin += BetDisk[5].BetTypes[DiceTotles - 4] * 7;
                Debug.Log(string.Format("押總和 ：總和數為：{0},當前中獎金額{1},押多少錢{2}", DiceTotles, WinCoin, BetDisk[5].BetTypes[DiceTotles - 4]));

            }
            else
            {

               
                WinCoin += BetDisk[5].BetTypes[DiceTotles - 4] * 6;
                Debug.Log(string.Format("押總和 ：總和數為：{0},當前中獎金額{1},押多少錢{2}", DiceTotles, WinCoin, BetDisk[5].BetTypes[DiceTotles - 4]));


            }

        }

        if (_DiceType!=DiceType.圍骰)
        {
            foreach (int i in GetTwoindex(ShowNumbers))
            {

                WinCoin += BetDisk[4].BetTypes[i]*5;
                Debug.Log(string.Format("押注指定兩數：當前中獎金額：{0},押注金額：{1}", WinCoin, BetDisk[4].BetTypes[i]));

            }
        }
        



    }

    public DiceType Judge(out int Outindex)
    {
        
        int index;
        if (ShowNumbers[0] == ShowNumbers[1])
        {
            if (ShowNumbers[1] == ShowNumbers[2])
            {

                _DiceType = DiceType.圍骰;

            }
            else
            {

                _DiceType = DiceType.雙骰;
            }
            index = ShowNumbers[0];
        }
        else if (ShowNumbers[1] == ShowNumbers[2] || ShowNumbers[0] == ShowNumbers[2])
        {

            _DiceType = DiceType.雙骰;
            index = ShowNumbers[2];
        }
        else
        {
            index = 0;
            _DiceType = DiceType.單骰;

        }

        Debug.Log(_DiceType.ToString());
        Outindex = index;
        return _DiceType;


    }




    [System.Serializable]
    public class intcount
    {

        public intcount(int i,string TypeName)
        {

            BetTypes = new int[i];
            this.TypeName = TypeName;
        }

        public string TypeName;
        public int[] BetTypes;
        

    }



    public List<int> GetTwoindex(int [] index)
    {
        List<int> ret = new List<int>();

        for (int i=1;i<6;i++)
        {
            bool HasNumber = ((IList)index).Contains(i);
            if (HasNumber)
            {

                for (int j=i+1; j<=6;j++)
                {
                    bool HasNumber2 = ((IList)index).Contains(j);
                    if (HasNumber2)
                    {

                        int num = (i * 10) + j;
                        string s = "數字" + num.ToString();
                        Twoindex _Twoindex = (Twoindex)System.Enum.Parse(typeof(Twoindex),s);
                        int tempi = (int)_Twoindex;
                        ret.Add(tempi);
                        //PlayerCoin += BetDisk[4].BetTypes[tempi];
                        Debug.Log(string.Format("指定雙數為{0}和{1}", i, j));
                    }

                }

                break;

            }

        }

        return ret;

    }



}

public enum DiceType
{

    圍骰,
    雙骰,
    單骰


}

public enum Twoindex
{
    數字12,
    數字13,
    數字14,
    數字15,
    數字16,
    數字23,
    數字24,
    數字25,
    數字26,
    數字34,
    數字35,
    數字36,
    數字45,
    數字46,
    數字56,


}
