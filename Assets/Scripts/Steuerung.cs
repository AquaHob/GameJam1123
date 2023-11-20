using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using UnityEngine.AudioModule;

public class Steuerung : MonoBehaviour
{
    // List<KeyCode> Player1KeyCodes = new List<KeyCode>();
    KeyCode[] Player1KeyCodes = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Y, KeyCode.X, KeyCode.C };
    public KeyCode Player1NextKey = KeyCode.Q;
    KeyCode[] Player2KeyCodes = { KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.B, KeyCode.N, KeyCode.M };
    public KeyCode Player2NextKey = KeyCode.U;

    public TextMeshProUGUI Player1KeyText;
    public TextMeshProUGUI Player2KeyText;

    public int TotalDistance = 300;

    public bool EventOngoing;
    public bool GameOngoing = true;
    public bool Player1Loser = false;
    public bool Player2Loser = false;
    public bool Player1PlayBlocker = false;
    public bool Player2PlayBlocker = false;

    public int MaxStage = 4;
    public int GameStagePlayer1 = 1;
    public int GameStagePlayer1BeforeBoost = 0;
    public int GameStagePlayer2 = 1;
    public int GameStagePlayer2BeforeBoost = 0;

    public TextMeshProUGUI CountDownText;

    public int StartCountdown = 3;
    public GameObject Player1EventCounterGameObject;
    public TextMeshProUGUI Player1EventCounterGameObjectText;
    public TextMeshProUGUI Player1WinnerText;
    public int Player1CorrectPresses;
    public int Player1PenaltyCounter = 0;
    public int Player1DistanceRemaining;
    public int Player1DistanceDecrease = 1;
    public TextMeshProUGUI Player1DistanceMainText;
    public TextMeshProUGUI Player1DistanceSubText;

    public GameObject Player2EventCounterGameObject;
    public TextMeshProUGUI Player2EventCounterGameObjectText;
    public TextMeshProUGUI Player2WinnerText;
    public int Player2CorrectPresses;
    public int Player2PenaltyCounter = 0;
    public int Player2DistanceRemaining;
    public int Player2DistanceDecrease = 1;
    public TextMeshProUGUI Player2DistanceMainText;
    public TextMeshProUGUI Player2DistanceSubText;

    public int Player1EventCounter = 0;
    public int Player2Eventcounter = 0;

    public int ShowWinnerTimeMS = 1500;
    public int HideLoserTimeMS = 5000;
    public int PenaltyTimeMS = 5000;

    public int MaxMashCount = 20;
    public int MinMashCount = 10;
    public int Player1CurrentMashCount = 0;
    public int Player2CurrentMashCount = 0;

    public bool EventCountDownActive = false;
    public GameObject TimerTextGameObject;
    public TextMeshProUGUI TimerText;
    private int MaxTimeCounter = 5;

    public List<Sprite> ButtonSpriteList = new List<Sprite>();
    private Image buttonSpriteRen;
    public GameObject PlayerButton1;
    public GameObject PlayerButton2;
    private int lastIndex = 0;
    public TextMeshProUGUI Player1StatusText, Player2StatusText;
    public TextMeshProUGUI EventStartAnnouncement;
    public GameObject pSchild;

    public AudioSource AudioSource;

    public AudioClip InputBlockedSound;
    public AudioClip ChallengeSound;
    public AudioClip StageUpSound;
    public AudioClip StartCounterSound;
    public AudioClip WinnerSound;

    private void PlayBlockedSound()
    {
        AudioSource.clip = InputBlockedSound;
        AudioSource.Play();
    }
    private void PlayChallengeSound()
    {
        AudioSource.clip = ChallengeSound;
        AudioSource.Play();
    }
    private void PlayStageUpSound()
    {
        AudioSource.clip = StageUpSound;
        AudioSource.Play();
    }
    private async void PlayStartCounterSound()
    {
        await Task.Delay(500);
        AudioSource.clip = StartCounterSound;
        AudioSource.Play();
    }
    private void PlayWinnerSound()
    {
        AudioSource.clip = WinnerSound;
        AudioSource.Play();
    }

    void Start()
    {
        GameOngoing = false;
        Player1DistanceRemaining = TotalDistance;
        Player1DistanceMainText.text = Player1DistanceRemaining.ToString();

        Player2DistanceRemaining = TotalDistance;
        Player2DistanceMainText.text = Player2DistanceRemaining.ToString();

        Player1EventCounterGameObject.SetActive(false);
        Player2EventCounterGameObject.SetActive(false);
        Player1WinnerText.gameObject.SetActive(false);
        Player2WinnerText.gameObject.SetActive(false);

        Player1KeyText.gameObject.SetActive(false);
        Player2KeyText.gameObject.SetActive(false);

        TimerTextGameObject.SetActive(false);
        GameStartCountDown();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameOngoing)
        {
            if (!Player1PlayBlocker) CheckPlayer1Input();
            if (!Player2PlayBlocker) CheckPlayer2Input();
        }

    }

    private async void GameStartCountDown()
    {
        PlayStartCounterSound();
        int startCounter = 4;
        await Task.Delay(150);
        while (Application.isPlaying && startCounter > 0)
        {
            startCounter--;
            CountDownText.text = "The Game will start in:\n" + startCounter.ToString();
            await Task.Delay(1000);
        }

        GameOngoing = true;
        CountDownText.gameObject.SetActive(false);
        pSchild.SetActive(true);

        Player1DistanceRemaining = TotalDistance - 1;
        Player1DistanceMainText.text = Player1DistanceRemaining.ToString();
        P1DistanceSubTextUpdateLoop();

        Player2DistanceRemaining = TotalDistance - 1;
        Player2DistanceMainText.text = Player2DistanceRemaining.ToString();
        P2DistanceSubTextUpdateLoop();

        Player1KeyText.gameObject.SetActive(true);
        Player2KeyText.gameObject.SetActive(true);
        RollNextInput(1);
        RollNextInput(2);
    }

    private async void P1DistanceSubTextUpdateLoop()
    {
        int hundreds = 9;
        int msCount = 0;
        while (Application.isPlaying && GameOngoing)
        {
            await Task.Delay(100);
            msCount += 100;
            if (msCount >= 700)
            {
                hundreds = hundreds - 7;
                if (hundreds < 0) hundreds += 10;
                msCount -= 1000;
            }
            int ranTen = Random.Range(0, 9);
            int ranOnes = Random.Range(1, 9);
            // randomize tens and singles
            Player1DistanceSubText.text = "," + hundreds.ToString() + ranTen.ToString() + ranOnes.ToString() + "m";
        }
        if (!GameOngoing && Player1DistanceRemaining <= 0)
        {
            Player1DistanceSubText.text = ",000m";
        }
    }
    private async void P2DistanceSubTextUpdateLoop()
    {
        int hundreds = 8;
        int msCount = 0;
        while (Application.isPlaying && GameOngoing)
        {
            await Task.Delay(100);
            msCount += 100;
            if (msCount >= 700)
            {
                hundreds = hundreds - 7;
                if (hundreds < 0) hundreds += 10;
                msCount -= 1000;
            }
            int ranTen = Random.Range(0, 9);
            int ranOnes = Random.Range(1, 9);
            Player2DistanceSubText.text = "," + hundreds.ToString() + ranTen.ToString() + ranOnes.ToString() + "m";
        }
        if (!GameOngoing && Player2DistanceRemaining <= 0)
        {
            Player2DistanceSubText.text = ",000m";
        }
    }

    private void RollNextInput(int playerNum)
    {
        if (playerNum == 1)
        {
            Player1Loser = false;
            int ran = Random.Range(0, Player1KeyCodes.Length);
            Player1NextKey = Player1KeyCodes[ran];
            Player1KeyText.text = Player1NextKey.ToString();
        }
        else
        {
            Player2Loser = false;
            int ran = Random.Range(0, Player2KeyCodes.Length);
            Player2NextKey = Player2KeyCodes[ran];
            Player2KeyText.text = Player2NextKey.ToString();
        }
    }

    private async void StartTimeLoop()
    {
        pSchild.SetActive(false);
        TimerTextGameObject.SetActive(true);
        EventStartAnnouncement.text = "Challenge starts \n in:";
        TimerText.text = MaxTimeCounter.ToString();
        int timeCounter = MaxTimeCounter;

        while (Application.isPlaying && timeCounter > 0 && GameOngoing)
        {
            await Task.Delay(1000);

            timeCounter--;
            TimerText.text = timeCounter.ToString();
        }
        if (GameOngoing)
        {
            EventStartAnnouncement.text = "";
            TimerText.text = "0";
            Debug.Log("Event!");
            StartEvent();
            EventCountDownActive = false;
            TimerTextGameObject.SetActive(false);
            pSchild.SetActive(true);

        }

    }

    private void StartEvent()
    {
        PlayChallengeSound();

        EventOngoing = true;
        Player1PlayBlocker = false;
        Player2PlayBlocker = false;

        RollNextInput(1);
        RollNextInput(2);
        int MashRan = Random.Range(MinMashCount, MaxMashCount + 1);
        Player1CurrentMashCount = MashRan;
        Player2CurrentMashCount = MashRan;

        Player1EventCounterGameObject.SetActive(true);
        Player2EventCounterGameObject.SetActive(true);

        Player1EventCounterGameObjectText.text = Player1CurrentMashCount.ToString();
        Player2EventCounterGameObjectText.text = Player2CurrentMashCount.ToString();

        GameStagePlayer1BeforeBoost = GameStagePlayer1;
        GameStagePlayer2BeforeBoost = GameStagePlayer2;

        if (GameStagePlayer1 >= GameStagePlayer2)
        {
            GameStagePlayer2 = Mathf.Min(GameStagePlayer1 + 1, MaxStage);
            GameStagePlayer1 = GameStagePlayer2;
        }
        else
        {
            GameStagePlayer1 = Mathf.Min(GameStagePlayer1 + 1, MaxStage);
            GameStagePlayer2 = GameStagePlayer1;
        }
    }


    private void StopEvent()
    {

        EventOngoing = false;

        GameStagePlayer1 = GameStagePlayer1BeforeBoost;
        GameStagePlayer2 = GameStagePlayer2BeforeBoost;

        RollNextInput(1);
        RollNextInput(2);

        Player1EventCounterGameObject.SetActive(false);
        Player2EventCounterGameObject.SetActive(false);
    }


    private void CheckPlayer1Input()
    {
        if (Input.GetKeyDown(Player1NextKey))
        {
            ChangeButton1Visual();
            Player1PenaltyCounter = (int)Mathf.Max(0.0f, Player1PenaltyCounter - 1);


            Player1DistanceRemaining -= GameStagePlayer1;
            Player1DistanceMainText.text = Player1DistanceRemaining.ToString();

            if (Player1DistanceRemaining <= 0)
            {
                Player1DistanceRemaining = 0;
                Player1DistanceMainText.text = "0";
                GameOngoing = false;
                PlayWinnerSound();

                Debug.Log("P1 Wins!");
            }
            else
            {
                if (!EventOngoing)
                {
                    // Debug.Log("Correct");
                    RollNextInput(1);
                    Player1CorrectPresses++;

                    if (!EventCountDownActive && Player1CorrectPresses == 15)
                    {
                        GameStagePlayer1++;
                        ShowStageUpText(1);
                        Player1CorrectPresses = 0;
                        if (GameStagePlayer1 > GameStagePlayer2)
                        {
                            EventCountDownActive = true;
                            StartTimeLoop();
                        }
                    }
                    else if (Player1CorrectPresses == 15)
                    {
                        GameStagePlayer1++;
                        Player1CorrectPresses = 0;
                    }

                }
                else
                {
                    // Mashing
                    Player1CurrentMashCount--;
                    Player1EventCounterGameObjectText.text = Player1CurrentMashCount.ToString();
                    if (Player1CurrentMashCount <= 0)
                    {
                        StopEvent();
                        ShowWinnerLoop(Player1WinnerText);
                        Debug.Log("P1 wins Event");
                        ShowLoserLoop(2);

                    }
                }
            }
        }
        else if (
           Input.GetKeyDown(KeyCode.Q) ||
           Input.GetKeyDown(KeyCode.W) ||
           Input.GetKeyDown(KeyCode.E) ||
           Input.GetKeyDown(KeyCode.A) ||
           Input.GetKeyDown(KeyCode.S) ||
           Input.GetKeyDown(KeyCode.D) ||
           Input.GetKeyDown(KeyCode.Y) ||
           Input.GetKeyDown(KeyCode.X) ||
           Input.GetKeyDown(KeyCode.C)
       )
        {
            // Debug.Log("Falsch");
            if (!Player1Loser) Player1PenaltyCounter++;
            if (Player1PenaltyCounter == 7)
            {
                Player1PenaltyCounter = 0;
                PenaltyTimer(1);
                PlayBlockedSound();
                Debug.Log("Blockiert!");
            }
        }
    }

    private void CheckPlayer2Input()
    {
        if (Input.GetKeyDown(Player2NextKey))
        {
            ChangeButton2Visual();
            Player2DistanceRemaining -= GameStagePlayer2;
            Player2DistanceMainText.text = Player2DistanceRemaining.ToString();

            if (Player2DistanceRemaining <= 0)
            {
                Player2DistanceRemaining = 0;
                Player2DistanceMainText.text = "0";
                GameOngoing = false;
                PlayWinnerSound();
                Debug.Log("P2 Wins!");
            }
            else
            {
                if (!EventOngoing)
                {
                    //Debug.Log("Correct");
                    RollNextInput(2);
                    Player2CorrectPresses++;
                    if (!EventCountDownActive && Player2CorrectPresses == 15)
                    {
                        GameStagePlayer2++;
                        ShowStageUpText(2);
                        Player2CorrectPresses = 0;
                        if (GameStagePlayer2 > GameStagePlayer1)
                        {
                            EventCountDownActive = true;
                            StartTimeLoop();
                        }
                    }
                    else if (Player2CorrectPresses == 15)
                    {
                        GameStagePlayer2++;
                        Player2CorrectPresses = 0;
                    }

                }
                else
                {
                    // Mashing
                    Player2CurrentMashCount--;
                    Player2EventCounterGameObjectText.text = Player2CurrentMashCount.ToString();
                    if (Player2CurrentMashCount <= 0)
                    {
                        StopEvent();
                        Debug.Log("P2 wins Event");
                        ShowWinnerLoop(Player2WinnerText);
                        ShowLoserLoop(1);

                    }
                }
            }
        }
        else if (
           Input.GetKeyDown(KeyCode.U) ||
           Input.GetKeyDown(KeyCode.I) ||
           Input.GetKeyDown(KeyCode.O) ||
           Input.GetKeyDown(KeyCode.H) ||
           Input.GetKeyDown(KeyCode.J) ||
           Input.GetKeyDown(KeyCode.K) ||
           Input.GetKeyDown(KeyCode.B) ||
           Input.GetKeyDown(KeyCode.N) ||
           Input.GetKeyDown(KeyCode.M)
       )
        {
            // Debug.Log("Falsch");
            if (!Player2Loser) Player2PenaltyCounter++;
            if (Player2PenaltyCounter == 7)
            {
                Player2PenaltyCounter = 0;
                PlayBlockedSound();
                PenaltyTimer(2);
                Debug.Log("Blockiert!");
            }
        }
    }

    private async void ShowStageUpText(int playerNum)
    {
        PlayStageUpSound();
        if (playerNum == 1)
        {
            Player1StatusText.text = "Stage Up!";
        }
        else if (playerNum == 2)
        {
            Player2StatusText.text = "Stage Up!";
        }

        await Task.Delay(1000);

        if (playerNum == 1 && !Player1PlayBlocker)
        {
            Player1StatusText.text = "";
        }
        else if (playerNum == 2 && !Player2PlayBlocker)
        {
            Player2StatusText.text = "";
        }
    }

    private async void ShowWinnerLoop(TextMeshProUGUI Text)
    {
        Text.gameObject.SetActive(true);

        await Task.Delay(ShowWinnerTimeMS);

        Text.gameObject.SetActive(false);
    }

    private async void ShowLoserLoop(int playerNum)
    {
        if (playerNum == 1)
        {
            Player1Loser = true;
            Player1KeyText.text = "?";
        }
        else if (playerNum == 2)
        {
            Player2Loser = true;
            Player2KeyText.text = "?";
        }

        await Task.Delay(HideLoserTimeMS);

        if (playerNum == 1)
        {
            Player1Loser = false;
            Player1KeyText.text = Player1NextKey.ToString();
        }
        else if (playerNum == 2)
        {
            Player2Loser = false;
            Player2KeyText.text = Player2NextKey.ToString();
        }
    }

    public async void PenaltyTimer(int playerNum)
    {
        if (playerNum == 1)
        {
            Player1PlayBlocker = true;
            Player1StatusText.text = "You're Blocked!";
        }
        else if (playerNum == 2)
        {
            Player2PlayBlocker = true;
            Player2StatusText.text = "You're Blocked!";
        }
        await Task.Delay(PenaltyTimeMS);
        if (playerNum == 1)
        {
            Player1PlayBlocker = false;
            Player1StatusText.text = "";
        }
        else if (playerNum == 2)
        {
            Player2PlayBlocker = false;
            Player2StatusText.text = "";
        }
    }

    private void ChangeButton1Visual()
    {

        buttonSpriteRen = PlayerButton1.GetComponent<Image>();
        int ranIndex = (lastIndex + Random.Range(1, ButtonSpriteList.Count)) % ButtonSpriteList.Count;
        buttonSpriteRen.sprite = ButtonSpriteList[ranIndex];
        lastIndex = ranIndex;
    }

    private void ChangeButton2Visual()
    {
        buttonSpriteRen = PlayerButton2.GetComponent<Image>();
        int ranIndex = (lastIndex + Random.Range(1, ButtonSpriteList.Count)) % ButtonSpriteList.Count;
        buttonSpriteRen.sprite = ButtonSpriteList[ranIndex];
        lastIndex = ranIndex;
    }
}

