using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChangeScene : MonoBehaviour
{

    
    public void MoveToMain()
    {
        if(StageManager.Basic == true)
        {
            Managers.Clear();
            SceneManager.LoadScene("MainPage");
            StageManager.ToMain = true;
            
        }
        else if(StageManager.Codition == true)
        {
            Managers.Clear();
            SceneManager.LoadScene("MainPage");
            StageManager.ToMain2 = true;

        }
        else if(StageManager.Loop == true)
        {
            Managers.Clear();
            SceneManager.LoadScene("MainPage");
            StageManager.ToMain3 = true;

        }
        
        else if (StageManager.Challenge == true)
        {
            Managers.Clear();
            SceneManager.LoadScene("MainPage");
            StageManager.ToMain4 = true;
        }
        


    }
    public void MoveToStage()
    {
        Managers.Music.audioSource.gameObject.SetActive(true);
        MoveToMain();
    }
    
    public void ReloadMe()
    {
        Managers.Clear();
        Managers.Music.audioSource.gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MoveToNext()
    {
        Managers.Clear();
        Managers.Music.audioSource.gameObject.SetActive(true);
        string sceneName = SceneManager.GetActiveScene().name;
        int sceneNameLastIndex = sceneName.Length - 1;
        int replaceNum = sceneName[sceneNameLastIndex] + 1; //basic 1

        if ((replaceNum-48) == 10)
        {
            if (sceneName[2].Equals('s'))
            {
                LoadingSceneController.LoadScene("Basic 10");
            }
            else if (sceneName[2].Equals('a'))
            {
                LoadingSceneController.LoadScene("Challenge10");
            }
            else if (sceneName[2].Equals('n'))
            {
                LoadingSceneController.LoadScene("Condition 10");
            }
            else if (sceneName[2].Equals('o'))
            {
                LoadingSceneController.LoadScene("Loop 10");
            }
        }
        else if ((sceneNameLastIndex == 7) && sceneName[2].Equals('s'))
        {
            MoveToMain();
        }
        else if ((sceneNameLastIndex == 10) && sceneName[2].Equals('a'))
        {
            MoveToMain();
        }
        else if ((sceneNameLastIndex == 11) && sceneName[2].Equals('n'))
        {
            MoveToMain();
        }
        else if ((sceneNameLastIndex == 6) && sceneName[2].Equals('o'))
        {
            MoveToMain();
        }

        else
        {
            char replacedNum = System.Convert.ToChar(replaceNum);
            LoadingSceneController.LoadScene(sceneName.Replace(sceneName[sceneNameLastIndex], replacedNum));
        }
    }
    




    public void MoveToBasic_1()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 1");
    }

    public void MoveToBasic_2()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 2");
    }

    public void MoveToBasic_3()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 3");
    }
    public void MoveToBasic_4()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 4");
    }
    public void MoveToBasic_5()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 5");
    }
    public void MoveToBasic_6()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 6");
    }
    public void MoveToBasic_7()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 7");
    }
    public void MoveToBasic_8()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 8");
    }
    public void MoveToBasic_9()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 9");
    }
    public void MoveToBasic_10()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Basic 10");
    }

    public void MoveToCodition_1()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 1");
    }
    public void MoveToCodition_2()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 2");
    }
    public void MoveToCodition_3()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 3");
    }
    public void MoveToCodition_4()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 4");
    }
    public void MoveToCodition_5()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 5");
    }
    public void MoveToCodition_6()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 6");
    }
    public void MoveToCodition_7()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 7");
    }
    public void MoveToCodition_8()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 8");
    }
    public void MoveToCodition_9()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 9");
    }
    public void MoveToCodition_10()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Condition 10");
    }


    public void MoveToLoop_1()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 1");
    }
    public void MoveToLoop_2()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 2");
    }
    public void MoveToLoop_3()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 3");
    }
    public void MoveToLoop_4()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 4");
    }
    public void MoveToLoop_5()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 5");
    }
    public void MoveToLoop_6()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 6");
    }
    public void MoveToLoop_7()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 7");
    }
    public void MoveToLoop_8()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 8");
    }
    public void MoveToLoop_9()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 9");
    }
    public void MoveToLoop_10()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Loop 10");
    }

    public void MoveToChallenge_1()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge1");
    }
    public void MoveToChallenge_2()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge2");
    }
    public void MoveToChallenge_3()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge3");
    }
    public void MoveToChallenge_4()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge4");
    }
    public void MoveToChallenge_5()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge5");
    }
    public void MoveToChallenge_6()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge6");
    }
    public void MoveToChallenge_7()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge7");
    }
    public void MoveToChallenge_8()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge8");
    }
    public void MoveToChallenge_9()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge9");
    }
    public void MoveToChallenge_10()
    {
        Managers.Clear();
        LoadingSceneController.LoadScene("Challenge10");
    }


}
