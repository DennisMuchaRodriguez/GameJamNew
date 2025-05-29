using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    [Header("Object Generation Settings")]
    [SerializeField] private GameObject[] objectPrefabs; // Array de prefabs a generar
    [SerializeField] private Transform spawnArea; // Área donde se generarán los objetos

    [Header("Spawn Position Range")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float spawnHeight = 0.2f; 

    [Header("Spawn Timing")]
    [SerializeField] private float initialSpawnRate = 1f;
    [SerializeField] private float minSpawnRate = 0.3f;
    [SerializeField] private float maxSpawnRate = 1.5f;
    [SerializeField] private float spawnRateDecreaseOverTime = 0.01f; // Aumenta frecuencia con el tiempo
    [SerializeField] private float timeBetweenWaves = 5f; // Tiempo entre oleadas
    [SerializeField] private int objectsPerWave = 3; // Objetos por oleada

    private float timer;
    private float nextSpawnTime;
    private float currentSpawnRate;
    private float waveTimer;

    private void Start()
    {
        currentSpawnRate = initialSpawnRate;
        nextSpawnTime = Random.Range(minSpawnRate, maxSpawnRate);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        waveTimer += Time.deltaTime;

        
        if (currentSpawnRate > minSpawnRate)
        {
            currentSpawnRate -= spawnRateDecreaseOverTime * Time.deltaTime;
        }

      
        if (waveTimer >= timeBetweenWaves)
        {
            SpawnWave();
            waveTimer = 0f;
        }

       
        if (timer >= nextSpawnTime && objectPrefabs != null && objectPrefabs.Length > 0)
        {
            SpawnSingleObject();
            timer = 0f;
            nextSpawnTime = Random.Range(minSpawnRate, maxSpawnRate) * currentSpawnRate;
        }
    }

    private void SpawnSingleObject()
    {
        GameObject selectedPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];
        Vector3 spawnPosition = new Vector3(
            Random.Range(minX, maxX),
            spawnHeight,
            spawnArea.position.z
        );

        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity, spawnArea);
    }

    private void SpawnWave()
    {
        for (int i = 0; i < objectsPerWave; i++)
        {
            // Pequeño retraso entre objetos de la misma oleada
            float delay = Random.Range(0f, 0.5f);
            Invoke("SpawnSingleObject", delay);
        }
    }

    // Método para aumentar dificultad
    public void IncreaseSpawnRate(float amount)
    {
        objectsPerWave += Mathf.RoundToInt(amount);
        currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - (amount * 0.1f));
    }
}