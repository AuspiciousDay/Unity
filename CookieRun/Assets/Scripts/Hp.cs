using UnityEngine;

public class Hp : MonoBehaviour
{
    public GameManager gameManager;
    public float length = 1f;

    private void Update()
    {
        if(length < 0)
        {
            gameManager.OnPlayerDead();
        }
        length -= Time.deltaTime;
        gameObject.transform.localScale = new Vector3(length,1f,1f);
    }

}
