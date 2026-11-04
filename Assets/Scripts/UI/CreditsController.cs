using System.Collections;
using TMPro;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] GameObject victoryPanel;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] CanvasGroup creditsPanel;

    [Header("Textos")]
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text roleText;

    [Header("Contenido")]
    [SerializeField] string developerName = "TU NOMBRE";
    [SerializeField] float letterDelay = 0.12f;

    GameObject returnTo;

    // Enlázalos en el OnClick de los botones
    public void ShowFromVictory() { Open(victoryPanel); }
    public void ShowFromMenu() { Open(mainMenuPanel); }

    public void Back()
    {
        StopAllCoroutines();
        creditsPanel.gameObject.SetActive(false);
        if (returnTo != null) returnTo.SetActive(true);
    }

    void Open(GameObject from)
    {
        returnTo = from;
        if (from != null) from.SetActive(false);
        creditsPanel.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        // Todo con tiempo "real" por si el juego queda en Time.timeScale = 0
        titleText.text = "DESARROLLADO POR";
        nameText.text = developerName;
        roleText.text = "Diseño de Videojuegos · SENATI";

        titleText.alpha = 0; roleText.alpha = 0;
        nameText.maxVisibleCharacters = 0;
        creditsPanel.alpha = 0;

        yield return Fade(creditsPanel, 0, 1, 0.5f);
        yield return FadeText(titleText, 0, 1, 0.6f);

        // Letras del nombre una por una, con un "pop" de escala
        nameText.ForceMeshUpdate();
        int total = nameText.textInfo.characterCount;
        for (int i = 1; i <= total; i++)
        {
            nameText.maxVisibleCharacters = i;
            StartCoroutine(Pop(nameText.rectTransform));
            yield return new WaitForSecondsRealtime(letterDelay);
        }

        yield return FadeText(roleText, 0, 1, 0.8f);
    }

    IEnumerator Pop(RectTransform rt)
    {
        float t = 0;
        while (t < 0.15f)
        {
            t += Time.unscaledDeltaTime;
            float s = 1f + 0.08f * Mathf.Sin(t / 0.15f * Mathf.PI);
            rt.localScale = new Vector3(s, s, 1);
            yield return null;
        }
        rt.localScale = Vector3.one;
    }

    IEnumerator Fade(CanvasGroup g, float a, float b, float d)
    {
        for (float t = 0; t < d; t += Time.unscaledDeltaTime)
        { g.alpha = Mathf.Lerp(a, b, t / d); yield return null; }
        g.alpha = b;
    }

    IEnumerator FadeText(TMP_Text txt, float a, float b, float d)
    {
        for (float t = 0; t < d; t += Time.unscaledDeltaTime)
        { txt.alpha = Mathf.Lerp(a, b, t / d); yield return null; }
        txt.alpha = b;
    }
}