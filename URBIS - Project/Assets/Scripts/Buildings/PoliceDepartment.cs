﻿using System;
using UnityEngine;

public class PoliceDepartment: BaseBuilding
{
    [SerializeField] private float happinessIncrease = 0.1f;
    [SerializeField] private float secondaryHappinessIncrease = 0.05f;
    
    [SerializeField] private CollisionAuxiliary triggerCollider;
    
    [SerializeField] private GameObject radiusObject;
    [SerializeField] private int connectedHouses = 0;
    
    public int ConnectedHouses
    {
        get => connectedHouses;
        set => connectedHouses = value;
    }

    protected override void Awake()
    {
        base.Awake();
        triggerCollider.TriggerEnter += OnTriggerEnter;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<House>(out House house))
        {
            if (!house.HasPoliceDepartment)
            {
                house.CurrentHappiness += happinessIncrease;
                house.HasPoliceDepartment = true;
            }
            else
            {
                house.CurrentHappiness += secondaryHappinessIncrease;
            }

            connectedHouses++;
        }
    }
    
    protected override void OnSelection()
    {
        base.OnSelection();
        radiusObject.SetActive(true);
        CityGUIManager.Instance.ShowPoliceDepartmentInfo(this);
    }

    public override void Deselect()
    {
        radiusObject.SetActive(false);
    }
}
