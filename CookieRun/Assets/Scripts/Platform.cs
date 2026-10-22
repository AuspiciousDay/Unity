using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles;
    public float obstaclesRatio = 0.3f;
    private bool stepped = false;
    public GameManager gameManager;
    public int score = 10;
    public float yRange = 20f;
    public Transform spawnPoint;
    private void Start()
    {
        GameObject findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();
    }
    private void OnEnable()
    {
        stepped = false;
        foreach(var obstacle in obstacles)
        {
            obstacle.SetActive(Random.Range(0f, 1f) < obstaclesRatio);
            Vector3 point = spawnPoint.localScale;
            point.y += Random.Range(1, yRange);
            obstacle.transform.localScale = point;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player") && !stepped)
        {
            stepped = true;
            gameManager.AddScore(score);
        }
    }
}
