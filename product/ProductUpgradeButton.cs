using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductUpgradeButton : MonoBehaviour
{
    public Text mUpgradeProductText;
    public Text mUpgradeProductButtonText;
    public Button mUpgradeProductButton;
    // text부분
    public string mUpGradeProductName;
    // 제품 이름을 넣어줄 부분

    [HideInInspector]
    public long mGoldPerSecByUpgrade;
    public long mStartGoldPerSecByUpGrade = 1;
    // 초당 비용을 업그레이드 할 초기 값과 현재의 값

    [HideInInspector]
    public long mCurrentProductCost;
    public long mStartCurrentProductCost = 1;
    // 업그레이드 초기 비용과 현재 강화에 필요한 비용

    [HideInInspector]
    public int mLevel = 1;
    // 레벨부분

    public float mUpgradeMultiple = 1.14f;
    public float mCostMultiple = 2.4f;
    // 강화 할 때 마다 증가 할 값의 변수 부분

    [HideInInspector]
    public bool mUpgradeOK = true;
    [HideInInspector]
    public bool mIsPurchased = false;
    // 첫번째 변수는 강화의 레벨이 넘어가지 않았는 지를 확인하고, 두번 째 변수는 강화가 되어 있는지 안되어 있는 지 확인

    public Animator mAnim;  // 애니메이터 컨트롤러를 가져오기 위한 변수

    void Start()
    {
        DataController.Instance.LoadProductUpGradeButton(this); // 저장된 제품들의 값을, 불러온다.
        StartCoroutine("AddGoldLoop");  // AddGoldLoop라는, 코루틴을 실행
        UpdateUI(); // UI를 바꾸어주는 함수를 실행
    }

    public void PurchaseProductUpGrade()
    {
        if (DataController.Instance.mGold >= mCurrentProductCost && mUpgradeOK)  // 돈이 충분하다면
        {
            mIsPurchased = true;    // mIsPurchased를 true로 바꾸어주고
            DataController.Instance.mGold -= mCurrentProductCost;   // 업그레이드 비용만큼 현재 골드에서 감산을 하고
            mLevel++;   // 레벨을 증가시키고
            DataController.Instance.mGoldPerClick += mGoldPerSecByUpgrade/2;    // 클릭 당 골드를, 초 당 골드를 2로 나눈 값으로 더해주고
            UpdateProduct();    // UpdateProduct함수를 실행
            UpdateUI(); // UpdateUI함수를 실행
            DataController.Instance.SaveProductUpGradeButton(this); // 현재 값을 저장한다.
            mAnim.SetTrigger("OnBuy");  // 제품을 업그레이드 했을 때 모션을 실행
        }
    }

    private void FixedUpdate()
    {
        if (mLevel >= 50)   // 레벨이 50이상이라면
        {
            mUpgradeOK = false; // 업그레이드를 더이상 할 수 없게 false값으로 만들고,
            mUpgradeProductButton.interactable = false; // 버튼 클릭이 불가능하게 만들어버린다.
        }
    }

    IEnumerator AddGoldLoop()   // 1초당 초 당 골드를 얻게 해주는 코루틴
    {
        while(true) // while문으로 반복을 한다.
        {
            if(mIsPurchased)    // mIsPurchased가 true라면 실행
            {
                DataController.Instance.mGold += mGoldPerSecByUpgrade;  // 골드에, 제품의 초당 골드를 더해준다.
            }
            yield return new WaitForSeconds(1.0f);  // 1초 지연 시킨다.
        }
    }

    public void UpdateProduct()
    {
        mGoldPerSecByUpgrade = mStartGoldPerSecByUpGrade * (long)Mathf.Pow(mUpgradeMultiple, mLevel);   // Mathf.Pow는, mUpGradePower의 mLevel승이고, 초 당 골드를 증가 시킨다.
        mCurrentProductCost = mStartCurrentProductCost * (long)Mathf.Pow(mCostMultiple, mLevel);        // mCostMultiple의 mLevel승 만큼 업그레이드 비용 증가
    }

    public void UpdateUI()  // UI 변경 함수
    {
        mUpgradeProductText.text = mUpGradeProductName + "\n현재 레벨: " + mLevel;  // 현재 제품의 이름과 현재 레벨을 보여준다.
        mUpgradeProductButtonText.text = "현재 1초 당 " + mUpGradeProductName  + "의 돈: +" + 
            GetNumberAndStringText(mGoldPerSecByUpgrade).ToString() + "\n\t클릭당 돈: +" + GetNumberAndStringText(mGoldPerSecByUpgrade/2).ToString() +  
            "\n\t강화 필요 돈: " + GetNumberAndStringText(mCurrentProductCost).ToString();
        // 1초 당 골드와, 클릭 당 돈과, 강화에 필요한 돈의 버튼 Text를 보여준다.
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
