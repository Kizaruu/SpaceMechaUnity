using UnityEngine;
using System.Collections;

public class DisplayHidePanel : MonoBehaviour {
    public GameObject[] toDisplay;
    public GameObject[] toHide;

	public void Launch () {
	    for (int i =0; i < toDisplay.Length; i++)
        {
            toDisplay[i].SetActive(true);
        }
        for (int i = 0; i < toHide.Length; i++)
        {
            toHide[i].SetActive(false);
        }
    }
}
