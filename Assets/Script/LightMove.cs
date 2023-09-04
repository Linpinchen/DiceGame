using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightMove : MonoBehaviour
{

    public Image[] ims;
    public int RunCount;
    public bool MoveEnd;


    void Start()
    {
        
    }

   
    void Update()
    {
        
    }


    public void init(int RunCount)
    {

        ims = transform.GetComponentsInChildren<Image>();
        this.RunCount = RunCount;
        MoveEnd = true;

    }

    public IEnumerator Move(int index)
    {

        if (!MoveEnd)
        {

            for (int i = 0; i < RunCount; i++)
            {
                ims[ims.Length - 1].color = Color.white;

                for (int j = 1; j <= ims.Length; j++)
                {

                    ims[j - 1].color = Color.red;
                    if (j > 1)
                    {
                        ims[j - 2].color = Color.white;

                    }
                    yield return new WaitForSeconds(0.1f);

                }

            }

            ims[ims.Length - 1].color = Color.white;

            for (int j = 1; j <= index; j++)
            {

                ims[j - 1].color = Color.red;

                if (j > 1)
                {
                    ims[j - 2].color = Color.white;

                }
                yield return new WaitForSeconds(0.1f);


            }

            MoveEnd = true;
        }
        
    }

    public void rest()
    {

        foreach (var i in ims) { i.color = Color.white; }

    }


}
