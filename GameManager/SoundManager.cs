using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip mWowSound; // 3,5,100배에 걸렸을 때 쓰일 변수
    public AudioClip mNoSound;  // 꽝, 0.5 , 1에 걸렸을 때 쓰일 변수
    AudioSource mSoundManager;  // 사운드 매니저에 있는 오디오소스 컴포넌트를 사용할 변수
    ButtonManager mButtonManager;   // 버튼매니저에 있는 현재 볼륨값을 사용할 변수
    public static SoundManager instance;    // 싱글톤 패턴 변수
    private float mVolume;  // 버튼 매니저에 있는 볼륨값을 저장해줄 변수

    private void Awake()
    {
        if (SoundManager.instance == null)  // 사운드 매니저 instance변수가 null이라면,
        {
            SoundManager.instance = this;   // 지금 SoundManager클래스를 가리킨다.
        }
        mButtonManager = GameObject.Find("Canvas").GetComponent<ButtonManager>();   // Canvas 컴포넌트에 있는 ButtonManager 스크립트를 사용하게 해준다.
    }

    private void Start()
    {
        mSoundManager = GetComponent<AudioSource>();    // 사운드매니저안에 있는 AudioSource를 사용
    }

    // Update is called once per frame
    void Update()
    {
        mVolume = mButtonManager.mCurrentSoundVolume;   // mVolume변수에 버튼매니저에서의 변경된 볼륨값을 업데이트해준다.
    }

    public void PlayWowSound()
    {
        mSoundManager.volume = mVolume; // 업데이트 된 볼륨값의 따라, 사운드 볼륨을 결정
        mSoundManager.Stop();   // PlayNOSound와 겹치지 않게 일단 정지시키고
        mSoundManager.PlayOneShot(mWowSound);   // mWowSound변수안에 있는 오디오소스를 실행
    }

    public void PlayNOSound()   // 위와 같은 방식
    {
        mSoundManager.volume = mVolume;
        mSoundManager.Stop();
        mSoundManager.PlayOneShot(mNoSound);
    }
}
