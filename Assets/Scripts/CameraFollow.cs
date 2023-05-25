using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private bool MultiplierHitStatus = false;
    public GameManager gm;
    void Start()
    {
        offset = transform.position - player.transform.position;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gm.getOver())
        {
            Vector3 pos = player.transform.position;
            transform.LookAt(new Vector3(pos.x + 1.15f, transform.position.y - 4f, pos.z + 1.5f));
            transform.Translate(Vector3.right * Time.deltaTime);

        }
        else
        {

            if (MultiplierHitStatus)
            {
                MultiplierHitStatus = false;
                Vector3 pos = player.transform.position + offset;
                transform.position = new Vector3(pos.x, transform.position.y + 1, pos.z);
            }
            else
            {

                Vector3 pos = player.transform.position + offset;
                transform.position = new Vector3(pos.x, transform.position.y, pos.z);
            }
        }

    }
    public void MultiplierHit()
    {
        MultiplierHitStatus = true;
    }

}

