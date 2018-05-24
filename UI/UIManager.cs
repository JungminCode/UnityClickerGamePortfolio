using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text mCurrentGoldText;     // 현재 골드 텍스트 변수
    public Text mGoldPerText;         // 클릭당 골드 텍스트 변수
    public Text mGoldPerSecText;      // 1초 당 골드 텍스트 변수
    public Text mChequeText;          // 수표 텍스트 변수


    void FixedUpdate()
    {
        mCurrentGoldText.text = "돈 : " + GetNumberAndStringText(DataController.Instance.mGold).ToString();      // 현재 돈을 텍스트로 출력
        mGoldPerText.text = "클릭 당: + " + GetNumberAndStringText(DataController.Instance.mGoldPerClick).ToString();  // 클릭 당 돈을 텍스트로 출력
        mGoldPerSecText.text = "초당 + : " + GetNumberAndStringText(DataController.Instance.GetGoldPerSec()).ToString();  // 1초당 돈을 텍스트로 출력
        mChequeText.text = "수표 : " + GetNumberAndStringText(DataController.Instance.mCheque).ToString();        // 수표를 텍스트로 출력
    }

    public string GetNumberAndStringText(long data) // 돈을, 간편하게 볼 수 있게 하기 위한 함수
    {
        string mConvert;    // mConvert라는 string형 변수를 만듬

        if (data >= 1000000000000000000)    // 현재 돈이 100경이상이면, 현재 돈을 100경만큼 나누고, 소수점 하나로 해서, 뒤에 f를 붙여주는 형식이다.
        {
            mConvert = ((float)data / 1000000000000000000f).ToString("f1") + "f";
        }
        else if (data >= 1000000000000000)  // 이것은 1000조
        {
            mConvert = ((float)data / 1000000000000000f).ToString("f1") + "e";
        }
        else if (data >= 1000000000000) // 1조
        {
            mConvert = ((float)data / 1000000000000f).ToString("f1") + "d";
        }
        else if (data >= 1000000000)    // 10억
        {
            mConvert = ((float)data / 1000000000f).ToString("f1") + "c";
        }
        else if (data >= 1000000)   // 100만
        {
            mConvert = ((float)data / 1000000f).ToString("f1") + "b";
        }
        else if (data >= 1000)  // 천
        {
            mConvert = ((float)data / 1000f).ToString("f1") + "a";
        }
        else
        {
            mConvert = "" + data;   // 1000단위가 아니라면, 그냥 보여준다.
        }
        return mConvert;    // if문을 이용해 변환한 값을 리턴한다.
    }
}
