using UnityEngine;

public class ColorManager : MonoBehaviour
{

    static ColorManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }




    public static Color CubeColor = new Color(0.9f, 0.8f, 0.3f, 1f);


    public void setCubeColor(Color color)
    {
        CubeColor = color;
    }
    public Color getCubeColor()
    {
        return CubeColor;
    }
}
