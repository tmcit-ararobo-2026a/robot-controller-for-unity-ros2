using UnityEngine;
using ROS2;
using UnityEngine.UI;
using TMPro;
public class throwtextY : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI textMeshPro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = "throw_speedY   " + slider.value;
    }
}
