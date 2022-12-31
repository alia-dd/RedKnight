using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_loder : MonoBehaviour
{
    public int intlev;
    public string strlev;
    public string titleMenu;

    public bool isint = false;
    public static bool ispaused = false;

    public GameObject Panel;
    //public GameObject Pause_menu;
    // Start is called before the first frame update


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(ispaused)
            {
                Resume();
                
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Panel.SetActive(false);
      //  Pause_menu.SetActive(false);
        Time.timeScale = 1f;
        ispaused = false;
    }
    void Pause()
    {
        Panel.SetActive(true);
        //Pause_menu.SetActive(true);
        Time.timeScale = 0f;
        ispaused = true;
    }
  
   
    

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void BackToTitleScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(titleMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



     private void OnTriggerEnter2D(Collider2D collision)
   {
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.name == "player")
        {
            laodScene();
        }
    }
    void laodScene()
    {
        if(isint)
        {
            SceneManager.LoadScene(intlev);
        }
        else
        {
            SceneManager.LoadScene(strlev);
        }
    }


}
