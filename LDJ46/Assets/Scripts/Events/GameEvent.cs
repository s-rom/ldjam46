﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]


public class GameUnityEvent : UnityEvent<GameEvent>
{ 
}

public class GameEvent
{

    public GameUnityEvent gameUnityEvent = new GameUnityEvent();
    
    public bool Completed { get; set; }


    [SerializeField]
    public Transform m_target;

    [SerializeField]
    public int m_type;
    [SerializeField]
    private float m_startTime;
    [SerializeField]
    private float m_duration;

    [SerializeField]
    private bool m_warningSoundPlayed = false;
    private float m_warningSoundThreshold;

    public GameEvent(int type, float duration, Transform target, float warningThreshold)
    { 
        m_startTime = Time.time;
        Completed = false;
        m_type = type;
        m_duration = duration;
        m_target = target;
        m_warningSoundThreshold = warningThreshold;
    }

    // Returns a value between 0 and 1 representing the time left.
    // 0 means the time has expired. 1 means the event just started.
    public float checkExpiration()
    {
        float elapsedTime = Time.time - m_startTime;
        float remaining = Mathf.Clamp01(1 - elapsedTime / m_duration);

        if (!m_warningSoundPlayed && remaining <= m_warningSoundThreshold) {
            try {
                m_target.GetComponent<AudioSource>().Play();
                m_warningSoundPlayed = true;
            }
            catch {
                Debug.LogWarning("Error: No audio source attached to vampire" + m_target);
            }
        }

        return remaining;
    }
}
