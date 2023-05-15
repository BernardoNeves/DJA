using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRange : MonoBehaviour {

    public GameObject projectilePrefab;
    public float shootDistance;

    private Transform playerTransform;
    private float timeBetweenShot = 1f;
    private float timeSinceLastShot = 0f;

    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speed = 6;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1.0f;
    public int edgeInteractions = 4;

    private bool _playerInRange;

    Vector3 _playerPos;


    void Start () {

        playerTransform = GameManager.instance.Player.transform;

        _playerPos = playerTransform.position;

        _playerInRange = false;

        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(_playerPos);

    }

    void Update() {

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        EnviromentView();

        if (distanceToPlayer <= shootDistance) {

            Shoot();
            Stop();

        } else {

            ChasePlayer();

        }

        timeSinceLastShot += Time.deltaTime;

    }

    void EnviromentView()
    {

        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {

            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle)
            {

                float distToPlayer = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, dirToPlayer, distToPlayer, obstacleMask))
                {

                    _playerInRange = true;

                }
                else
                {

                    _playerInRange = false;

                }

            }

            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {

                _playerInRange = false;

            }

            if (_playerInRange)
            {

                _playerPos = playerTransform.position;

            }

        }

    }

    void ChasePlayer() {

        Move();
        navMeshAgent.SetDestination(_playerPos);

    }

    void Move() {

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;

    }

    void Stop() {

        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;

    }

    private void Shoot() {

        if (CanShot()) {

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector3 direction = (playerTransform.position - transform.position).normalized;

            direction.x *= 100f;
            direction.y += 0.2f;
            direction.z *= 100f;

            projectile.GetComponent<Rigidbody>().velocity = direction;

            timeSinceLastShot = 0;

        }

    }

    private bool CanShot() {

        if (timeSinceLastShot < timeBetweenShot) {

            return false;

        } else {

            return true;

        }

    }

}