using UnityEngine;

public class CylinderBehaviour : MonoBehaviour
{
    public GameManager gm;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    void FixedUpdate()
    {
        if (!gm.getAlive() || gm.getPauseStatus())
        {
            return;
        }
        transform.Rotate(0, 2, 0);
    }
}
