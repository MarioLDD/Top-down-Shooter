using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    private Transform player;
    public float range = 10f;
    public float shootRange = 10f;

    public LayerMask capasObstaculos;
    public float timeRefresh = 0.1f;
    private NavMeshAgent navMeshAgent;
    public float fireRate = 0.5f;

    private float nextFireTime;
    public Transform firePoint;
    public Rigidbody bullet;
    public float proyectileForce = 10;
    public int score = 175;
    private Rigidbody proyectileRb;
    public ScoreManager scoreManager;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distancePlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionPlayer = (player.position - transform.position).normalized;

        Ray ray = new Ray(transform.position, directionPlayer);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, capasObstaculos))
        {
            Debug.DrawRay(ray.origin, ray.direction * distancePlayer, Color.green);
            if (hit.collider.gameObject.CompareTag("Player"))
            {               
                StartCoroutine(Move());
            }
        }

        Ray rayAim = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitTarget;
        Debug.DrawRay(rayAim.origin, rayAim.direction * shootRange, Color.green);
        if (Physics.Raycast(rayAim, out hitTarget, shootRange, capasObstaculos))
        {
            if (hitTarget.collider.gameObject.CompareTag("Player"))
            {
                //Debug.Log("¡Disparar al jugador!");
                if (nextFireTime < Time.time)
                {
                    proyectileRb = Instantiate(bullet, firePoint.position, Quaternion.identity);
                    proyectileRb.AddRelativeForce(rayAim.direction * proyectileForce, ForceMode.Impulse);
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }
    IEnumerator Move()
    {
        WaitForSeconds wait = new WaitForSeconds(timeRefresh);
        while (enabled)
        {
            navMeshAgent.destination = player.position;
            yield return wait;
        }
    }
    public void Dead()
    {
        scoreManager.RefreshScore(score);
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, shootRange);
    }
}


