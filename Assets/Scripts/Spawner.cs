
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> mapSpawnTransforms;
    public List<Transform> desertBallSpawnTransfroms;
    public Camel camelPrefab;
    public float camelSpawnRate;
    public bool startSpawn;

    public DesertBall desertBallPrefab;
    private IEnumerator Start()
    {
        GameEvents.OnStartInfoEndEvent.AddListener(StartInfoEndEvent);


        yield return new WaitUntil(delegate { return startSpawn; });
        StartCoroutine(SpawnCamel());
        InvokeRepeating(nameof(SpawnDesertBall), 20, Random.Range(3, 7));
    }

    private void Awake()
    {
        
    }

    private void OnDestroy()
    {
        GameEvents.OnStartInfoEndEvent.RemoveListener(StartInfoEndEvent);
    }
    private IEnumerator SpawnCamel()
    {
        var camel = Instantiate(camelPrefab, GetRandomMapPoint(),Quaternion.identity);
        yield return new WaitForSeconds(camelSpawnRate);
        StartCoroutine(SpawnCamel());
    }
    public Vector3 GetRandomMapPoint()
    {
        int r = Random.Range(0, mapSpawnTransforms.Count);
        return mapSpawnTransforms[r].position;
    }
    private void StartInfoEndEvent()
    {
        startSpawn = true;
    }



    private void SpawnDesertBall()
    {
        int r = Random.Range(0, desertBallSpawnTransfroms.Count);
        Transform spawnTransform = desertBallSpawnTransfroms[r];

        var desertBall = Instantiate(desertBallPrefab, spawnTransform.position, Quaternion.identity);
        desertBall.transform.right = spawnTransform.right;
        desertBall.Initialize();
    }
}
