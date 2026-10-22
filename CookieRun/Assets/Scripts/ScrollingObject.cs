using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private GameManager gameManager;
    public float speed = 5f;
    private void Start()
    {
        GameObject findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();
    }
    private void Update()
    {
        if (gameManager.IsGameOver)
        {
            return;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
}
