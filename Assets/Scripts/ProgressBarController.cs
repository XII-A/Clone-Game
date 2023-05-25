using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public GameObject playerContainer;
    public Slider slider;

    void Update()
    {
        var zVal = playerContainer.transform.position.z;
        Transform platform = GameObject.FindGameObjectWithTag("Platform").transform;
        var scaleZ = platform.localScale.z;
        var progress = zVal * 100 / scaleZ;
        if (progress <= 100)
        {
            slider.value = (progress) / 100f;
        }
        else
        {
            slider.value = 100 / 100;
        }
    }
}
