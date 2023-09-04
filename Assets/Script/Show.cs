using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Show 
{


    public Image ShowImage;
    public Text Showtext;

    public Show(Image image)
    {

        ShowImage = image;
        Showtext = ShowImage.transform.GetChild(0).GetComponent<Text>();

    }


    public IEnumerator Show_bulletin(Date date)
    {
        ShowImage.gameObject.SetActive(true);

        Showtext.text = date._DiceType.ToString();
        Showtext.text += date.DiceTotles+"點";
        for (int i=0;i<date.ShowNumbers.Length;i++)
        {
            Showtext.text += date.ShowNumbers[i].ToString();

        }
        if (date.DiceTotles > 10)
        {
            Showtext.text += "大";
        }
        else
        {

            Showtext.text += "小";
        }

        yield return new WaitForSeconds(2);

        Showtext.text = "贏得金額："+date.WinCoin;
        yield return new WaitForSeconds(2);
    }



}
