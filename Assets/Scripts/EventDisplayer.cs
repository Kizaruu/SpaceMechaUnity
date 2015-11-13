using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventDisplayer : MonoBehaviour {
    private ClientServices clientServices = new ClientServices();
    private bool isEventFound;
    public Text eventName;
    public Image eventImg;
    public GameObject eventPricePref, startBtn;
    private EventGame currentEvent;
    public RectTransform scrollable, window;
	// Use this for initialization
	void Start () {
        clientServices.GetEventGameCompleted += ClientServices_GetEventGameCompleted;
        clientServices.GetEventGameAsync();
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
            eventImg.sprite = ConvertByteToSprite(currentEvent.eventS.image);
            
            foreach (var item in currentEvent.priceS)
            {
                GameObject price = Instantiate(eventPricePref);
                price.transform.SetParent(scrollable);
                price.transform.localScale = Vector3.one;

                if(item.image != null)
                {
                    price.GetComponent<Image>().sprite = ConvertByteToSprite(item.image);
                }
            }
        }
	}

    private Sprite ConvertByteToSprite(byte[] data)
    {
        Texture2D text = new Texture2D(1, 1);
        text.LoadImage(data);

        Sprite sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(.5f, .5f));
        return sprite;
    }
}
