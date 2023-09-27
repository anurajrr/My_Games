using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    [Header("Coin")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinSpeed;
    [SerializeField] float coinSpawnRate = 15f; // Adjust the spawn rate for coins
    [SerializeField] float coinSpawnTime= 15f; // Adjust the spawn rate for coins

    [Header("Bomb")]
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float bombSpeed;
    [SerializeField] float bombSpawnTime = 5f; // Adjust the spawn rate for bomb
    [SerializeField] float bombSpawnRate = 5f; // Adjust the spawn rate for bomb

    [Header("Rocket Bomb")]
    [SerializeField] private GameObject rocketbombprefab;
    [SerializeField] private float rocketbombSpeed;
    [SerializeField] float rocketbombSpawnTime = 5f; // Adjust the spawn rate for rocketbomb
    [SerializeField] float rocketbombSpawnRate = 5f; // Adjust the spawn rate for rocketbomb

    

    float minHieght = -4.5f;
    float maxHieght = 4.5f;

    private ObjectPool coinPooler;
    private ObjectPool bombPooler;
    private ObjectPool rocketBombPooler;

    private void Start()
    {
        // Find the GameObjects with the ObjectPool components and get the corresponding ObjectPool components.
        GameObject coinPoolHolder = GameObject.Find("CoinPoolHolder");
        GameObject bombPoolHolder = GameObject.Find("BombPoolHolder");
        GameObject rocketBombPoolHolder = GameObject.Find("RocketBombPoolHolder");

        if (coinPoolHolder != null && bombPoolHolder != null && rocketBombPoolHolder != null)
        {
            coinPooler = coinPoolHolder.GetComponent<ObjectPool>();
            bombPooler = bombPoolHolder.GetComponent<ObjectPool>();
            rocketBombPooler = rocketBombPoolHolder.GetComponent<ObjectPool>();
        }
        else
        {
            Debug.LogError("One or more ObjectPoolHolders not found. Make sure they exist in the scene.");
        }

        // Start spawning
        InvokeRepeating(nameof(SpawnCoin), coinSpawnTime, coinSpawnRate);
        InvokeRepeating(nameof(SpawnBomb), bombSpawnTime, bombSpawnRate);
        InvokeRepeating(nameof(SpawnRocketBomb), rocketbombSpawnTime, rocketbombSpawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(SpawnCoin));
        CancelInvoke(nameof(SpawnBomb));
        CancelInvoke(nameof(SpawnRocketBomb));
    }

    private void SpawnCoin()
    {
        GameObject coin = coinPooler.GetObjectFromPool(coinPrefab); // Pass the coinPrefab as an argument

        if (coin != null)
        {
            coin.transform.position = transform.position + Vector3.up * Random.Range(minHieght, maxHieght);
            MovingObejects coinMovement = coin.GetComponent<MovingObejects>();
            coinMovement.speed = coinSpeed;
        }
    }

    private void SpawnBomb()
    {
        GameObject bomb = bombPooler.GetObjectFromPool(bombPrefab); // Pass the bombPrefab as an argument

        if (bomb != null)
        {
            bomb.transform.position = transform.position + Vector3.up * Random.Range(minHieght, maxHieght);
            MovingObejects bombMovement = bomb.GetComponent<MovingObejects>();
            bombMovement.speed = bombSpeed;
        }
    }

    private void SpawnRocketBomb()
    {
        GameObject rocketbomb = rocketBombPooler.GetObjectFromPool(rocketbombprefab); // Pass the rocketbombprefab as an argument

        if (rocketbomb != null)
        {
            rocketbomb.transform.position = transform.position + Vector3.up * Random.Range(-2, 2);
            MovingObejects rocketbombMovement = rocketbomb.GetComponent<MovingObejects>();
            rocketbombMovement.speed = rocketbombSpeed;
            rocketbomb.transform.DOMoveY(rocketbomb.transform.position.y + 4f, 1f) // Move up
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo); // Infinite loop (up and down)
        }
    }
}
