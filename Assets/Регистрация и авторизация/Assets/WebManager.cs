using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class UserData
{
    public Player playerData;
    public Error error;
}
[System.Serializable]
public class Error
{
    public string errorText;
    public bool isError;
}
[System.Serializable]
public class Player
{
    public string nickname;
    public Color colorMain;
    public Color colorSecond;

    public Player()
    {

    }

    public Player(string nick, Color c1, Color c2)
    {
        nickname = nick;
        colorMain = c1;
        colorSecond = c2;
    }

    public void SetMainColor(Color color) => colorMain = color;
    public void SetSecondColor(Color color) => colorSecond = color;
    public void SetNickname(string name) => nickname = name;
}

public class WebManager : MonoBehaviour
{
    public static UserData userData = new UserData();
    [SerializeField] private string targetURL;

    [SerializeField] private UnityEvent OnLogged, OnRegistered, OnError;

    public enum RequestType
    {
        logging,
        register
    }

    public string GetUserData(UserData data)
    {
        return JsonUtility.ToJson(data);
    }

    public UserData SetUserData(string data)
    {
        return JsonUtility.FromJson<UserData>(data);
    }

    private void Start()
    {
        userData.error = new Error() { errorText = "text", isError = true };
        userData.playerData = new Player("Yagir", new Color(), new Color());
    }

    public void Login(string login, string password)
    {
        StopAllCoroutines();
        if (CheckString(login) && CheckString(password))
        {
            Logging(login, password);
        }
        else
        {
            userData.error.errorText = "Error:Слишком маленький логин или пароль,попробуйте ещё.";
            OnError.Invoke();
        }
    }

    public void Registration(string login, string password, string password2, string nickname)
    {
        StopAllCoroutines();
        if (CheckString(login) && CheckString(password) && CheckString(password2) && CheckString(nickname) &&
            password == password2)
        {
            Registering(login, password, password2, nickname);
        }
        else
        {
            userData.error.errorText = "Error:Слишком маленький логин или пароль,попробуйте ещё.";
            OnError.Invoke();
        }
    }

    public bool CheckString(string toCheck)
    {
        toCheck = toCheck.Trim();
        if (toCheck.Length > 4 && toCheck.Length < 16)
        {
            return true;
        }

        return false;
    }


    public void Logging(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.logging.ToString());
        form.AddField("login", login);
        form.AddField("password", password);
        StartCoroutine(SendData(form, RequestType.logging));
    }

    public void Registering(string login, string password1, string password2, string nickname)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.register.ToString());
        form.AddField("login", login);
        form.AddField("password1", password1);
        form.AddField("password2", password2);
        form.AddField("nickname", nickname);
        StartCoroutine(SendData(form, RequestType.register));
    }

    IEnumerator SendData(WWWForm form, RequestType type)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(targetURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var data = SetUserData(www.downloadHandler.text);
                if (!data.error.isError)
                {

                    userData = data;
                    if (type == RequestType.logging)
                    {
                        OnLogged.Invoke();
                    }
                    else
                    {
                        OnRegistered.Invoke();
                    }

                }
                else
                {
                    userData = data;
                    OnError.Invoke();
                }
            }
        }
    }
}
