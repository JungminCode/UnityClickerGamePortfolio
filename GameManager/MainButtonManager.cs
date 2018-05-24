using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // SceneManager를 사용하기 위해 using

public class MainButtonManager : MonoBehaviour
{
    public GameObject [] mButton;   // Start와 Quit 버튼을 배열로 만듬

    private void Awake()
    {
        StartCoroutine(ButtonOn()); // Button이 바로 뜨지 않게 코루틴을 사용해, 2초 후에 버튼이 보여지게 함.
    }

    public void MainQuitButton()    // Quit버튼을 눌렀을 때,
    {
        Application.Quit(); // 현재 어플리케이션을 종료
    }

    public void MainStartButton()   // 스타트 버튼을 눌렀다면
    {
        SceneManager.LoadScene("Game"); // Game이라는 이름의 씬으로 넘어감
    }

    IEnumerator ButtonOn()  // 버튼이 2초후에 보여지는 코루틴
    {
        yield return new WaitForSeconds(2.0f);  // 게임이 실행되고, 2초후에
        mButton[0].SetActive(true);
        mButton[1].SetActive(true);
        // Start와 Quit버튼이 보여진다.
    }
}
