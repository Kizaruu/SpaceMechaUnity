using UnityEngine;
using UnityEngine.UI;

public class EventDisplayer : MonoBehaviour {
    private ClientServices clientServices = new ClientServices();
    private bool isEventFound, winnerPreviousEvents;
    public Text eventName, playerName;
    public Image eventImg;
    public GameObject eventPricePref, startBtn;
    private EventGame currentEvent;
    private EventGame[] eventsWon = null;
    public RectTransform scrollable, window;
    int[] allPastEvents = null;
    int numEventWon = 0;
	// Use this for initialization
	void Start () {
        playerName.text = ApplicationModel.playerName;
        clientServices.GetEventGameCompleted += ClientServices_GetEventGameCompleted;
        clientServices.GetEventGameAsync();
        clientServices.GetAllFinishedEventCompleted += ClientServices_GetAllFinishedEventCompleted;
        clientServices.GetAllFinishedEventAsync();
        clientServices.GetEarnedPricesCompleted += ClientServices_GetEarnedPricesCompleted;
        
    }

    private void ClientServices_GetEarnedPricesCompleted(object sender, GetEarnedPricesCompletedEventArgs e)
    {
        if (e.Result != null)
        {
            numEventWon++;
            if (eventsWon == null)
            {
                eventsWon = new EventGame[1];
                eventsWon[0] = e.Result;
            }
            else
            {
                EventGame[] tempEvents = eventsWon;
                eventsWon = new EventGame[numEventWon];
                int i;
                for (i = 0; i < tempEvents.Length; i++)
                {
                    eventsWon[i] = tempEvents[i];
                }
                eventsWon[i] = e.Result;
            }
        }
    }

    private void ClientServices_GetAllFinishedEventCompleted(object sender, GetAllFinishedEventCompletedEventArgs e)
    {
        if (e.Result != null)
        {
            if (allPastEvents == null)
            {
                allPastEvents = e.Result;
            }
            /*
            else
            {
                int[] tempEvents = allPastEvents;
                int i;
                for (i = 0; i < tempEvents.Length; i++)
                {
                    allPastEvents[i] = tempEvents[i];
                }
                for (; i < e.Result.Length; i++)
                {
                    allPastEvents[i] = e.Result[i];
                }
            }
            */
        }       
    }

    private void ClientServices_GetEventGameCompleted(object sender, GetEventGameCompletedEventArgs e)
    {
        if(e.Error == null)
        {
            if(e.Result != null)
            {
                currentEvent = e.Result;
                isEventFound = true;
            }
            else
            {
                Debug.Log("No event found");
            }
        }
        else
        {
            Debug.LogError("Get event Error " + e.Error.Message);
        }
    }

    // Update is called once per frame
    void Update () {
	    if (isEventFound)
        {
            isEventFound = false;
            window.gameObject.SetActive(true);
            eventName.text = currentEvent.eventS.name;
            eventImg.gameObject.SetActive(true);
            eventImg.sprite = ConvertByteToSprite(currentEvent.eventS.image);
            startBtn.gameObject.SetActive(true);

            ApplicationModel.idEvent = currentEvent.eventS.id;

            foreach (var item in currentEvent.priceS)
            {
                GameObject price = Instantiate(eventPricePref);
                price.transform.SetParent(scrollable);
                price.transform.localScale = Vector3.one;

                if(item.image != null)
                {
                    price.GetComponent<Image>().sprite = ConvertByteToSprite(item.image);
                }

                price.SetActive(true);
            }
        }
        
        if (allPastEvents != null)
        {
            for (int i = 0; i < allPastEvents.Length; i++)
            {
                if (!PlayerPrefs.HasKey("\n" + allPastEvents[i].ToString()))
                    clientServices.GetEarnedPricesAsync(ApplicationModel.idProfil, allPastEvents[i]);
                else
                {
                    // pour test
                    PlayerPrefs.DeleteKey("\n" + allPastEvents[i].ToString());
                    PlayerPrefs.Save();
                }
            }
            allPastEvents = null;
        }
        /*
        if (numEventWon > 0)
        {
            for (int i = 0; i < numEventWon; i++)
            {
                //Dialog.DialogPlayer(eventsWon[i]);

                System.

                if (EditorUtility.DisplayDialog("Bravo Champion!", "Félicitation " + ApplicationModel.playerName +
                        " ! Lors de votre participation à l'event " + eventsWon[i].eventS.name + " vous avez gagné "+ 
                        eventsWon[i].priceS.Length.ToString() + " lot(s), voulez-vous télécharger ça maintenant?", "Oui!", "Non, plus tard..."))
                {
                    for (int j = 0; j < eventsWon[i].priceS.Length; j++)
                    {
                        string nomPrix = eventsWon[i].priceS[j].name;

                        string savePath = EditorUtility.SaveFilePanel("Félicitation " + ApplicationModel.playerName +
                            " ! vous avez gagné un lot lors de votre participation à l'event " + eventsWon[i].eventS.name + "!", "", nomPrix, "");

                        if (savePath != "")
                        {
                            System.IO.File.WriteAllBytes(savePath, eventsWon[i].priceS[j].path);
                        }
                    }
                    PlayerPrefs.SetString("\n" + eventsWon[i].eventS.id.ToString(), "\n");
                    PlayerPrefs.Save();
                }
                
            }
            numEventWon = 0;
            eventsWon = null;
        }
        */
	}
    
    private Sprite ConvertByteToSprite(byte[] data)
    {
        Texture2D text = new Texture2D(1, 1);
        text.LoadImage(data);

        Sprite sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(.5f, .5f));
        return sprite;
    }
}
