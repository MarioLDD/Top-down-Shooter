using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    private Slider slider;
    public Transform target;
    private Camera mainCamera;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.rotation = mainCamera.transform.rotation;
        transform.position = target.position + offset;
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}
