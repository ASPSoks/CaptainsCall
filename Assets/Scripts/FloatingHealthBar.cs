using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;
    public void UpdateHealthBar(float currentValue, float maxvalue){
        slider.value = currentValue/maxvalue;
    }
  
    void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = target.position +offset;
    }
}
