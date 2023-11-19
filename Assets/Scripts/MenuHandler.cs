using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("RaceScene");
    }

    public void ShowCredits(){
        Debug.Log("ToDo");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
