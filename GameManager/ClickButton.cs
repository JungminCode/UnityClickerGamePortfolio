using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    ButtonManager mButtonManager;   // Canvas 오브젝트에 있는 스크립트 ButtonManager를 사용할 변수
    private bool mVive = true;  // 진동은 true값
    public Animator mAnim;  // 플레이어의 애니메이터를 가져온다.

    private void Start()
    {
        mButtonManager = GameObject.Find("Canvas").GetComponent<ButtonManager>();   // Canvas라는 게임오브젝트의 컴포넌트중에 ButtonManager를 가져온다.
    }

    private void Update()
    {
        mVive = mButtonManager.mCurrentVive;    // 진동은, 버튼매니저에서 변경되고 있는 진동의 값을 가져온다.
    }

    public void OnMouseDown()   // 지정해 준, 콜라이더에 터치가 되었다면,
    {
        long mGoldPerClick = DataController.Instance.mGoldPerClick; // 클릭 당 골드를, mGoldPerClick이라는 변수에 넣어준다.
        DataController.Instance.mGold += mGoldPerClick; // 현재 골드를 mGoldPerClick만큼 합산
        if (mVive == true)  // mVive의 값이 true라면,
        {
            Handheld.Vibrate(); // 진동을 울리게 한다.
        }
        mAnim.SetTrigger("OnClick");    // 그리고 뛰어가는 애니메이션을 실행
    }
}
