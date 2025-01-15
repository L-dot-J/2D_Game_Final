using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lana;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private SpriteRenderer _portalRenderer;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    private void Awake()
    {
        _portalRenderer.color = _inactiveColor;
    }
    public Transform GetSpawnPoint()
    {
        return _spawnPoint;
    }
    public void SetCheckPointState(bool IsActive)
    {
        var color = IsActive ? _activeColor : _inactiveColor;
        _portalRenderer.color = color; 
    }
}
