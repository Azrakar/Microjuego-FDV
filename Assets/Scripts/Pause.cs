using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private Boolean escPushed = false;
    private Estados estado = Estados.Juego;
    [SerializeField] private GameObject pantallaPausa;
    // Update is called once per frame
    void Update()
    {
        if (estado == Estados.Juego)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !escPushed)
            {
                escPushed = true;
                estado = Estados.Pausa;
                Time.timeScale = 0f;
                pantallaPausa.SetActive(true);
            }
        }

        if (!Input.GetKeyDown(KeyCode.Escape)) escPushed = false;

        if (Input.GetKeyDown(KeyCode.Escape) && !escPushed)
        {
            Time.timeScale = 1f;
            escPushed = true;
            estado = Estados.Juego;
            pantallaPausa.SetActive(false);
        }
        
        if(Input.GetKeyDown(KeyCode.Return)){
            Time.timeScale = 1f;
            escPushed = true;
            estado = Estados.Juego;
            pantallaPausa.SetActive(false);
            Pooler.ClearPools();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        

    }

    public enum Estados
    {
        Juego,
        Pausa
    }
}
