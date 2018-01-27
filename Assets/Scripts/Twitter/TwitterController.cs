
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TwitterController : MonoBehaviour 
{

    // You need to register your game or application in Twitter to get cosumer key and secret.
    // Go to this page for registration: http://dev.twitter.com/apps/new
    public string CONSUMER_KEY;
    public string CONSUMER_SECRET;
	public string USERID;

    // You need to save access token and secret for later use.
    // You can keep using them whenever you need to access the user's Twitter account. 
    // They will be always valid until the user revokes the access to your application.
    const string PLAYER_PREFS_TWITTER_USER_ID           = "TwitterUserID";
    const string PLAYER_PREFS_TWITTER_USER_SCREEN_NAME  = "TwitterUserScreenName";
    const string PLAYER_PREFS_TWITTER_USER_TOKEN        = "TwitterUserToken";
    const string PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET = "TwitterUserTokenSecret";

    Twitter.RequestTokenResponse m_RequestTokenResponse;
    Twitter.AccessTokenResponse m_AccessTokenResponse;

	bool tweetsLoaded = false;
	public bool isAuthenticated = false;
	public string[] tweets;

	// Use this for initialization
	void Start() 
    {
        LoadTwitterUserInfo();
	}
	
	// Update is called once per frame
	void Update() 
    {
	}

    // GUI
    void OnGUI()
    {

        if (string.IsNullOrEmpty(CONSUMER_KEY) || string.IsNullOrEmpty(CONSUMER_SECRET))
        {
            string text = "You need to register your game or application first.\n Click this button, register and fill CONSUMER_KEY and CONSUMER_SECRET of Demo game object.";
			print (text);
        }
        else
        {
            string text = string.Empty;

            if (string.IsNullOrEmpty(m_AccessTokenResponse.ScreenName))
            {
				text = "You need to register your game or application first.";
            }
        }
			
    }

	public void LoadTweets()
	{
		this.tweetsLoaded = false;
		StartCoroutine(Twitter.API.GetHashtag("HabloEscobarGGJ18", 10, CONSUMER_KEY, CONSUMER_SECRET, m_AccessTokenResponse,
			new Twitter.GetTimelineCallback(this.OnGetTimeline)));
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
            print(log);
        }

    }

	public bool isTweetsLoaded() {
		return this.tweetsLoaded;
	}

    
	void OnGetTimeline(bool success, string[] tweets)
	{
		print("OnGetTimeline - " + (success ? "succedded." : "failed."));
		this.tweetsLoaded = true;
		this.tweets = tweets;
	}
		
}
