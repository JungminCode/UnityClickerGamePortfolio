using UnityEngine;

public class ButtonManager : MonoBehaviour  // 회사와 제품, 옵션의 버튼을 눌렀을 때, 보여주는 스크립트입니다.
{
    RectTransform mCompanyScrollView;   // 회사 스크롤 뷰 UI를 위치로 보여지고,안보여지게 만들기 위한 변수
    RectTransform mProductScrollView;   // 제품 스크롤 뷰 ""
    RectTransform mOptionScrollView;    // 옵션 스크롤 뷰 ""
    public GameObject mDataDeletePanel; // Data삭제를 눌렀을 때, 패널을 보여지고 안보여지게 하기 위해, GameObject로 변수를 만듬.
    RouletteManager mRouletteManager;   // 룰렛 매니저 클래스를 객체로 만듬.
    public int testCount = 0;   // testCount는, 포트폴리오 동영상을 위해, test버튼을 5번 눌렀을 경우, 수표 50개가 생성되게 하기 위해 만들었습니다.

    [HideInInspector]   // HideInInspector를 이용해, public인 변수값을 인스펙터창에 보여지지 않게 하였습니다.
    public float mCurrentSoundVolume;   // 볼륨을, ON과 OFF를 통해, 키우고 줄이고를 하기 위한 변수
    [HideInInspector]
    public float mSoundStartVolume = 1.0f;  // 시작 볼륨값
    [HideInInspector]
    public bool mCurrentVive;   // 현재 진동이, ON, OFF인지, 알기 위한 변수
    [HideInInspector]
    public bool mStartVive = true;  // 처음 진동은 true값입니다.


    private bool mDeleteDataView = false;   // 데이터 삭제를 누르고, 데이터를 삭제하시겠습니까?라는, 패널이 보일 때, true이면, 패널이 보여지고, 
                                            //false일 경우, 패널을 안보이게 하기 위한 변수 입니다.

    private void Start()
    {
        mCompanyScrollView = GameObject.Find("Company View").GetComponent<RectTransform>(); // Find함수를 이용해, Company View라는 이름의 오브젝트 안에 RectTransform 컴포넌트를 가져옵니다.
        mProductScrollView = GameObject.Find("Product View").GetComponent<RectTransform>(); // 같은 방식으로, Product View라는 오브젝트의 컴포넌트를 가져옵니다.
        mOptionScrollView = GameObject.Find("Option View").GetComponent<RectTransform>();   // Option View 오브젝트 컴포넌트
        mRouletteManager = GameObject.Find("GameManager").GetComponent<RouletteManager>();  // GameManager 오브젝트 컴포넌트
        DataController.Instance.LoadSound(this);    // 시작할 때, 음악이 ON,OFF인지를 가져옵니다.
        DataController.Instance.LoadVive(this);     // 시작할 때, 진동이 ON,OFF인지를 가져옵니다.
    }

    // 4개의 버튼을 클릭했을 때, 행동 부분

    public void CompanyButtonClicked()  // 회사 버튼을 눌렀을 때 입니다.
    {
        mCompanyScrollView.anchoredPosition = new Vector3(0, -276.65f, 0);  // 회사 스크롤뷰를 화면에 보이게 합니다.
        mProductScrollView.anchoredPosition = new Vector3(0, -2000f, 0);
        mOptionScrollView.anchoredPosition = new Vector3(0, -2000f, 0);
        mRouletteManager.mNeedle.GetComponent<Transform>().position = new Vector3(0, 2000f, 0);
        mRouletteManager.mRouletteCircle.GetComponent<Transform>().position = new Vector3(0, 2000f, 0);
        mRouletteManager.mRotateButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mStopButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mBetText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mBetButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mBetHelpText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mFullUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mFullDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mStop = false;
        mRouletteManager.mRotate = false;
        // 나머지, 제품 , 옵션 , 룰렛뷰를 위치 값을 이용하여 숨깁니다.
    }

    public void ProductButtonClicked()  // 제품 버튼을 눌렀을 때 입니다.
    {
        mCompanyScrollView.anchoredPosition = new Vector3(0, -2000f, 0);
        mProductScrollView.anchoredPosition = new Vector3(0, -276.65f, 0);  // 제품 스크롤뷰를 화면에 보이게 합니다.
        mOptionScrollView.anchoredPosition = new Vector3(0, -2000f, 0);
        mRouletteManager.mNeedle.GetComponent<Transform>().position = new Vector3(0, 2000f, 0);
        mRouletteManager.mRouletteCircle.GetComponent<Transform>().position = new Vector3(0, 2000f, 0);
        mRouletteManager.mRotateButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mStopButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mBetText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mBetButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mBetHelpText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mFullUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mFullDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mStop = false;
        mRouletteManager.mRotate = false;
        // 나머지 회사, 옵션 , 룰렛 뷰를 위치 값을 이용해 숨깁니다.
    }

