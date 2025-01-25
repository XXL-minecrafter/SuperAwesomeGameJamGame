using UnityEngine;
using System.Collections.Generic;

public class DistributionManager : MonoBehaviour
{
    public static DistributionManager Instance { get; private set; }

    [SerializeField] private List<SpawnPoint> coinSpawnPoints;
    [SerializeField] private List<SpawnPoint> vendingMachineSpawnPoints;
    [SerializeField] private List<SpawnPoint> gumPlaceSpawnPoints;

    private GameObject coinPrefab;
    private GameObject vendingMachinePrefab;
    private GameObject gumPlacePrefab;

    [SerializeField] private Transform coinContainer;
    [SerializeField] private Transform vendingMachineContainer;
    [SerializeField] private Transform gumContainer;


    [SerializeField] private int minimumGumSpawnPoints = 10;
    [SerializeField] private int minimumCoins = 3;
    [SerializeField] private int minimumVendingMachines = 1;

    private int existingGumSpawnPoints;
    private int existingCoins;
    private int existingVendingMachines;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else { Destroy(Instance); return; }

        FindObjectsOfType<SpawnPoint>();

        coinPrefab = Resources.Load<GameObject>("Coin");
        gumPlacePrefab = Resources.Load<GameObject>("GumPlace");
        vendingMachinePrefab = Resources.Load<GameObject>("VendingMachine");
    }

    private void OnEnable()
    {
        SpawnPoint.OnObjectCleared += RemoveSpawnPointObject;
    }

    private void OnDisable()
    {
        Resources.UnloadAsset(coinPrefab);
        Resources.UnloadAsset(vendingMachinePrefab);
        Resources.UnloadAsset(gumPlacePrefab);

        SpawnPoint.OnObjectCleared -= RemoveSpawnPointObject;
    }

    private void Start() => CollectSpawnPoints();

    private void Update()
    {
        if(existingGumSpawnPoints < minimumGumSpawnPoints)
        {
            DistributeGumPlace();
        }

        if(existingCoins < minimumCoins)
        {
            DistributeCoin();
        }

        if(existingVendingMachines < minimumVendingMachines)
        {
            DistributeVendingMachine();
        }
    }

    private void CollectSpawnPoints()
    {
        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();

        for (int i = 0; i < spawnPoints.Length; i++) 
        {
            switch(spawnPoints[i].SpawnPointObject)
            {
                case SpawnPointObject.GumPlacePosition:
                    gumPlaceSpawnPoints.Add(spawnPoints[i]);
                    break;
                case SpawnPointObject.Coin:
                    coinSpawnPoints.Add(spawnPoints[i]);
                    break;
                case SpawnPointObject.VendingMachine:
                    vendingMachineSpawnPoints.Add(spawnPoints[i]);
                    break;
            }
        }
    }

    #region Gum Place Spawning
    private void DistributeGumPlace()
    {
        bool foundValidSpawnPoint = false;
        int randomSpawnPointIndex;

        while (!foundValidSpawnPoint) 
        {
            randomSpawnPointIndex = Random.Range(0, gumPlaceSpawnPoints.Count);

            SpawnPoint spawnPoint = gumPlaceSpawnPoints[randomSpawnPointIndex];

            if (spawnPoint.HasObject) continue;

            SpawnGumPlace(spawnPoint); foundValidSpawnPoint = true;
        }
    }

    private void SpawnGumPlace(SpawnPoint at)
    {
        Instantiate(gumPlacePrefab, at.transform.position, Quaternion.identity, gumContainer);

        at.HasObject = true;

        existingGumSpawnPoints++;
    }
    #endregion

    #region Coin Spawning
    private void DistributeCoin()
    {
        bool foundValidSpawnPoint = false;
        int randomSpawnPointIndex;

        while (!foundValidSpawnPoint)
        {
            randomSpawnPointIndex = Random.Range(0, coinSpawnPoints.Count);

            SpawnPoint spawnPoint = coinSpawnPoints[randomSpawnPointIndex];

            if (spawnPoint.HasObject) continue;

            SpawnCoin(spawnPoint);foundValidSpawnPoint = true;
        }
    }

    private void SpawnCoin(SpawnPoint at)
    {
        GameObject coinObject = Instantiate(coinPrefab, at.transform.position, Quaternion.identity, coinContainer);

        if(coinObject.TryGetComponent(out CoinBehaviour coinBehaviour))
        {
            coinBehaviour.SpawnPoint = at;

            at.HasObject = true;

            existingCoins++;
        }
    }
    #endregion

    #region Vending Machine Spawning
    private void DistributeVendingMachine() 
    {
        bool foundValidSpawnPoint = false;
        int randomSpawnPointIndex;

        while (!foundValidSpawnPoint)
        {
            randomSpawnPointIndex = Random.Range(0, vendingMachineSpawnPoints.Count);

            SpawnPoint spawnPoint = vendingMachineSpawnPoints[randomSpawnPointIndex];

            if (spawnPoint.HasObject) continue;

            SpawnVendingMachine(spawnPoint); foundValidSpawnPoint = true;
        }
    }

    private void SpawnVendingMachine(SpawnPoint at)
    {
        GameObject vendingMachineObject = Instantiate(vendingMachinePrefab, at.transform.position, Quaternion.identity, vendingMachineContainer);

        if(vendingMachineObject.TryGetComponent(out VendingMachineBehaviour vendingMachineBehaviour))
        {
            vendingMachineBehaviour.SpawnPoint = at;

            at.HasObject = true;

            existingVendingMachines++;
        }
    }
    #endregion

    public void RemoveSpawnPointObject(SpawnPoint spawnPoint)
    {
        spawnPoint.HasObject = false;

        switch (spawnPoint.SpawnPointObject)
        {
            case SpawnPointObject.GumPlacePosition:
                gumPlaceSpawnPoints.Remove(spawnPoint);
                existingGumSpawnPoints--;
                break;
            case SpawnPointObject.Coin:
                coinSpawnPoints.Remove(spawnPoint);
                existingCoins--;
                break;
            case SpawnPointObject.VendingMachine:
                vendingMachineSpawnPoints.Remove(spawnPoint);
                existingVendingMachines--;
                break;
        }
    }
}
