using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private PipeManager pipeManager;
    [SerializeField] private Player player;
    [SerializeField] private GameObject pickUpObject;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private GameObject[] obstacleObjects;

    [SerializeField] private Single difficultyModif;
    [SerializeField] private Int32 freeLane;

    // static lane configurations
    public static Vector3[] Lanes = new Vector3[3] {
        new Vector3(-3.0f, -1.25f, 0.0f),
        new Vector3( 0.0f, -3.25f, 0.0f),
        new Vector3(3.0f, -1.25f, 0.0f) };

    public static Int32[][] LaneConfigurations = new Int32[3][] {
        new Int32[2]{ 1, 2 },
        new Int32[2]{ 0, 2 },
        new Int32[2]{ 0, 1 } };

    public static Quaternion[][] LaneQuaternions = new Quaternion[3][] {
        new Quaternion[2]{ Quaternion.identity, Quaternion.AngleAxis(30, Vector3.forward)},
        new Quaternion[2]{ Quaternion.AngleAxis(-30, Vector3.forward), Quaternion.AngleAxis(30, Vector3.forward) },
        new Quaternion[2]{ Quaternion.AngleAxis(-30, Vector3.forward), Quaternion.identity } };

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutines();
    }

    public void StartCoroutines()
    {
        difficultyModif = 1.0f;
        // start spawning obstacles
        StartCoroutine("SpawnObstaclesCoroutine");
        // start the coroutine for spawning items
        StartCoroutine("SpawnPickupsCoroutine");
        // start coroutine for spawning powerups
        StartCoroutine("SpawnPowerUpsCoroutine");
    }

    // continually spawn pickups every ten seconds
    IEnumerator SpawnPickupsCoroutine()
    {
        while (true)
        {
            SpawnPickups(10);

            yield return new WaitForSeconds(15.0f / (player.speed * 0.1f));
        }
    }

    IEnumerator SpawnObstaclesCoroutine()
    {
        while (true)
        {
            // randomly pick a lane
            freeLane = UnityEngine.Random.Range(0, 3);

            // spawn obstacles in remaining ones
            SpawnObstacles();

            // wait for 5 seconds before adding obstacles
            yield return new WaitForSeconds(5.0f / (player.speed * 0.1f * difficultyModif));
            difficultyModif = 1.0f + (player.score / 2000) * 0.25f; // increase difficulty every 2000 points (obstacles appear more often)
        }
    }

    IEnumerator SpawnPowerUpsCoroutine()
    {
        GameObject powerUp;
        while (true)
        {
            // attempt to randomly generate a power up
            if (UnityEngine.Random.Range(0, 2000) > 1900)
            {
                powerUp = Instantiate(powerUps[UnityEngine.Random.Range(0, powerUps.Length)], 
                    pipeManager.pipeStack[pipeManager.bottomOfPipe].transform);
                powerUp.transform.Translate(Lanes[freeLane] - new Vector3(0.0f, 0.0f, PipeManager.PipeLength * 0.5f));
                powerUp.GetComponent<PowerUp>().type = PowerUp.Type.Time; // just a time slower for now

                // success, so wait 1 minute till spawning
                yield return new WaitForSeconds(60.0f / (player.speed * 0.1f));
            }
            else
            {
                // failure, try again in 20 seconds
                yield return new WaitForSeconds(20.0f / (player.speed * 0.1f));
            }
        }
    }

    void SpawnObstacles()
    {
        GameObject obstacle;
        switch (UnityEngine.Random.Range(0, 3))
        {
            // spawn both obstacles
            case 0:
            {
                // generate a random obstacle>
                obstacle = Instantiate(obstacleObjects[UnityEngine.Random.Range(0, obstacleObjects.Length)], pipeManager.BottomOfPipe().transform); // the transform of the gameobject the pipe component is attached to
                obstacle.transform.Translate(Lanes[LaneConfigurations[freeLane][0]] + new Vector3(0.0f, 0.0f, pipeManager.BottomOfPipe().currentOffset));
                obstacle.transform.rotation = LaneQuaternions[freeLane][0];

                obstacle = Instantiate(obstacleObjects[UnityEngine.Random.Range(0, obstacleObjects.Length)], pipeManager.BottomOfPipe().transform);
                obstacle.transform.Translate(Lanes[LaneConfigurations[freeLane][1]] + new Vector3(0.0f, 0.0f, pipeManager.BottomOfPipe().currentOffset));
                obstacle.transform.rotation = LaneQuaternions[freeLane][1];
                break;
            }
            case 1:
            {
                obstacle = Instantiate(obstacleObjects[UnityEngine.Random.Range(0, obstacleObjects.Length)], pipeManager.BottomOfPipe().transform); // the transform of the gameobject the pipe component is attached to
                obstacle.transform.Translate(Lanes[LaneConfigurations[freeLane][0]] + new Vector3(0.0f, 0.0f, pipeManager.BottomOfPipe().currentOffset));
                obstacle.transform.rotation = LaneQuaternions[freeLane][0];
                break;

            }
            case 2:
            {
                obstacle = Instantiate(obstacleObjects[UnityEngine.Random.Range(0, obstacleObjects.Length)], pipeManager.BottomOfPipe().transform); // the transform of the gameobject the pipe component is attached to
                obstacle.transform.Translate(Lanes[LaneConfigurations[freeLane][1]] + new Vector3(0.0f, 0.0f, pipeManager.BottomOfPipe().currentOffset));
                obstacle.transform.rotation = LaneQuaternions[freeLane][1];
                break;
            }
        }
        pipeManager.BottomOfPipe().currentOffset += PipeManager.PipeLength * 0.3f;
    }


    // spawn n pickups in a line
    void SpawnPickups(Int32 numPickups)
    {
        // get the transform to the pipe furthest from the player
        Transform parent = pipeManager.pipeStack[pipeManager.bottomOfPipe].transform;
        Single zSpacing = PipeManager.PipeLength / (Single)numPickups;
        // geometry for pickups
        Vector3 offset = new Vector3(0, 0, PipeManager.PipeLength * 0.5f);

        for (Int32 p = 0; p < numPickups; p++)
        {
            // pick ups are now children of the pipe object
            GameObject spawnedPickUpObject = Instantiate(pickUpObject, parent);
            // place them relative to the 
            spawnedPickUpObject.transform.Translate(Lanes[freeLane] + offset);
            offset.z -= zSpacing;
        }        
    }

    // remove all objects that get beyond the player
    void OnTriggerEnter(Collider collider)
    {
        Destroy(collider.gameObject);
    }
}
