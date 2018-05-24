using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    AudioSource mBGMSource; // 오디오소스 컴포넌트 변수
    [HideInInspector]
    public float mVolume;   // 볼륨값 변수
    ButtonManager mButtonManager;   // 버튼매니저 클래스 변수

    // 안드로이드로 빌드할 때,버튼 터치 감도를 잘 조절해주는 것

    private const float inchToCm = 2.54f;

    [SerializeField]
    private EventSystem eventSystem = null;

    [SerializeField]
    private float dragThresholdCM = 0.5f;
    //For drag Threshold

    private void SetDragThreshold()
    {
        if (eventSystem != null)
        {
            eventSystem.pixelDragThreshold = (int)(dragThresholdCM * Screen.dpi / inchToCm);
        }
    }

    void Awake()
    {
        SetDragThreshold();
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // 화면이 꺼지지 않게 한다.
    }

    // 조절 부분 끝!

    // Use this for initialization
    void Start()
    {
        mBGMSource = GetComponent<AudioSource>();   // GameManager에 있는 오디오소스 컴포넌트를 가져온다.
        mButtonManager = GameObject.Find("Canvas").GetComponent<ButtonManager>();   // Canvas 게임 오브젝트에 있는 ButtonManager스크립트를 대입
    }

    // Update is called once per frame
    void Update()
    {
        mVolume = mButtonManager.mCurrentSoundVolume;   // ON,OFF를 했을 때 변경되는 값을 mVolume에 대입
        mBGMSource.volume = mVolume;    // mVolume에 값을, 현재 오디오 소스 컴포넌트에 설정되어 있는 볼륨에 대입

        if (Application.platform == RuntimePlatform.Android)    // 안드로이드에서 Back키를 눌렀을 때,
        {
            if (Input.GetKey(KeyCode.Escape))
            { 
                Application.Quit(); // 안드로이드에서 Back키를 누르면, 현재 어플리케이션이 종료된다.
            }
        }

        if(DataController.Instance.mGold >= 4000000000000000000)    // 골드가 400경 즉, 게임 화면에선 4.0f로 설정이 되어 있다. 그 값이 되면,
        {
            DataController.Instance.mGold -= 4000000000000000000;   // 골드에서 400경을 감산하고
            DataController.Instance.mCheque++;  // 수표를 증가시킨다.
        }
    }
}
