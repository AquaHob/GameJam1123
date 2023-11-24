using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour {

    [Header("UI Elements")]

    [SerializeField] private TextMeshProUGUI DistanceMainText;
    [SerializeField] private TextMeshProUGUI DistanceSubText;
    [SerializeField] private TextMeshProUGUI KeyPromptText;
    [SerializeField] private TextMeshProUGUI StatusText;
    [SerializeField] private TextMeshProUGUI EventMashCounterText;
    [SerializeField] private GameObject EventMashCounterGO;
    [SerializeField] private Image PlayerButtonImage;

    [Header("Run Variables")]
    public int playerNumber = 0;
    public KeyCode[] PossibleInputs = new KeyCode[9];   // { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Y, KeyCode.X, KeyCode.C };
                                                        // { KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.B, KeyCode.N, KeyCode.M };
    public KeyCode CurrentInputPrompt = KeyCode.Q;

    private Steuerung Steuerung;

    private bool eventOngoing = false;
    
    public int gameStage = 1;
    public int gameStageBeforeBoost = 0;

    public int currentMashCount = 0;

    public int correctInputs;
    public int failedInputCounter = 0;
    public int distanceRemaining;

    public bool eventLoser = false;
    public bool failedInputBlocker = false;

    private int lastButtonSpriteIndex = 0;

    public void Setup(Steuerung S){
        this.Steuerung = S;

        distanceRemaining = S.totalDistance;

        DistanceMainText.text = distanceRemaining.ToString();

        EventMashCounterGO.SetActive(false);
        StatusText.text = "";
        KeyPromptText.text = "";
    }

    public void GameStart(){
        distanceRemaining--;
        DistanceMainText.text = distanceRemaining.ToString();
        DistanceSubTextUpdateLoop();

        KeyPromptText.gameObject.SetActive(true);
        RollNextInput();
    }

    public void EventStart(int mashCountGiven, int boostedGameStage){
        failedInputBlocker = false;

        currentMashCount = mashCountGiven;

        EventMashCounterGO.SetActive(true);
        EventMashCounterText.text = currentMashCount.ToString();

        gameStageBeforeBoost = gameStage;
        gameStage = boostedGameStage;

        RollNextInput();
        eventOngoing = true;
    }

    public void EventStop(){
        gameStage = gameStageBeforeBoost;

        RollNextInput();

        EventMashCounterGO.SetActive(false);
        eventOngoing = false;
    }

    private async void DistanceSubTextUpdateLoop(){
        int hundreds = 9;
        int msCount = 0;
        int hundredsUpdateTimeMS = 700;

        await Task.Delay(100);

        while (Application.isPlaying && Steuerung.gameOngoing){
            if (msCount >= hundredsUpdateTimeMS){
                hundreds = hundreds - 7;
                if (hundreds < 0) hundreds += 10;
                msCount -= hundredsUpdateTimeMS;
            }
            int ranTen = UnityEngine.Random.Range(0, 9);
            int ranOnes = UnityEngine.Random.Range(1, 9);
            // randomize tens and singles
            DistanceSubText.text = "," + hundreds.ToString() + ranTen.ToString() + ranOnes.ToString() + "m";
            
            await Task.Delay(100);
            msCount += 100;
        }
        
        if (!Steuerung.gameOngoing && distanceRemaining <= 0){
            DistanceSubText.text = ",000m";
        }
    }

    public void CheckInput(){
        if (Input.GetKeyDown(CurrentInputPrompt)){
            ChangeButtonVisual();
            failedInputCounter = (int)Mathf.Max(0.0f, failedInputCounter - 1);


            distanceRemaining -= gameStage;
            DistanceMainText.text = distanceRemaining.ToString();

            if (distanceRemaining <= 0){
                PlayerWins();
            } 
            else {
                if (!eventOngoing){
                    CorrectInputDone();
                }
                else {
                    MashInputDone();
                }
            }
        }
        else if (CheckFailedInput()){
            FailedInputDone();
        }
    }

    private bool CheckFailedInput(){
        foreach(KeyCode KC in PossibleInputs){
            if(Input.GetKeyDown(KC)) return true;
        }
        return false;
    }

    private void CorrectInputDone(){
        RollNextInput();
        correctInputs++;

        if (correctInputs == 15){
            PlayerStageUp();

            if(!Steuerung.eventCountDownActive && Steuerung.GetOtherPlayer(this).gameStage < this.gameStage){
               Steuerung.StartEventCountDown(); 
            }
        }
    }

    private void FailedInputDone(){
        // Debug.Log("Falsch");
        if (!eventLoser) {
            failedInputCounter++;

            if (failedInputCounter == 7){
                Debug.Log("Blockiert!");
                failedInputCounter = 0;
                FailedInputPenalty();
                Steuerung.PlayBlockedSound();
            }
        }  
    }

    private void MashInputDone(){
        currentMashCount--;
        EventMashCounterText.text = currentMashCount.ToString();
        if (currentMashCount <= 0)
        {
            Steuerung.StopEvent();
            ShowWinnerText();
            Debug.Log("P"+this.playerNumber.ToString()+" wins Event");
            Steuerung.GetOtherPlayer(this).ShowLoserText();
        }
    }

    public void RollNextInput(){
        eventLoser = false;
        int ran = ( Array.IndexOf(PossibleInputs, CurrentInputPrompt) + UnityEngine.Random.Range(1, PossibleInputs.Length-1) ) % PossibleInputs.Length;
        CurrentInputPrompt = PossibleInputs[ran];
        KeyPromptText.text = CurrentInputPrompt.ToString();
    }

    private void ChangeButtonVisual(){
        int ranIndex = (lastButtonSpriteIndex + UnityEngine.Random.Range(1, Steuerung.ButtonSpriteList.Count)) % Steuerung.ButtonSpriteList.Count;
        PlayerButtonImage.sprite = Steuerung.ButtonSpriteList[ranIndex];
        lastButtonSpriteIndex = ranIndex;
    }

    private void PlayerStageUp(){
        if(gameStage < Steuerung.maxStage){
            ShowStageUpText();
            gameStage++;
        }
        correctInputs = 0;
    }

    private async void ShowStageUpText(){
        Steuerung.PlayStageUpSound();
        StatusText.text = "Stage Up!";

        await Task.Delay(1000);

        StatusText.text = "";
    }

    private async void ShowWinnerText(){
        StatusText.text = "Winner!";

        await Task.Delay(Steuerung.showEventWinnerTimeMS);

        StatusText.text = "";
    }

    public async void ShowLoserText(){
        eventLoser = true;
        KeyPromptText.text = "?";

        await Task.Delay(Steuerung.eventLoserPenaltyTimeMS);

        eventLoser = false;
        KeyPromptText.text = CurrentInputPrompt.ToString();
    }

    private async void FailedInputPenalty(){
        failedInputBlocker = true;
        StatusText.text = "You're Blocked!";

        await Task.Delay(Steuerung.failedInputPenaltyTimeMS);

        failedInputBlocker = false;
        StatusText.text = "";
    }

    private void PlayerWins(){
        distanceRemaining = 0;
        DistanceMainText.text = "0";
        Steuerung.RaceOver();

        Debug.Log("P"+playerNumber.ToString()+" Wins!");
    }
}