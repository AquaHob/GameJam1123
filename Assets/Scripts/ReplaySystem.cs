using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ReplaySystem : MonoBehaviour
{
    public GameObject GameOverMenu;
    public TextMeshProUGUI WinnerText;
    public Image WinnerImage;

    [SerializeField] private Sprite Player1WinnerSprite;
    [SerializeField] private Sprite Player2WinnerSprite;

    private Steuerung Steuerung;

    private bool gameOverMenuShown = false;

    void Start(){
        GameOverMenu.SetActive(false);

        GameObject StGO = GameObject.Find("Steuerung");
        if(StGO == null){
            Debug.LogError("Couldn't find Steuerung!");
            return;
        }
        Steuerung = StGO.GetComponent<Steuerung>();
    }

    private async void StartGameOverIntputs(){
        while(Application.isPlaying && gameOverMenuShown){
            CheckGameOverInputs();
            await Task.Yield();
        }
    }

    public void ShowGameOverMenu(){
        if(gameOverMenuShown) {
            Debug.LogError("Game Over Menu already active!");
            return;
        }
        gameOverMenuShown = true;

        if(Steuerung.Player1.distanceRemaining < Steuerung.Player2.distanceRemaining){
            WinnerText.text = "Player 1 won!";
            WinnerImage.sprite = Player1WinnerSprite;
        }else{
            WinnerText.text = "Player 2 won!";
            WinnerImage.sprite = Player2WinnerSprite;
        }

        GameOverMenu.SetActive(true);
        StartGameOverIntputs();
    }

    private void CheckGameOverInputs(){
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){
            RestartGame();
        }
    }

    public void RestartGame(){
        GameOverMenu.SetActive(false);
        gameOverMenuShown = false;
        SceneManager.LoadScene("RaceScene");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
