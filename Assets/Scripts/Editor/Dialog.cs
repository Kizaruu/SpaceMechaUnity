using UnityEditor;
using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour
{
    public static void DialogPlayer(EventGame wonEvent)
    {
        if (EditorUtility.DisplayDialog("Bravo Champion!", "Félicitation " + ApplicationModel.playerName +
        " ! Lors de votre participation à l'event " + wonEvent.eventS.name + " vous avez gagné " +
        wonEvent.priceS.Length.ToString() + " lot(s), voulez-vous télécharger ça maintenant?", "Oui!", "Non, plus tard..."))
        {
            for (int j = 0; j < wonEvent.priceS.Length; j++)
            {
                string nomPrix = wonEvent.priceS[j].name;

                string savePath = EditorUtility.SaveFilePanel("Félicitation " + ApplicationModel.playerName +
                " ! vous avez gagné un lot lors de votre participation à l'event " + wonEvent.eventS.name + "!", "", nomPrix, "");

                if (savePath != "")
                {
                    System.IO.File.WriteAllBytes(savePath, wonEvent.priceS[j].path);
                }
            }
            PlayerPrefs.SetString("\n" + wonEvent.eventS.id.ToString(), "\n");
            PlayerPrefs.Save();
        }
    }
}