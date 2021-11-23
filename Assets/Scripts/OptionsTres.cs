using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsTres : MonoBehaviour
{
   public GameObject optionPanel;
    
    public GameObject gameOver;
    public static GameObject gameOverStatic; 

    void Start()
    {
        GameOptionsController.gameOverStatic = gameOver;
        GameOptionsController.gameOverStatic.gameObject.SetActive(false);
        optionPanel.SetActive(false);
    }

   public static void mostrarGameOver()
    {
        GameOptionsController.gameOverStatic.gameObject.SetActive(true);
    }

    public void optionsPanel()
    {
        Time.timeScale = 0;
        optionPanel.SetActive(true);
    }

    public void Return()//regresa depues de pausar (restart)
    {
        Time.timeScale = 1;
        optionPanel.SetActive(false);
    }

    public void returnMenu()//exit
    {
        SceneManager.LoadScene("InicioMenu"); 
    }

    public void reinicioTrasMorir()
    {
        SceneManager.LoadScene("Nivel03");
    }
}