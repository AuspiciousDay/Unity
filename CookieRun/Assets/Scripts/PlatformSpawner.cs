using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] PlatformPrefabs = new GameObject[3];
    private int initCount = 7;
    private GameObject[] TopPlatforms;
    private GameObject[] BottomPlatforms;
    private GameObject[] SmallPlatforms;
    private int[] currentIndices = new int[3]; 
    public float[] Interval = new float[3];
    public float intervalRange = 0.25f;
    public float[] CurrentInterval = new float[3];
    private float[] spawnTimers = new float[3]; 
    public float yRange = 1.5f;
    public Transform[] SpawnPoints = new Transform[3];
    public GameManager gameManager;

    private void Awake()
    {
        NewPlatforms(ref TopPlatforms, PlatformPrefabs[0]);
        NewPlatforms(ref BottomPlatforms, PlatformPrefabs[1]);
        NewPlatforms(ref SmallPlatforms, PlatformPrefabs[2]);
    }

    private void Start()
    {
        for (int i = 0; i < CurrentInterval.Length; i++)
        {
            CurrentInterval[i] = Interval[i] + Random.Range(-intervalRange, intervalRange);
        }
        spawnTimers[1] = CurrentInterval[1];
    }

    private void Update()
    {
        if (gameManager.IsGameOver)
        {
            return;
        } 
        for (int i = 0; i < spawnTimers.Length; i++)
        {
            spawnTimers[i] += Time.deltaTime;
        }
        SpawnTime(0, TopPlatforms, SpawnPoints[0], ref CurrentInterval[0], ref Interval[0]);
        SpawnTime(1, BottomPlatforms, SpawnPoints[1], ref CurrentInterval[1], ref Interval[1]);
        SpawnTime(2, SmallPlatforms, SpawnPoints[2], ref CurrentInterval[2], ref Interval[2], true);
    }
    private void SpawnTime(int typeIndex, GameObject[] gameObjects, Transform targetTransform, ref float currentInterval, ref float interval, bool trans = false)
    {
        if (spawnTimers[typeIndex] > currentInterval)
        {
            spawnTimers[typeIndex] = 0f; 
            currentInterval = interval + Random.Range(-intervalRange, intervalRange);
            Spawn(typeIndex, gameObjects, targetTransform, trans);
        }
    }

    private void NewPlatforms(ref GameObject[] gameObjects, GameObject prefabObject)
    {
        gameObjects = new GameObject[initCount];
        for(int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i] = Instantiate(prefabObject);
            gameObjects[i].SetActive(false);
        }
    }
    private void Spawn(int typeIndex, GameObject[] gameObjects, Transform targetTransform, bool trans)
    {
        int index = currentIndices[typeIndex];
        gameObjects[index].SetActive(false);
        gameObjects[index].transform.position = targetTransform.position; 
        if (trans)
        {
            Vector3 point = targetTransform.position;
            point.y += Random.Range(-yRange, yRange);
            gameObjects[index].transform.position = point;
            foreach(Transform child in gameObjects[index].transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        gameObjects[index].SetActive(true);
        currentIndices[typeIndex] = (index + 1) % gameObjects.Length;
    }
}
