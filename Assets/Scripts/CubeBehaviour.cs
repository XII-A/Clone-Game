using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    public GameObject CubePrefab;
    public ParticleSystem plusOne;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupParent"))
        {
            PickUpTrigger(other);
        }
        if (other.tag == "Obstacle")
        {
            ObstacleTrigger(other);

        }
        if (other.tag == "Lava")
        {
            LavaTrigger(other);


        }
        if (other.tag == "SpeedBoost")
        {
            SpeedBoostTrigger(other);


        }
        if (other.tag == "Multiplier")
        {
            MultiplierTrigger(other);

        }
        if (other.tag == "FinishLine")
        {
            FinishLineTrigger(other);
        }
        if (
            other.tag == "Diamond"
        )
        {
            DiamondTrigger(other);

        }

    }

    private void DiamondTrigger(Collider other)
    {
        GameObject diamond = other.gameObject;
        Destroy(diamond);
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.CollectCoin();
        SoundManager.PlaySound(SoundManager.Sound.Coin);
    }

    private void FinishLineTrigger(Collider other)
    {
        other.tag = "Hit";
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (gm.GetFinishLine())
        {
            gm.EndGame("FinishLine");
        }
        else
        {
            gm.FinishLine();
        }
    }

    private void MultiplierTrigger(Collider other)
    {
        other.tag = "Hit";
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (transform.tag == "MainCube")
        {
            gm.EndGame("Multiplier");
            return;
        }

        Transform destroyedCubes = GameObject.FindGameObjectWithTag("DestroyedCubes").transform;
        transform.SetParent(destroyedCubes);
        gm.setMultiplier();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().MultiplierHit();

        SoundManager.PlaySound(SoundManager.Sound.Obstacle);
    }

    private void SpeedBoostTrigger(Collider other)
    {
        other.isTrigger = false;
        GameObject player = GameObject.FindGameObjectWithTag("PlayerContainer");
        player.GetComponent<PlayerController>().setBoost();
        SoundManager.PlaySound(SoundManager.Sound.Speed);
    }

    private void LavaTrigger(Collider other)
    {
        if (transform.tag == "MainCube")
        {
            GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.EndGame("Lava");
            return;
        }
        Destroy(gameObject);
        SoundManager.PlaySound(SoundManager.Sound.Lava);
    }

    private void PickUpTrigger(Collider other)
    {
        other.enabled = false;
        int count = other.gameObject.GetComponent<CubeInstantiator>().count;
        Destroy(other.gameObject);
        CreateCube(count);
        SoundManager.PlaySound(SoundManager.Sound.Pop);
    }

    private void ObstacleTrigger(Collider other)
    {
        other.tag = "Hit";

        for (int i = 0; i < other.transform.parent.parent.childCount; i++)
        {
            for (int j = 0; j < other.transform.parent.parent.GetChild(i).childCount; j++)
            {
                other.transform.parent.parent.GetChild(i).GetChild(j).tag = "Hit";
            }
        }

        if (transform.tag == "MainCube")
        {
            GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.EndGame("Obstacle");
            return;
        }
        Transform destroyedCubes = GameObject.FindGameObjectWithTag("DestroyedCubes").transform;
        if (other.transform.parent.parent.parent.tag == "Cylinder")
        {
            transform.SetParent(other.transform.parent.parent.parent);

        }
        else
        {

            transform.SetParent(destroyedCubes);
        }
        if (transform.tag == "Cube")
        {
            SoundManager.PlaySound(SoundManager.Sound.Obstacle);

        }
        transform.tag = "Hit";

    }

    private void CreateCube(int count)
    {

        Color color = GameObject.FindGameObjectWithTag("ColorManager").GetComponent<ColorManager>().getCubeColor();

        for (int i = 0; i < count; i++)
        {

            Transform parent = GameObject.FindGameObjectWithTag("PlayerContainer").transform;
            GameObject cube = Instantiate(CubePrefab, parent);

            cube.GetComponent<Renderer>().material.SetColor("_Color", color);

            parent.position = new Vector3(parent.position.x, parent.position.y + 1, parent.position.z);
            cube.transform.position = new Vector3(parent.position.x, 1, parent.position.z);
            ParticleSystem.Instantiate(plusOne, cube.transform).Play();

        }


    }

}
