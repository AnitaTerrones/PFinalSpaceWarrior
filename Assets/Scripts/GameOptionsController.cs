using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOptionsController : MonoBehaviour
{
    
    public GameObject optionPanel;
    
    public GameObject gameOver;//boton
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

    public void optionsPanel()//btnPausa
    {
        Time.timeScale = 0;
        optionPanel.SetActive(true);
    }

    public void Return()//regresa despues de pausar (restart)
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
        SceneManager.LoadScene("Nivel01");
    }
}
