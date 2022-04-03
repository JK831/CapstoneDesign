using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MoveToChallenge()
    {
        SceneManager.LoadScene("ChallengeChoice");
    }

    public void MoveToStudy()
    {
        SceneManager.LoadScene("StudyChoice");
    }

    public void MoveToCondition()
    {
        SceneManager.LoadScene("ConditionChoice");
    }

    public void MoveToLoop()
    {
        SceneManager.LoadScene("LoopChoice");
    }

    public void MoveToBasic()
    {
        SceneManager.LoadScene("BasicChoice");
    }
}
