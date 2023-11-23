using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Steuerung : MonoBehaviour
{
    [Header("UI Elements")]
    [Header("General")]

    [SerializeField] private TextMeshProUGUI GameStartCountDownText;
    [SerializeField] private TextMeshProUGUI EventStartAnnouncementText;
    [SerializeField] private TextMeshProUGUI EventCountDownText;

    [Header("Game Run Variables")]

    public Player Player1;
    public Player Player2;

    public int totalDistance = 300;
    [SerializeField] private int maxStage = 4;
    [SerializeField] private int gameStartCounter = 3;
    [SerializeField] private int eventStartCounter = 5;
    [SerializeField] private int maxMashCount = 20;
    [SerializeField] private int minMashCount = 10;

    [SerializeField] public int showEventWinnerTimeMS = 1500;
    [SerializeField] public int eventLoserPenaltyTimeMS = 5000;
    [SerializeField] public int failedInputPenaltyTimeMS = 5000;

    public bool eventCountDownActive = false;
    public bool gameOngoing = true;

    [Header("Sprites & Visuals")]

    public List<Sprite> ButtonSpriteList = new List<Sprite>();
    public GameObject pSchild;

    [Header("Audio")]

    public AudioSource AudioSource;

    public AudioClip InputBlockedSound;
    public AudioClip ChallengeSound;
    public AudioClip StageUpSound;
    public AudioClip StartCounterSound;
    public AudioClip WinnerSound;

    void Start(){
        gameOngoing = false;

        Player1.Setup(this);
        Player2.Setup(this);

        GameStartCountDownText.gameObject.SetActive(true);
        EventStartAnnouncementText.gameObject.SetActive(true);
        EventCountDownText.gameObject.SetActive(true);

        GameStartCountDownText.text = "";
        EventStartAnnouncementText.text = "";
        EventCountDownText.text = "";

        GameStartCountDown();
    }

    // Update is called once per frame
    void Update(){
        if (gameOngoing){
            if (!Player1.failedInputBlocker) Player1.CheckInput();
            if (!Player2.failedInputBlocker) Player2.CheckInput();
        }
    }

    private async void GameStartCountDown(){
        PlayStartCounterSound();
        int startCounter = gameStartCounter;
        await Task.Delay(150);
        while (Application.isPlaying && startCounter > 0){
            startCounter--;
            GameStartCountDownText.text = "The Game will start in:\n\n\n" + startCounter.ToString();
            await Task.Delay(1000);
        }

        if(Application.isPlaying){
            gameOngoing = true;
            GameStartCountDownText.text = "";
            pSchild.SetActive(true);

            Player1.GameStart();
            Player2.GameStart(); 
        }
    }

    public async void StartEventCountDown(){
        eventCountDownActive = true;
        pSchild.SetActive(false);

        EventStartAnnouncementText.text = "Challenge starts \n in:";
        EventCountDownText.text = eventStartCounter.ToString();

        int timeCounter = eventStartCounter;

        while (Application.isPlaying && timeCounter > 0 && gameOngoing){
            await Task.Delay(1000);

            timeCounter--;
            EventCountDownText.text = timeCounter.ToString();
        }

        if (gameOngoing){
            EventStartAnnouncementText.text = "";
            EventCountDownText.text = "0";
            Debug.Log("Event!");
            StartEvent();
            eventCountDownActive = false;
            EventStartAnnouncementText.text = "";
            pSchild.SetActive(true);
        }
    }

    private void StartEvent(){
        PlayChallengeSound();

        int mashRan = UnityEngine.Random.Range(minMashCount, maxMashCount + 1);
        int eventGameStage = 1;

        if (Player1.gameStage >= Player2.gameStage) {
            eventGameStage = Mathf.Min(Player1.gameStage + 1, maxStage);
        }
        else {
            eventGameStage = Mathf.Min(Player1.gameStage + 1, maxStage);
        }

        Player1.EventStart(mashRan, eventGameStage);
        Player2.EventStart(mashRan, eventGameStage);
    }

    public void StopEvent(){
        Player1.EventStop();
        Player2.EventStop();

        EventCountDownText.text = "";
    }

    public bool PlayerGameStageHigherThanOther(Player P_in){
        if(P_in.playerNumber == 1){
            return P_in.gameStage > Player2.gameStage;
        }else if(P_in.playerNumber == 2){
            return P_in.gameStage > Player1.gameStage;
        }else{
            Debug.LogError("Unexpected Player Number: "+P_in.playerNumber.ToString());
            return false;
        }
    }


#region Audio Functions

    public void PlayBlockedSound()
    {
        AudioSource.clip = InputBlockedSound;
        AudioSource.Play();
    }
    public void PlayStageUpSound()
    {
        AudioSource.clip = StageUpSound;
        AudioSource.Play();
    }
    public void PlayWinnerSound()
    {
        AudioSource.clip = WinnerSound;
        AudioSource.Play();
    }
    private async void PlayStartCounterSound()
    {
        await Task.Delay(500);
        AudioSource.clip = StartCounterSound;
        AudioSource.Play();
    }
    private void PlayChallengeSound()
    {
        AudioSource.clip = ChallengeSound;
        AudioSource.Play();
    }

#endregion
}