    public void RulletButtonClicked()   // 룰렛 버튼을 클릭했을 때 입니다.
    {
        mCompanyScrollView.anchoredPosition = new Vector3(0, -2000f, 0);
        mProductScrollView.anchoredPosition = new Vector3(0, -2000f, 0);
        mOptionScrollView.anchoredPosition = new Vector3(0, -2000f, 0);
        // 회사, 제품, 옵션 스크롤 뷰를 y값으로 -2000f로 이동시킵니다.
    }

    public void OptionButtonClicked()  // 옵션 뷰를 눌렀을 때 입니다.
    {
        mCompanyScrollView.anchoredPosition = new Vector3(0, -2000f, 0);
        mProductScrollView.anchoredPosition = new Vector3(0, -2000f, 0);
        mOptionScrollView.anchoredPosition = new Vector3(0, -276.65f, 0);   // 옵션 스크롤뷰를 화면에 보이게 합니다.
        mRouletteManager.mNeedle.GetComponent<Transform>().position = new Vector3(0, 2000f, 0);
        mRouletteManager.mRouletteCircle.GetComponent<Transform>().position = new Vector3(0, 2000f, 0);
        mRouletteManager.mRotateButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mStopButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mBetText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mBetButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mBetHelpText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mFullUpButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mFullDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 2000f, 0);
        mRouletteManager.mStop = false;
        mRouletteManager.mRotate = false;
        // 옵션 스크롤 뷰를 제외한 나머지 스크롤뷰와 룰렛 뷰를 숨깁니다.
    }

    // 4개의 버튼을 클릭했을 때, 행동 부분 끝!


    // 사운드 옵션 부분 함수

    public void OptionSoundON() // 사운드 ON을 눌렀을 떄,
    {
        mCurrentSoundVolume = 1.0f; // 현재 볼륨을 1.0f로 만듭니다.
        DataController.Instance.SaveSound(this);    // 그리고 값을 싱글턴 패턴으로 만든, DataController 클래스에 있는 SaveSound를 실행시켜, 현재 볼륨값을 전달합니다.
    }

    public void OptionSoundOFF()    // 사운드 OFF를 눌렀을 때,
    {
        mCurrentSoundVolume = 0.0f; // 볼륨을 0으로 바꾸어, OFF시킵니다.
        DataController.Instance.SaveSound(this);    // SaveSound함수에 현재 볼륨값을 전달합니다.
    }

    // 사운드 부분 끝

    // 진동 옵션 부분

    public void OptionViveON()  // 진동 ON 버튼을 눌렀을 때
    {
        mCurrentVive = true;    // 현재 진동을 true로 하고,
        DataController.Instance.SaveVive(this); // true인 값을, SaveVive에 전달
    }

    public void OptionViveOFF() // 진동 OFF 버튼을 눌렀을 때
    {
        mCurrentVive = false;   // 현재 진동을 false 즉, 끈다라고 설정하고
        DataController.Instance.SaveVive(this); // 그 값을, SaveVive에 전달
    }

    // 진동 부분 끝!!

    // 데이터 삭제

    public void OptionDataDelete()  // 데이터 삭제 버튼을 눌렀을 때
    {
        mDeleteDataView = !mDeleteDataView; // 버튼을 누를 때, false였던, mDeleteDataView의 값을 true로 바꾼다.
        if (mDeleteDataView == true)    // true일 경우,
        {
            mDataDeletePanel.SetActive(true);   // 데이터 삭제 패널을 보여지게 한다.
        }
    }

    public void DeleteData()    // 데이터 삭제 패널에서 예를 눌렀을 경우
    {
        PlayerPrefs.DeleteAll();    // PlayerPrefs.DeletaAll()을 이용하여, PlayerPrefs를 이용해 저장된 값들을 전부 초기화한다.
        mDeleteDataView = false;    // mDeleteDataView 변수를 false값으로 바꾸고
        mDataDeletePanel.SetActive(false);  // 데이터 삭제 패널을 감춘다.
        Application.Quit(); // 그리고 실행한 파일을 종료한다.
    }

    public void DeleteNo()  // 아니오를 눌렀을 때
    {
        mDeleteDataView = false;    // mDeleteDataView 변수를 false값으로 바꾸고
        mDataDeletePanel.SetActive(false);  // 데이터 삭제 패널을 감춘다.
    }

    public void Test()  // 테스트 버튼을 눌렀을 때
    {
        testCount++;    // 누를 때 마다, testCount가 증가한다.
        if (testCount == 5) // testCount가 5번이 눌러지면
        {
            DataController.Instance.mCheque += 1000;    // 수표를 1000개 증가 시키고
            testCount = 0;  // testCount는 0으로 초기화
        }
    }

    // 데이터 삭제 끝!
}
