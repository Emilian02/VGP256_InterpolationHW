using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] Slider slider;
    [SerializeField] GameObject[] curves;

     int currentIndex = 0;
     Catmull_Rom_Spline catmullRomSpline;

    void Start()
    {
        slider.gameObject.SetActive(false);
        UpdateActiveObject();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {
        if (curves[currentIndex].CompareTag("Catmull-Rom") && Input.GetKeyDown(KeyCode.Space))
        {
            slider.gameObject.SetActive(true);
            if (catmullRomSpline != null)
            {
                slider.value = catmullRomSpline.tension;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            slider.gameObject.SetActive(false);
        }
    }

    void UpdateActiveObject()
    {
        foreach (GameObject obj in curves)
        {
            obj.SetActive(false);
        }

        // Activate the current object
        curves[currentIndex].SetActive(true);

        // Update button states
        leftButton.interactable = currentIndex > 0;
        rightButton.interactable = currentIndex < curves.Length - 1;

        if (curves[currentIndex].CompareTag("Catmull-Rom"))
        {
            catmullRomSpline = curves[currentIndex].GetComponent<Catmull_Rom_Spline>();
        }
        else
        {
            catmullRomSpline = null;
        }
    }

    public void RightButton()
    {
        if (currentIndex < curves.Length - 1)
        {
            currentIndex++;
            UpdateActiveObject();
        }
    }

    public void LeftButton()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateActiveObject();
        }
    }

    void OnSliderValueChanged(float value)
    {
        if (catmullRomSpline != null)
        {
            catmullRomSpline.tension = value;
        }
    }
}
