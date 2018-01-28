
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TwitterMenu : MonoBehaviour 
{
	public GameObject UserLogInButton;

	public GameObject PinInput;

	public GameObject PinEnter;

	public GameObject StartButton;

	public GameObject HeaderButton;


    // You need to register your game or application in Twitter to get cosumer key and secret.
    // Go to this page for registration: http://dev.twitter.com/apps/new
    public string CONSUMER_KEY;
    public string CONSUMER_SECRET;

    // You need to save access token and secret for later use.
    // You can keep using them whenever you need to access the user's Twitter account. 
    // They will be always valid until the user revokes the access to your application.
    const string PLAYER_PREFS_TWITTER_USER_ID           = "TwitterUserID";
    const string PLAYER_PREFS_TWITTER_USER_SCREEN_NAME  = "TwitterUserScreenName";
    const string PLAYER_PREFS_TWITTER_USER_TOKEN        = "TwitterUserToken";
    const string PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET = "TwitterUserTokenSecret";

    Twitter.RequestTokenResponse m_RequestTokenResponse;
    Twitter.AccessTokenResponse m_AccessTokenResponse;

    string m_PIN = "Please enter your PIN here.";
	bool isActive = false;
	bool isAuthenticated = false;

	// Use this for initialization
	void Start() 
    {
		isActive = false;
        LoadTwitterUserInfo();
	}



	public void StartTwitter(){
		if (isActive) {
			UserLogInButton.SetActive (false);
			StartButton.SetActive (true);
			HeaderButton.SetActive (true);
			isActive = false;
			return;
		}
		isActive = true;
		string text = string.Empty;
		if (string.IsNullOrEmpty (CONSUMER_KEY) || string.IsNullOrEmpty (CONSUMER_SECRET)) {
			text = "You need to register your game or application first.\n Click this button, register and fill CONSUMER_KEY and CONSUMER_SECRET of Demo game object.";
		} else {
			if (!string.IsNullOrEmpty (m_AccessTokenResponse.ScreenName)) {
				text = m_AccessTokenResponse.ScreenName + "\nClick to register with a different Twitter account";
			} else {
				text = "You need to register your game or application first.";
			}
		}
		UserLogInButton.GetComponentInChildren<Text> ().text = text;

		UserLogInButton.SetActive (true);
		StartButton.SetActive (false);
		HeaderButton.SetActive (false);
	}


	public void OnClickLogin(){
		if (string.IsNullOrEmpty (CONSUMER_KEY) || string.IsNullOrEmpty (CONSUMER_SECRET)) {
			Application.OpenURL ("http://dev.twitter.com/apps/new");
		} else {
			StartCoroutine (Twitter.API.GetRequestToken (CONSUMER_KEY, CONSUMER_SECRET,
				new Twitter.RequestTokenCallback (this.OnRequestTokenCallback)));
		}
		PinInput.SetActive (true);
		PinEnter.SetActive (true);
	}

	// Update is called once per frame
	void Update() 
    {
	}

	public void EnterPin(){

		string m_Pin = PinInput.GetComponentsInChildren<Text> ()[1].text;
		
		StartCoroutine (Twitter.API.GetAccessToken (CONSUMER_KEY, CONSUMER_SECRET, m_RequestTokenResponse.Token, m_Pin,
			new Twitter.AccessTokenCallback (this.OnAccessTokenCallback)));
		PinInput.SetActive (false);
		UserLogInButton.SetActive (false);
		PinEnter.SetActive (false);
		StartButton.SetActive (true);
		HeaderButton.SetActive (true);
		isActive = false;
	}

  

	public void setActive() {
		this.isActive = !this.isActive;
		Canvas.ForceUpdateCanvases ();
	}


    void LoadTwitterUserInfo()
    {
        m_AccessTokenResponse = new Twitter.AccessTokenResponse();

        m_AccessTokenResponse.UserId        = PlayerPrefs.GetString(PLAYER_PREFS_TWITTER_USER_ID);
        m_AccessTokenResponse.ScreenName    = PlayerPrefs.GetString(PLAYER_PREFS_TWITTER_USER_SCREEN_NAME);
        m_AccessTokenResponse.Token         = PlayerPrefs.GetString(PLAYER_PREFS_TWITTER_USER_TOKEN);
        m_AccessTokenResponse.TokenSecret   = PlayerPrefs.GetString(PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET);

        if (!string.IsNullOrEmpty(m_AccessTokenResponse.Token) &&
            !string.IsNullOrEmpty(m_AccessTokenResponse.ScreenName) &&
            !string.IsNullOrEmpty(m_AccessTokenResponse.Token) &&
            !string.IsNullOrEmpty(m_AccessTokenResponse.TokenSecret))
        {
			this.isAuthenticated = true;
            string log = "LoadTwitterUserInfo - succeeded";
            log += "\n    UserId : " + m_AccessTokenResponse.UserId;
            log += "\n    ScreenName : " + m_AccessTokenResponse.ScreenName;
            log += "\n    Token : " + m_AccessTokenResponse.Token;
            log += "\n    TokenSecret : " + m_AccessTokenResponse.TokenSecret;
			log += "\n    isAuthenticated : " + this.isAuthenticated;
            print(log);
        }

    }

    void OnRequestTokenCallback(bool success, Twitter.RequestTokenResponse response)
    {
        if (success)
        {
            string log = "OnRequestTokenCallback - succeeded";
            log += "\n    Token : " + response.Token;
            log += "\n    TokenSecret : " + response.TokenSecret;
            print(log);

            m_RequestTokenResponse = response;

            Twitter.API.OpenAuthorizationPage(response.Token);
        }
        else
        {
            print("OnRequestTokenCallback - failed.");
        }
    }

    void OnAccessTokenCallback(bool success, Twitter.AccessTokenResponse response)
    {
        if (success)
        {
            string log = "OnAccessTokenCallback - succeeded";
            log += "\n    UserId : " + response.UserId;
            log += "\n    ScreenName : " + response.ScreenName;
            log += "\n    Token : " + response.Token;
            log += "\n    TokenSecret : " + response.TokenSecret;
            print(log);
  
            m_AccessTokenResponse = response;

            PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_ID, response.UserId);
            PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_SCREEN_NAME, response.ScreenName);
            PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_TOKEN, response.Token);
            PlayerPrefs.SetString(PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET, response.TokenSecret);
			this.isAuthenticated = true;
        }
        else
        {
            print("OnAccessTokenCallback - failed.");
        }
    }
		
}
