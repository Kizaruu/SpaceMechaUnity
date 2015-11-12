﻿using UnityEngine;
using System.Collections;

public class ServiceManager : MonoBehaviour {
    public ProfilForm profilForm;
    private ClientServices client = new ClientServices();
    private string message;
    private bool isCreated, isLogged;
    private bool canSavePrefs;
    public GameObject eventPanel;
    // Use this for initialization
    void Start () {
	
	}
	void Awake()
    {
        client.CreateProfilCompleted += Client_CreateProfilCompleted;
        client.GetProfilCompleted += Client_GetProfilCompleted;

        if (PlayerPrefs.HasKey("playerName"))
        {
            client.GetProfilAsync(PlayerPrefs.GetString("playerName"), "");

        }
        else
        {
            profilForm.gameObject.SetActive(true);
        }
    }



    private void Client_CreateProfilCompleted(object sender, CreateProfilCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            if (e.Result.id > 0)
            {
                Debug.Log("Profil created");

                canSavePrefs = true;
                isCreated = true;
            }
            else
            {
                message = "NickName already exist!";
            }
        }
        else
        {
            message = "Erreur : " + e.Error.Message;
        }
    }

    public void CreateProfil()
    {
        if (profilForm.nickname.text != "")
        {
            client.CreateProfilAsync(profilForm.nickname.text, "");
        }
        else
            profilForm.message.text = "Nickname already exists";
    }

   
    private void Client_GetProfilCompleted(object sender, GetProfilCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            if (e.Result.id > 0)
            {
                Debug.Log("Welcome " + e.Result.name);
                isLogged = true;
            }
            else
            {
                Debug.Log("Error get profil " + e.Error.Message);
            }
        }
    }

    // Update is called once per frame
    void Update () {
	if(message != "")
        {
            profilForm.message.text = message;
            message = "";
        }
    if(isCreated)
        {
            isCreated = false;
            profilForm.gameObject.SetActive(false);
        } 
    if(canSavePrefs)
        {
            canSavePrefs = false;
            PlayerPrefs.SetString("playerName", profilForm.nickname.text);
            PlayerPrefs.Save();
        }
    if(isLogged)
        {
            isLogged = false;
            eventPanel.SetActive(true);
        }
	}
}
