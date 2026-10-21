using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject TopPlatformPrefab;
    public GameObject BottomPlatformPrefab;
    public GameObject SmallPlatformPrefab;
    private int initCount = 5;
    private GameObject[] TopPlatforms;
    private GameObject[] BottomPlatforms;
    private GameObject[] SmallPlatforms;
    private int currentIndex = 0;
    public float interval = 1f;
    private float intervalRange = 0.25f;
    private float currentInterval;
    public float yRange = 1.5f;
    private float spawnTimer = 0f;
    public Transform TopSpawnPoint;
    public Transform BottomSpawnPoint;
    public Transform SmallSpawnPoint;
    public GameManager gameManager;
    private bool canSpawn;
    
    private void Awake()
    {
        TopPlatforms = new GameObject[initCount];
        for(int i = 0; i < TopPlatforms.Length; i++)
        {
            TopPlatforms[i] = Instantiate(TopPlatformPrefab);
            TopPlatforms[i].SetActive(false);
        }
        BottomPlatforms = new GameObject[initCount];
        for(int i = 0; i < BottomPlatforms.Length; i++)
        {
            BottomPlatforms[i] = Instantiate(BottomPlatformPrefab);
            BottomPlatforms[i].SetActive(false);
        }
        SmallPlatforms = new GameObject[initCount];
        for(int i = 0; i < SmallPlatforms.Length; i++)
        {
            SmallPlatforms[i] = Instantiate(SmallPlatformPrefab);
            SmallPlatforms[i].SetActive(false);
        }
    }
    private void Start()
    {
        currentInterval = Random.Range(-intervalRange, intervalRange);
        spawnTimer = currentInterval;
    }
    private void Update()
    {
        if (gameManager.IsGameOver)
        {
            return;
        }
        spawnTimer += Time.deltaTime;
        if(spawnTimer > currentInterval)
        {
            spawnTimer = 0f;
            currentInterval = interval + Random.Range(-intervalRange, intervalRange);
            Spawn(TopPlatforms, TopSpawnPoint);
            Spawn(BottomPlatforms, BottomSpawnPoint);
            canSpawn = true;
        }
        if(spawnTimer > currentInterval - 0.1f && canSpawn)
        {
            Spawn(SmallPlatforms, SmallSpawnPoint, true);
        }
    }
    private void Spawn(GameObject[] gameObjects, Transform transform, bool trans = false)
    {
        gameObjects[currentIndex].SetActive(false);
        gameObjects[currentIndex].transform.position = transform.transform.position;
        if (trans)
        {
            Vector3 point = transform.position;
            point.y += Random.Range(-yRange, yRange);
            gameObjects[currentIndex].transform.position = point;
        }
        gameObjects[currentIndex].SetActive(true);
        currentIndex = (currentIndex + 1) % gameObjects.Length;
    }
}
