using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    public GameObject TitleMenu;
    public GameObject CreditsMenu;

    void Start()
    {
        TitleMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        waitForCutscene();
    }

    private async void waitForCutscene()
    {
        await Task.Delay(12000);
        LoadCreditsMenu(false);
    }

    public void StartGame(){
        SceneManager.LoadScene("RaceScene");
    }

    public void ShowCredits(){
        LoadCreditsMenu(true);
    }

    public void BackToTitle(){
        LoadCreditsMenu(false);
    }

    public void QuitGame(){
        Application.Quit();
    }

    private void LoadCreditsMenu(bool on){
        if(on){
            TitleMenu.SetActive(false);
            CreditsMenu.SetActive(true);
        }else{
            TitleMenu.SetActive(true);
            CreditsMenu.SetActive(false);
        }
    }
}
