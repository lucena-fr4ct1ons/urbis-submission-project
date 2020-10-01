﻿using System;
using UnityEngine;

public class BusStop: BaseBuilding, ICostlyBuilding
{
    [SerializeField] private CollisionAuxiliary auxCollider;
    
    [SerializeField] private GameObject radiusObject;
    [SerializeField] private int connectedHouses = 0;
    
    [SerializeField] private float operationCostPerSecond = 1.0f;

    public float operationCost
    {
        get => operationCostPerSecond;
        set => operationCostPerSecond = value;
    }

    public int ConnectedHouses
    {
        get => connectedHouses;
        set => connectedHouses = value;
    }

    protected override void Awake()
    {
        base.Awake();
        auxCollider.TriggerEnter += OnAuxEnter;
        auxCollider.TriggerExit += OnAuxExit;
    }

    protected override void Start()
    {
        base.Start();
        CityManager.Instance.TrackCostlyBuildings(this);
        
    }

    private void OnAuxEnter(Collider other)
    {
        if (other.TryGetComponent<House>(out House house))
        {
            house.AmountOfBusStops++;
            connectedHouses++;
        }
    }

    private void OnAuxExit(Collider other)
    {
        if (other.TryGetComponent<House>(out House house))
        {
            house.AmountOfBusStops--;
            connectedHouses--;
        }
    }
    
    protected override void OnSelection()
    {
        base.OnSelection();
        radiusObject.SetActive(true);
        CityGUIManager.Instance.ShowBusStopInfo(this);
    }

    public override void Deselect()
    {
        radiusObject.SetActive(false);
    }
    
    public BusStopSaveData ToSaveData()
    {
        BusStopSaveData data = new BusStopSaveData();
        
        data.position = transform.position;
        data.rotation = transform.eulerAngles;
        data.scale = transform.lossyScale;

        return data;
    }
}