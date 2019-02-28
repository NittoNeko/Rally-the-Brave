using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trigger when vitality changes
public delegate void VitalityChange(Transform target, float previous, float current, float max);

// Trigger when primary resource changes
public delegate void PriResChange(Transform target, float previous, float current, float max);

// Trigger when attribute changes
public delegate void AttrChange(Transform target, EAttrType type, float previous, float current);

// Trigger when attribute modifier changes
public delegate void AttrModChange(EAttrType type);

// Trigger when a certain combat event happens
public delegate void CombatEvent(Transform target);
