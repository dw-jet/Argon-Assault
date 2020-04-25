using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathFx;
    [SerializeField] private Transform parent;
    [SerializeField] int scorePerHit = 10;
    
    private ScoreBoard scoreBoard;
    
    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void AddNonTriggerBoxCollider()
    {
        var bc = gameObject.AddComponent<BoxCollider>();
        bc.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(scorePerHit);
        var fx = Instantiate(deathFx, gameObject.transform.position, Quaternion.identity);
        fx.transform.parent = parent; 
        Destroy(gameObject);
    }
}
