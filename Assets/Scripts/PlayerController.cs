using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float xLimit;
    public float horizontalSpeed;
    public float timeLeft = 0;
    private float _lastTouchedX;
    public float forwardSpeed = 10f;
    public Transform trans;
    public GameObject CubePrefab;
    public ParticleSystem plusOne;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().setAlive(true);
        Color color = GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>().getCubeColor();
        GameObject.FindGameObjectWithTag("MainCube").GetComponent<Renderer>().material.color = color;




    }
    void FixedUpdate()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (!gm.getAlive() || gm.getPauseStatus())

        {
            return;
        }

        Movement();
    }

    private void Movement()
    {
        float newX;
        float touchDeltaX = 0;
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _lastTouchedX = Input.GetTouch(0).position.x;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchDeltaX = 5 * (Input.GetTouch(0).position.x - _lastTouchedX) / Screen.width;
                _lastTouchedX = Input.GetTouch(0).position.x;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            touchDeltaX = Input.GetAxis("Mouse X");
        }
        newX = transform.position.x + horizontalSpeed * touchDeltaX * Time.deltaTime;
        newX = Mathf.Clamp(newX, -xLimit, xLimit);

        if (timeLeft > 0)
        {
            timeLeft -= Time.fixedDeltaTime;
            forwardSpeed = 20f;
        }
        else
        {
            if (forwardSpeed > 10f)
                forwardSpeed -= 0.25f;
        }



        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + forwardSpeed * Time.deltaTime);
        transform.position = newPosition;
    }



    public void CreateCube(int count)
    {
        Color color = GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>().getCubeColor();

        for (int i = 0; i < count; i++)
        {
            GameObject cube = Instantiate(CubePrefab, transform);


            cube.GetComponent<Renderer>().material.SetColor("_Color", color);

            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            cube.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            ParticleSystem.Instantiate(plusOne, cube.transform).Play();

        }

    }
    public void setSpeed(float speed)
    {
        forwardSpeed = speed;
    }
    public float getSpeed()
    {
        return forwardSpeed;
    }
    public void setBoost()
    {
        timeLeft = 1f;
    }



}