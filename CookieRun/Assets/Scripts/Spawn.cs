using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject platformPrefab;
    public int initCount = 5;
    public Transform spawnPoint;
    public float yRange = 2f;
    private GameObject[] platforms;
    private int currentIndex = 0;
    public float interval = 1f;
    public float intervalRange = 0.25f;
    private float currentInterval;
    private float spawnTimer = 0f;
    public GameManager gameManager;
    private void Awake()
    {
        platforms = new GameObject[initCount];
        for(int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = Instantiate(platformPrefab);
            platforms[i].SetActive(false);
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
            Spawn();
        }
    }
    private void Spawn()
    {
        platforms[currentIndex].SetActive(false);
        Vector3 point = spawnPoint.position;
        point.y += Random.Range(-yRange, yRange);
        platforms[currentIndex].transform.position = point;
        platforms[currentIndex].SetActive(true);
        currentIndex = (currentIndex + 1) % platforms.Length;
    }
}
