using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGradeButton : MonoBehaviour
{
    public GameObject mMark;    // 회사를 샀을 경우, 화면에 보여줄 게임 마크
    public Text mUpgradeCompanyText;
    public Text mUpgradeCompanyButtonText;
    public Button mUpgradeButton;
    // text부분
    public string mUpGradeCompanyName;
    // 회사이름을 넣어줄 부분

    [HideInInspector]
    public long mGoldByUpgrade;
    public long mStartGoldByUpGrade = 1;
    // 초당 비용을 업그레이드 할 초기 값과 현재의 값

    [HideInInspector]
    public long mCurrentCost;
    public long mStartCurrentCost = 1;
    // 업그레이드 초기 비용과 현재 강화에 필요한 비용

    [HideInInspector]
    public int mLevel = 1;
    // 레벨부분

    public float mUpgradeMultiple = 1.14f;
    public float mCostMultiple = 2.4f;
    // 강화 할 때 마다 증가 할 값의 변수 부분

    [HideInInspector]
    public bool mUpgradeOK = true;  // 업그레이드가 가능한 지 알려줄 수 있는, 변수

    public Animator mAnim;  // 캐릭터의 애니메이터 컨트롤러를 가져올 변수

    void Start()
    {
        DataController.Instance.LoadCompanyUpGradeButton(this); // 저장되어있는 회사의 정보를 불러온다.
        UpdateUI(); // UI를 업데이트 한다.
    }

    public void PurchaseCompanyUpGrade()    // 회사 업그레이드 버튼
    {
        if (DataController.Instance.mCheque >= mCurrentCost && mUpgradeOK)  // 수표가 충분하다면
        {
            DataController.Instance.mCheque -= mCurrentCost;    // 현재 강화에 필요한 수표만큼 가지고 있는 수표에서 차감한다.
            mLevel++;   // 레벨을 증가
            DataController.Instance.mGoldPerClick += mGoldByUpgrade;    // 클릭 당 돈을 늘려준다. 회사를 살 때 마다 늘어날 수 있는 만큼
            UpdateUpGrade();    // 업그레이드에 필요한 값을, 변경한다.
            UpdateUI(); // UI의 값을 변경한다.
            DataController.Instance.SaveCompanyUpGradeButton(this); // 회사 정보 저장
            mAnim.SetTrigger("OnBuy");  // OnBuy로 되어 있는, Trigger를 캐릭터가 가지고 있는, 애니메이터 컨트롤러에서 발동시켜, 모션 실행
            mMark.SetActive(true);  // 플레이어가 산 회사마크를 보이게 한다.
            if (this.name == "SAMSONG Group")   // 그 회사의 오브젝트가 SAMSONG Group이라면,
            {
                mAnim.SetTrigger("OnBuySamsong");   // 다른 모션을 실행시킨다.
            }
        }
    }

    private void FixedUpdate()
    {
        UpdateUI(); // UI의 값을 변경
        if (mLevel >= 30)   // mLevel이 30이상이면
        {
            mUpgradeOK = false; // 업그레이드가 불가능하게 변경
            mUpgradeButton.interactable = false;    // 버튼 클릭을 못하게 변경
        }
    }

    public void UpdateUpGrade() // 업그레이드 업데이트 함수
    {
        mGoldByUpgrade = mStartGoldByUpGrade * (int)Mathf.Pow(mUpgradeMultiple, mLevel);   // Mathf.Pow는, mUpGradePower의 mLevel승이다. 클릭당 골드를 증가시킨다.
        mCurrentCost = mStartCurrentCost * (int)Mathf.Pow(mCostMultiple, mLevel);       // mCostMultiple의 mLevel승 만큼 업그레이드 비용 증가
    }

    public void UpdateUI()  // 텍스트 UI 변경 함수
    {
        mUpgradeCompanyText.text = mUpGradeCompanyName + "\n현재 레벨: " + mLevel; // 현재 회사의 이름과 현재 레벨을 보여준다.
        mUpgradeCompanyButtonText.text = "클릭당 돈 : + " + GetNumberAndStringText(mGoldByUpgrade).ToString() + 
            "\n\t강화 필요 수표: " + GetNumberAndStringText(mCurrentCost).ToString();
        // 클릭 당 골드와, 강화에 필요한 골드의 버튼 Text를 보여준다.
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
