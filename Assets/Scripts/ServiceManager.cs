using UnityEngine;
using System.Collections;

public class ApplicationModel : MonoBehaviour
{
    static public int idEvent = 0;
    static public int idProfil = 0;
}

public class ServiceManager : MonoBehaviour {
    public ProfilForm profilForm;
    private ClientServices client = new ClientServices();
    private string message;
    private bool isCreated, isLogged;
    private bool canSavePrefs;
    private string token = "";
    public GameObject eventPanel;
    //static public int idEvent = 0;

    // Use this for initialization
    void Start () {
        //PlayerPrefs.DeleteAll();
	}
	void Awake()
    {
        client.CreateProfilCompleted += Client_CreateProfilCompleted;
        client.GetProfilCompleted += Client_GetProfilCompleted;

        if (PlayerPrefs.HasKey("playerName"))
        {
            client.GetProfilAsync(PlayerPrefs.GetString("playerName"), PlayerPrefs.GetString(PlayerPrefs.GetString("playerName")));
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
                message = "Profil created";

                this.token = e.Result.tokenid;

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
            Debug.Log ("Erreur : " + e.Error.Message);
        }

        if (!isCreated)
        {
            PlayerPrefs.DeleteAll();
            profilForm.gameObject.SetActive(true);
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
                message = ("Welcome " + e.Result.name);
                isLogged = true;
                ApplicationModel.idProfil = e.Result.id;
            }
            else
            {
                Debug.Log("Error get profil " + e.Error.Message);
            }
        }
        else
        { 
            Debug.Log("Erreur : " + e.Error.Message);
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
            isLogged = true;
        } 
    if(canSavePrefs)
        {
            canSavePrefs = false;
            PlayerPrefs.SetString("playerName", profilForm.nickname.text);
            PlayerPrefs.SetString(profilForm.nickname.text, this.token);
            PlayerPrefs.Save();
        }
    if(isLogged)
        {
            isLogged = false;
            eventPanel.SetActive(true);
        }
	}
}
