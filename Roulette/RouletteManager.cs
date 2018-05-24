using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteManager : MonoBehaviour
{
    public Transform mRoulette; // 룰렛의 Transform 변수
    public GameObject mRouletteCircle;  // 룰렛 전신 오브젝트 변수
    public GameObject mNeedle;  // 바늘 변수
    public GameObject mStopButton;  // 멈추기 버튼 변수
    public GameObject mRotateButton;    // 돌리기 버튼 변수

    // 배팅 필요 UI
    public GameObject mUpButton;    // 100원 단위로 올려주는 오브젝트 변수
    public GameObject mDownButton;  // 100원 단위로 내려주는 오브젝트 변수
    public GameObject mBetText;     // 베팅 단위 변수
    public GameObject mBetButton;   // 베팅 버튼 변수
    public GameObject mBetHelpText; // 베팅 하는법을 알려주는 변수
    public GameObject mFullUpButton;    // 현재 가지고 있는 돈 전부에서 10.0e까지 전부 거는 변수
    public GameObject mFullDownButton;  // 베팅한 것을 초기화하는 변수

    public float mRotateSpeed = 100.0f; // 룰렛이 돌아가는 스피드 변수

    public bool mRotate = false;    // 돌리기를 눌렀는 지 안눌렀는지 확인 하는 변수
    public bool mStop = false;      // 멈추기를 눌렀는 지 안눌렀는지 확인 하는 변수

    private float mTime = 0f;   // 멈추기를 눌렀을 때, 2초후에 룰렛에 관한 것을 숨기게 해줄 시간 변수
    private int count = 1;  // 배팅을 할 때, 버그가 생겨서 만든 변수

    [HideInInspector]
    public long mBetMoney;  // 베팅 머니 변수

    // Use this for initialization
    void Start()
    {
        mRoulette = mRoulette.GetComponent<Transform>();    // 룰렛 원판의 Transform 컴포넌트를 가져온다.
    }

    // Update is called once per frame
    void Update()
    {
        if (mRotate)    // mRotate가 true라면, 
        {
            mRoulette.Rotate(0, 0, mRotateSpeed); // z값을 mRotateSpeed만큼 증가시켜, 돌려준다.
        }
        if (mStop)  // 멈추기 버튼을 눌렀을 때,
        {
            mRotateSpeed = 100f;    // mRotateSpeed를 100f로 변경하고,
            mTime += Time.deltaTime;    // mTime을 Time.deltaTime만큼 더해준다.
            if(mTime >= 2.0f)   // 2초 이상이면
            {
                mUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
                mDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
                mBetButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
                mBetHelpText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
                mBetText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
                mFullUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
                mFullDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
                mRouletteCircle.GetComponent<Transform>().position = new Vector3(0, 2000f, 0);
                mNeedle.GetComponent<Transform>().position = new Vector3(0, 2000f, 0);
                mRotateButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
                mStopButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
                // 모든 룰렛에 대한 것들을 숨겨준다.
            }
        }
        if (mBetMoney <= 0) // mBetMoney가 0이하면
        {
            mBetMoney = 0;  // mBetMoney를 0으로 바꾸어준다.
        }
        UpdateUI(); // UI를 업데이트 한다.
    }

    public void RouletteButtonOnClicked()   // 룰렛 버튼을 눌렀을 때,
    {
        mBetMoney = 0;  // 베팅 머니를 0으로
        mTime = 0;  // 시간 변수도 0으로
        count = 1;  // 카운트를 1로 만들고
        mUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-233f, -494f, 0);
        mDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(239, -494f, 0);
        mBetButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(3, -299f, 0);
        mBetHelpText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -113f, 0);
        mBetText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -499.9f, 0);
        mFullUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-346f, -494f, 0);
        mFullDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(357f, -494f, 0);
        mRouletteCircle.GetComponent<Transform>().position = new Vector3(0, 2000f, 0);
        mNeedle.GetComponent<Transform>().position = new Vector3(0, 2000f, 0);
        mRotateButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mStopButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRotate = false;
        mStop = false;
        // 돌리기, 멈추기에 필요한 bool형 변수를 false로 만들어주고,
        // 버튼을 눌렀을 때만 필요한 UI들을 보여주게 한다.
    }

    public void UpButtonClick() // 100원단위 버튼
    {
        if (DataController.Instance.mGold >= mBetMoney && DataController.Instance.mGold >= 100 * count) // 골드가, 베팅에 필요한 돈 이상이고, 골드가 100원 단위 이상일 경우
        {
            count++;    // count를 증가시키고
            mBetMoney += 100;   // 100원 단위로 증가시킨다.
        }
        if(DataController.Instance.mGold == mBetMoney)  // 현재 골드가 베팅 머니 변수의 값과 같다면
        {
            long temp = 0;  // temp변수를 0으로 만들고
            mBetMoney = temp;   // temp를 mBetMoney변수에 대입하고
            mBetMoney += DataController.Instance.mGold; // 현재 골드를 mBetMoney에 더한다.
        }
    }

    public void DownButtonClick()   // 100원 단위로 내리는 버튼
    {
        if (mBetMoney > 0)  // mBetMoney가 0보다 작다면
        {
            count--;    // count를 감소 시키고
            mBetMoney -= 100;   // 베팅 머니를 100원 감소 시킨다.
        }
    }

    public void FullUpButtonClick() // 풀 베팅 버튼
    {
        if (DataController.Instance.mGold >= 100) // 현재 베팅할 금액이 100원보다 많다면
        {
            mUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-233f, 2000f, 0);    // 100원 업버튼과
            mDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(239, -2000f, 0);   // 100원 다운버튼을 숨기고
            long temp = 0;
            mBetMoney = temp;
            mBetMoney += DataController.Instance.mGold; // 현재 가지고 있는 금액을 베팅금액에 더해준다.
            count = 1;  // count는 1이다.
        }
        if(mBetMoney >= 10000000000000000)  // mBetMoney가 게임단위 10.0e보다 크거나 같다면
        {
            mUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-233f, 2000f, 0);    // 업
            mDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(239, -2000f, 0);   // 다운 버튼을 숨기고
            long temp = 0;
            mBetMoney = temp;
            mBetMoney += 10000000000000000;
            count = 1;
            // 10.0e만큼 베팅금액에 더해주고, count변수를 1로 만듬
        }
    }

    public void FullDownButtonClick()   // 전부 베팅금액 초기화할 경우
    {
        mUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-233f, -494f, 0);    // 업
        mDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(239, -494f, 0);    // 다운버튼을 다시 보여준다.
        mBetMoney = 0;  // 베팅금액을 0으로 초기화하고
        count = 1;  // count는 1로 한다.
    }

    public void UpdateUI()  // UI 업데이트 함수
    {
        Text temptext = mBetText.GetComponent<Text>();  // Text 컴포넌트를 가져온다.
        temptext.text = "배팅액 : " + GetNumberAndStringText(mBetMoney);   // 배팅액을 text로 보여준다.
    }

    public void BetEnterButton()    // 베팅완료 버튼
    {
        if (mBetMoney >= 100)   // 베팅머니가 100보다 크면 실행
        {
            DataController.Instance.mGold -= mBetMoney; // 골드를 베팅 머니만큼 차감
            mUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
            mDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
            mBetButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
            mBetHelpText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
            mBetText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
            mFullUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
            mFullDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
            // 룰렛 돌리기, 바늘 , 룰렛을 제외 한 나머지 UI는 감추기
            mRouletteCircle.GetComponent<Transform>().position = new Vector3(-0.06f, -1.2735f, 0);
            mNeedle.GetComponent<Transform>().position = new Vector3(-0.033528f, -0.097381f, 0);
            mRotateButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-116f, -423f, 0);
            // 룰렛 , 룰렛 돌리기 버튼 , 바늘을 보여준다. 
        }
    }

    public void RouletteRotate()    // 돌리기 버튼을 눌렀을 때,
    {
        mRotate = true; // mRotate를, true값으로 변경
        mRotateButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);    // 돌리기 버튼 감추기
        mStopButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(144f, -423f, 0);   // 멈추기 버튼 보이기
    }

    public void RouletteStop()  // 멈추기 버튼을 눌렀을 때,
    {
        mRotate = false;    // mRotate를 false로 바꾼다.
        mStop = true;       // mStop을 true로 변경
        mStopButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);  // 멈추기 버튼 감추기
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
