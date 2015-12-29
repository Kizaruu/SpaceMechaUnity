using UnityEngine;
using System.Collections;

public class StartPractiseClick : MonoBehaviour {

    public void OnClick()
    {
        ApplicationModel.idEvent = 0;
        Application.LoadLevel(1);
    }
}
