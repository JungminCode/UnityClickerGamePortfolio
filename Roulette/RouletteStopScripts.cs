using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteStopScripts : MonoBehaviour
{
    public RouletteManager mRouletteManager;    // 룰렛 매니저 스크립트를 가지고 있는, GameManager 오브젝트 안에 있는 RouletteManager 스크립트를 사용하기 위한 변수

    void Start()
    {
    }

    void OnCollisionEnter2D(Collision2D other)  // 콜라이더가 부딪힌 곳에 태그값의 따라서, 그 값에 룰렛 베팅의 결과가 정해진다.
    {
        if (other.collider.CompareTag("Zero") && mRouletteManager.GetComponent<RouletteManager>().mStop == true)    // 꽝에 멈췄을 경우
        {
            SoundManager.instance.PlayNOSound();
            DataController.Instance.mGold += 0;
            // 꽝에 부딪혔을 경우에, 사운드 매니저에 있는 PlayNOSound 함수에 작성된 bgm이 나옴. 그리고, 베팅금액을 0원 합산
        }
        else if (other.collider.CompareTag("x0.5") && mRouletteManager.GetComponent<RouletteManager>().mStop == true)   // 0.5에 멈췄을 때
        {
            double mTempMoney;  // 0.5로 계산을 해서 그 값을 저장 할 변수
            mTempMoney = (double)mRouletteManager.GetComponent<RouletteManager>().mBetMoney * 0.5f; // 베팅머니의 50%를 곱해주어, mTempMoney변수에 대입
            Debug.Log(mTempMoney);
            SoundManager.instance.PlayNOSound();    // 사운드 매니저에 있는 PlayNOSound함수 실행
            DataController.Instance.mGold += (long)mTempMoney;  // long형으로 캐스팅해준, 50%의 값을 현재 골드에 대입
        }
        else if (other.collider.CompareTag("x1") && mRouletteManager.GetComponent<RouletteManager>().mStop == true) // 1에 멈출 경우
        {
            SoundManager.instance.PlayNOSound();    // 사운드 매니저에 있는 PlayNOSound함수 실행
            DataController.Instance.mGold += mRouletteManager.GetComponent<RouletteManager>().mBetMoney * 1;    // 배팅금액 x 1 해준 값을 현재 골드에 합산
        }
        else if (other.collider.CompareTag("x2") && mRouletteManager.GetComponent<RouletteManager>().mStop == true) // 2에 멈출 경우
        {
            DataController.Instance.mGold += mRouletteManager.GetComponent<RouletteManager>().mBetMoney * 2;    // x2배 해준 값을 현재 골드에 합산
        }
        else if (other.collider.CompareTag("x3") && mRouletteManager.GetComponent<RouletteManager>().mStop == true) // 3에 멈출 경우
        {
            SoundManager.instance.PlayWowSound();   // PlayWowSound함수 사운드 매니저에서 호출
            DataController.Instance.mGold += mRouletteManager.GetComponent<RouletteManager>().mBetMoney * 3;    // 배팅액의 3배 값을 현재 골드에 합산
        }
        else if (other.collider.CompareTag("x5") && mRouletteManager.GetComponent<RouletteManager>().mStop == true) // 5에 멈출 경우
        {
            SoundManager.instance.PlayWowSound();   // PlayWowSound함수 사운드 매니저에서 호출
            DataController.Instance.mGold += mRouletteManager.GetComponent<RouletteManager>().mBetMoney * 5;    // 베팅액의 5배 값을 현재 골드에 합산
        }
        else if (other.collider.CompareTag("x100") && mRouletteManager.GetComponent<RouletteManager>().mStop == true)   // 100에 멈출 경우
        {
            SoundManager.instance.PlayWowSound();   // PlayWowSound함수 사운드 매니저에서 호출
            DataController.Instance.mGold += mRouletteManager.GetComponent<RouletteManager>().mBetMoney * 100;  // 베팅액의 100배 값을 현재 골드에 합산
        }
        else if (mRouletteManager.GetComponent<RouletteManager>().mStop == true)    // 충돌체크가 되지 않았다면,    
        {
            DataController.Instance.mGold += mRouletteManager.GetComponent<RouletteManager>().mBetMoney * 1;    // 배팅액을 다시 돌려준다.
        }
    }
}
