﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : BaseBuilding
{
    [Tooltip("Tax income of this house per second.")]
    [SerializeField] private float taxIncome = 1.0f;

    [Tooltip("Happiness in the current house, from 0 to 2. The house's taxIncome value will be multiplied by the happiness")]
    [SerializeField] private float currentHappiness = 1.0f;

    [SerializeField] private bool hasPoliceDepartment = false;

    public bool HasPoliceDepartment
    {
        get => hasPoliceDepartment;
        set => hasPoliceDepartment = value;
    }
    
    public float CurrentHappiness
    {
        get => currentHappiness;
        set => currentHappiness = value;
    }
    
    public float TaxIncome => taxIncome;

    private void Start()
    {
        if (CityManager.Instance)
        {
            CityManager.Instance.TrackHouse(this);
        }
        else
        {
            Debug.LogError("CityManager instance not set!");
        }
    }
}
