using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase.Auth;

public class Login_Util : MonoBehaviour
{
    private Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public GameObject Btn_Login;
    public GameObject Btn_Logout;

    void Start()
    {
        //구글 초기설정
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .RequestEmail()
            .Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate(); // 구글 플레이 활성화

        Btn_Login.SetActive(true);
        Btn_Logout.SetActive(false);

        auth = FirebaseAuth.DefaultInstance; // Firebase 액세스
    }

    void awake()
    {
        CheckFirebaseDependencies();
    }

    public void TryGoogleLogin()
    {
        if (!Social.localUser.authenticated) // 로그인 되어 있지 않는지 확인
        {
            Social.localUser.Authenticate(success => // 로그인 시도
            {
                if (success) // 성공하면
                {
                    Debug.Log("로그인 성공");
                    StartCoroutine(TryFirebaseLogin()); // Firebase Login 시도
                }
                else // 실패하면
                {
                    Debug.Log("로그인 실패");
                    Set_Logout_to_LogIn();
                }
            });
        }
    }


    public void TryGoogleLogout()
    {
        if (Social.localUser.authenticated) // 로그인 되어 있는지 확인
        {
            PlayGamesPlatform.Instance.SignOut(); // Google 로그아웃
            Debug.Log("구글 로그아웃 완료");
            auth.SignOut(); // Firebase 로그아웃
            Debug.Log("파이어베이스 로그아웃 완료");
            Set_Logout_to_LogIn();
        }
    }

    private void CheckFirebaseDependencies()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            string name = user.DisplayName;
            string uid = user.UserId;
            Set_Login_to_LogOut();
        }
        else
        {
            Set_Logout_to_LogIn();
        }
    }

    IEnumerator TryFirebaseLogin()
    {
        while (string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
            yield return null;
        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();


        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("파이어 베이스 로그인 인증 취소");
                Set_Logout_to_LogIn();
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("파이어베이스 로그인 에러: " + task.Exception);
                Set_Logout_to_LogIn();
                return;
            }
            else {
                Set_Login_to_LogOut();
                Debug.Log("파이어베이스 로그인 성공");
            }
        });
    }

    public string Get_User_Id() // firebase_id get
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        string uid = "Nothing";
        if (user != null)
        {
            uid = user.UserId;
        }
        return uid;
    }

    public void Set_Login_to_LogOut() // 로그인이 보일때 -> 로그아웃이 보이게 바꾸기
    {
        Btn_Login.SetActive(true);
        Btn_Logout.SetActive(false);
    }

    public void Set_Logout_to_LogIn() // 로그아웃이 보일때 -> 로그인이 보이게 바꾸기
    {
        Btn_Login.SetActive(false);
        Btn_Logout.SetActive(true);
    }
}