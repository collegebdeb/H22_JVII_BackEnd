using System;
using System.Collections;
using System.Collections.Generic;
using DG.DemiLib;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine.Serialization;

public class CanonTrail : MonoBehaviour
{
    public CanonStats stats;
    public List<Projectile> projectilePrefabs;
    public MatrixManager matrixManager;
    public ParticleSystem canonShoot;
    public bool canonFlower;
    public void changeModel()
    {
        canonFlower = true;
        foreach (Projectile projectile in projectilePrefabs)
        {
            projectile.SwitchToFlowerModel();
        }
    }
    private float ShootVanishPoint
    {
        get => vanishDistance;
        set
        {
            vanishDistance = value;
            CalculateTargetPos();
        }
    }

    [SerializeField] private float vanishDistance;
    [SerializeField, ReadOnly] private float shootDistanceLoop;
    [SerializeField, ReadOnly] private float vanishLoopRange;
    
    public GameObject projectilePrefab;

    private Vector3 targetPos;

    private void OnValidate()
    {
        ShootVanishPoint = vanishDistance;
    }

    private void Awake()
    {
        CalculateTargetPos();
        matrixManager = GameObject.FindObjectOfType<MatrixManager>();
    }

    private void Start()
    {
       
    }

    IEnumerator CoInstantiate()
    {
        yield return new WaitForSeconds(1f);

        InstantiateBall();
    }

    private void OnEnable()
    {
        MatrixManager.OnTransitionActivated += ActivateAllBalls;
        Projectile.OnCollisionWithPlayer += ActivateAllBalls;
        CollideBALL.OnCollideBall += PlayShoot;
        
        StartCoroutine(CoInstantiate());
    }

    private void OnDisable()
    {
        MatrixManager.OnTransitionActivated -= ActivateAllBalls;
        Projectile.OnCollisionWithPlayer -= ActivateAllBalls; 
        CollideBALL.OnCollideBall -= PlayShoot;
        
    }

    [Button]
    private void ActivateAllBalls()
    {
        foreach (var instance in projectilePrefabs)
        {
            instance.SetAlive();
        }
    }

    private void CalculateTargetPos()
    {
        Vector3 position = transform.position;
        targetPos = transform.position + transform.forward * vanishDistance;
    }

    private void AutomaticAdjustShootDistance()
    {
        vanishDistance -= vanishDistance % stats.canonBallInterval;
        shootDistanceLoop = vanishDistance + stats.canonBallInterval * stats.vanishingPointMultiplier;
        vanishLoopRange = shootDistanceLoop - vanishDistance;
    }
    
    [Button]
    private void InstantiateBall()
    {
        AutomaticAdjustShootDistance();
        int ballNumber = (int) (shootDistanceLoop / stats.canonBallInterval);

        for (int i = 0; i < ballNumber; i++)
        {
            var position = transform.position;
            Projectile instance = Instantiate(projectilePrefab, position + transform.forward * stats.canonBallInterval * i, Quaternion.identity).GetComponent<Projectile>();
            instance.transform.SetParent(transform);
            projectilePrefabs.Add(instance);
            instance.particle.transform.rotation = transform.rotation;
        }
    }

    private void Update()
    {
        UpdateBallPos();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.33f, 0.22f);
        Gizmos.DrawLine(transform.position, targetPos);

        if (transform.hasChanged)
        {
            CalculateTargetPos();
        }
    }

    public float alphaValue;
    public Animator anim;
    private void UpdateBallPos()
    {
        if (MatrixManager.isMatrixPlaying)
        {
            foreach (var instance in projectilePrefabs)
            {
                if (instance.transform.localPosition.z > shootDistanceLoop)
                {
                    anim.Play("CanonShot");
                    canonShoot.Play();
                }
                
            
            }
            return;
        }
        
        foreach (var instance in projectilePrefabs)
        {
            var position = instance.transform.position;
            instance.transform.position = position + transform.forward * Time.unscaledDeltaTime * stats.canonBallSpeed;
            
            
            if (instance.transform.localPosition.z > shootDistanceLoop)
            {
                instance.particleFire.gameObject.SetActive(false);
                instance.transform.position = transform.position;
                instance.SetAlive();
              
            }
            
            if (instance.transform.localPosition.z > ShootVanishPoint)
            {
                alphaValue = Mathf.Lerp(1, 0, (instance.transform.localPosition.z - vanishDistance) / vanishLoopRange);
                //instance.GetComponent<Renderer>().material.color= new Color(1f, 1f, 1f, alphaValue);
            }
            else
            {
                //instance.GetComponent<Renderer>().material.color = Color.white;
            }

            
        }
    }

    private void PlayShoot()
    {
        if (!enabled) return;
        if (!anim.enabled) return;
        anim.Play("CanonShot");
        canonShoot.Play();
    }
    
    

}
