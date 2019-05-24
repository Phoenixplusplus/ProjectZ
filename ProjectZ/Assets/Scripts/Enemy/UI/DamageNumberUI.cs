using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumberUI : MonoBehaviour
{
    [Header("Text Colours")]
    [SerializeField]
    Color normalColour = Color.white;
    [SerializeField]
    Color criticalColour = Color.red;

    [Header("Text Sizing")]
    [SerializeField]
    int baseFontSize = 200;
    [SerializeField]
    int biggestFontSize = 300;
    [SerializeField]
    int smallestFontSize = 50;

    [Header("Lerp Times")]
    [SerializeField]
    float topTime = 1f;
    [SerializeField]
    float bottomTime = 1f;

    [Header("Lerp to Local Locations")]
    public Vector3 topVector = Vector3.zero;
    public Vector3 bottomVector = Vector3.zero;
    public float randomZVariant = 1f;

    [Header("Object References")]
    [SerializeField]
    GameObject mainCamera = null;
    [SerializeField]
    Text textNumber = null;

    [Header("Instantiated Data")]
    [HideInInspector]
    public bool critical = false;
    [HideInInspector]
    public string text = "";
    [HideInInspector]
    public Vector3 clickedPosition = Vector3.zero;
    Vector3 randomZVector = Vector3.zero;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        textNumber.text = text;
        randomZVector = new Vector3(0, 0, Random.Range(-randomZVariant, randomZVariant));
    }

    // Update is called once per frame
    void Update()
    {
        // Keep this element facing the camera
        transform.rotation = mainCamera.transform.rotation;
    }

    void OnEnable()
    {
        SpawnDamageNumber(critical);
    }

    void OnDisable()
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }
    #endregion

    public void SpawnDamageNumber(bool critical)
    {
        StartCoroutine(ColourFade(critical));
    }

    IEnumerator ColourFade(bool critical)
    {
        float t = 0;
        Color textColour;
        if (critical) textColour = criticalColour;
        else textColour = normalColour;
        textNumber.color = textColour;
        // Lerp to top
        while (t < topTime)
        {
            textNumber.fontSize = (int)Mathf.Lerp(textNumber.fontSize, biggestFontSize, t / topTime);
            transform.localPosition = Vector3.Lerp(clickedPosition, clickedPosition + (topVector + randomZVector), t / topTime);
            t += Time.deltaTime;
            yield return null;
        }
        // Lerp to bottom
        t = 0;
        while (t < bottomTime)
        {
            textNumber.color = new Color(textColour.r, textColour.g, textColour.b, Mathf.Lerp(1, 0, t / bottomTime));
            textNumber.fontSize = (int)Mathf.Lerp(textNumber.fontSize, smallestFontSize, t / bottomTime);
            transform.localPosition = Vector3.Lerp(clickedPosition + (topVector + randomZVector), clickedPosition + (bottomVector + (randomZVector * 2)), t / bottomTime);
            t += Time.deltaTime;
            yield return null;
        }
        this.gameObject.SetActive(false);
        yield break;
    }
}
