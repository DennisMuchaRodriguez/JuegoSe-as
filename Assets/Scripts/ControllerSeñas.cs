using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerSeñas : MonoBehaviour
{
    public TMP_InputField inputFieldTMP; 
    public Image signImage;
    public Button showSignButton;
    public GameObject ActiveButton;
    private Dictionary<char, Sprite> signLanguageImages = new Dictionary<char, Sprite>();
    private Coroutine displayCoroutine;  

    void Start()
    {
        
        LoadSignLanguageImages();

      
        showSignButton.onClick.AddListener(OnButtonClick);
    }

    void LoadSignLanguageImages()
    {
        
        foreach (char letter in "ABCDEFGHIJKLMNOPQRSTUVWXYZÑ")
        {
            Sprite sprite = Resources.Load<Sprite>("SignLanguageSprites/" + letter);
            if (sprite != null)
            {
                signLanguageImages.Add(letter, sprite);
            }
        }
    }

    void OnButtonClick()
    {
        // Obtener el texto del InputField
        string text = inputFieldTMP.text;

        if (!string.IsNullOrEmpty(text))
        {
            // Si ya hay una corutina en marcha, detenerla
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine);
            }

            // Iniciar la corutina para mostrar las imágenes en secuencia
            displayCoroutine = StartCoroutine(ShowSignImages(text.ToUpper()));
            ActiveButton.SetActive(false);
        }
    }

    IEnumerator ShowSignImages(string word)
    {
        // Recorrer cada letra de la palabra ingresada
        foreach (char letter in word)
        {
            // Verificar si existe una imagen para la letra en el diccionario
            if (signLanguageImages.ContainsKey(letter))
            {
                // Mostrar la imagen correspondiente
                signImage.sprite = signLanguageImages[letter];
            }
            else
            {
                Debug.LogWarning("No se encontró imagen para la letra: " + letter);
            }

            // Esperar 1 segundo antes de mostrar la siguiente imagen
            yield return new WaitForSeconds(1f);



        }
        ActiveButton.SetActive(true);
    }

}
