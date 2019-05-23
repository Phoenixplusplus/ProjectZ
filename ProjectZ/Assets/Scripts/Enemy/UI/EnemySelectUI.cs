using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySelectUI : MonoBehaviour
{
    [Header("Image Scaling")]
    [SerializeField]
    Vector3 baseScale = new Vector3(0.004f, 0.004f, 1);
    [SerializeField]
    float scaleDivision = 6f;

    [Header("Image Colour")]
    [SerializeField]
    Color initialColour = Color.white;
    [SerializeField]
    Color endColour = Color.red;

    [Header("Object References")]
    [SerializeField]
    GameObject mainCamera = null;
    [SerializeField]
    Image imageRing = null;

    #region Unity API
    // Start is called before the first frame update
    void Start()
    {
        EnemyDeselected();
    }

    // Update is called once per frame
    void Update()
    {
        // Keep this element facing the camera
        transform.rotation = mainCamera.transform.rotation;
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
    #endregion

    public void EnemySelected(float time)
    {
        StopAllCoroutines();
        this.gameObject.SetActive(true);
        StartCoroutine(ScaleImage(time));
    }

    public void EnemyDeselected()
    {
        this.gameObject.SetActive(false);
    }

    IEnumerator ScaleImage(float time)
    {
        float t = 0;
        while (t < time)
        {
            imageRing.color = Color.Lerp(initialColour, endColour, t / time);
            transform.localScale = Vector3.Lerp(baseScale / scaleDivision, baseScale, t / time);
            t += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}
