using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyRange : MonoBehaviour {

    [Header("Prefabs")]
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public GameObject grenadePrefab;
    [SerializeField] public GameObject bigProjectilePrefab;
    [SerializeField] public GameObject explosionPrefab;

    [Header("Enemy Chance Spawn")]
    [SerializeField] public int enemyChanceSpawn;

    [Header("Enemy Minions Boss")]
    [SerializeField] public GameObject minionPrefab;
    [SerializeField] public float timeBetweenSummon = 10f;
    private float timeSinceLastSummon = 0f;
    [SerializeField] public int enemySummonQuantity = 5;

    [Header("Enemy Range")]
    [SerializeField] public float shootDistance;
    [SerializeField] public float throwDistance;

    [Header("Enemy Melee")]
    [SerializeField] public float enemyDamage;
    [SerializeField] public EnemyHealth enemyHealth;

    [Header("Enemy Type")]
    [SerializeField] public int enemyType = 1;

    [Header("Enemy")]
    private Transform playerTransform;
    [SerializeField] public float timeBetweenShot = 1f;
    private float timeSinceLastShot = 0f;

    [SerializeField] public NavMeshAgent navMeshAgent;
    [SerializeField] public float startWaitTime = 4;
    [SerializeField] public float timeToRotate = 2;
    [SerializeField] public float speedWalk = 4;
    [SerializeField] public float speedRun = 6;

    [SerializeField] public float viewRadius = 15;
    [SerializeField] public float viewAngle = 90;
    [SerializeField] public LayerMask playerMask;
    [SerializeField] public LayerMask obstacleMask;
    [SerializeField] public float meshResolution = 1.0f;
    [SerializeField] public int edgeInteractions = 4;

    [SerializeField] public Transform[] waypoints;
    private int _currentWaypointIndex;

    Vector3 playerLastPos = Vector3.zero;
    Vector3 _playerpos;

    private float _waitTime;
    private float _timeToRotate;
    private bool _playerInRange;
    private bool _playerNear;
    private bool _isPatrol;
    private bool _caughtPlayer;

    private int xPos;
    private int zPos;

    Vector3 _playerPos;


    void Start () {

        playerTransform = GameManager.instance.Player.transform;

        _playerPos = playerTransform.position;
        _isPatrol = true;
        _caughtPlayer = false;
        _playerInRange = false;
        _playerNear = false;
        _waitTime = startWaitTime;
        _timeToRotate = timeToRotate;

        _currentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(_playerPos);

    }

    void Update() {

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        EnviromentView();

        if (distanceToPlayer <= shootDistance) {

            Shoot();

            if (enemyType == 3)
            {

                SpawnMinions();

            }

            Stop();

            transform.LookAt(playerTransform);

        } else if (!_isPatrol) {

            ChasePlayer();

            transform.LookAt(playerTransform);

        } else  {

            Patroling();

        }

        timeSinceLastShot += Time.deltaTime;
        timeSinceLastSummon += Time.deltaTime;

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
                    _isPatrol = false;

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

    private void Patroling() {

        if (_playerNear) {

            if (_timeToRotate <= 0) {

                Move(speedWalk);
                LookingPlayer(playerLastPos);

            } else {

                Stop();
                _timeToRotate -= Time.deltaTime;

            }

        } else {

            _playerNear = false;
            playerLastPos = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {

                if (_waitTime <= 0) {

                    NextPoint();
                    Move(speedWalk);
                    _waitTime = startWaitTime;

                } else {

                    Stop();
                    _waitTime -= Time.deltaTime;

                }

            }

        }

    }

    void ChasePlayer() {

        _playerNear = true;
        playerLastPos = Vector3.zero;
        
        if (!_caughtPlayer) {

            Move(speedRun);
            navMeshAgent.SetDestination(_playerPos);

        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {

            if (_waitTime <= 0 && !_caughtPlayer && Vector3.Distance(transform.position, playerTransform.position) >= 6f)
            {

                _isPatrol = true;
                _playerNear = false;
                Move(speedWalk);
                _timeToRotate = timeToRotate;
                _waitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);

            } else {

                if (Vector3.Distance(transform.position, playerTransform.position) >= 2.5f) {

                    Stop();
                    _waitTime -= Time.deltaTime;

                }

            }

        }

    }

    void LookingPlayer(Vector3 player)
    {

        navMeshAgent.SetDestination(player);

        if (Vector3.Distance(transform.position, player) <= 0.3)
        {

            if (_waitTime <= 0)
            {

                _playerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);
                _waitTime = startWaitTime;
                _timeToRotate = timeToRotate;

            }
            else
            {

                Stop();
                _waitTime -= Time.deltaTime;

            }

        }

    }

    void Move(float speed) {

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;

    }

    void Stop() {

        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;

    }

    public void NextPoint() {

        _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);

    }

    private void Shoot() {

        if (CanShot()) {

            if (enemyType == 1)
            {

                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform);
                Vector3 direction = (playerTransform.position - transform.position).normalized;

                direction.x *= 100f;
                direction.y += 0.2f;
                direction.z *= 100f;

                projectile.GetComponent<Rigidbody>().velocity = direction;

                timeSinceLastShot = 0;

            }
            else if (enemyType == 2)
            {

                Vector3 throwOffset = new Vector3(0f, 1.2f, 0f);
                Vector3 playerDirection = (playerTransform.position - transform.position).normalized;

                playerDirection.y += 0.5f;

                GameObject grenade = Instantiate(grenadePrefab, transform.position + throwOffset, Quaternion.LookRotation(playerDirection), transform);
                Rigidbody rigidbody = grenade.GetComponent<Rigidbody>();
                rigidbody.AddForce(playerDirection * throwDistance, ForceMode.VelocityChange);

                timeSinceLastShot = 0;

            }
            else if (enemyType == 3)
            {

                GameObject projectile = Instantiate(bigProjectilePrefab, transform.position, Quaternion.identity, transform);
                Vector3 direction = (playerTransform.position - transform.position).normalized;

                direction.x *= 10f;
                direction.y -= 0.1f;
                direction.z *= 10f;

                projectile.GetComponent<Rigidbody>().velocity = direction;

                timeSinceLastShot = 0;

            }
            
        }

    }

    private bool CanShot() {

        if (timeSinceLastShot < timeBetweenShot) {

            return false;

        } else {

            return true;

        }

    }

    private void SpawnMinions() {

        if(CanSummon()) {

            for (int i = 0; i < enemySummonQuantity; i++) {

                xPos = Random.Range(-5, 6);
                zPos = Random.Range(-5, 6);

                Vector3 throwOffset = new Vector3(xPos, 0f, zPos);

                Instantiate(minionPrefab, transform.position + throwOffset, Quaternion.identity, transform);

            }

            timeSinceLastSummon = 0;

        }

    }

    private bool CanSummon() {

        if (timeSinceLastSummon < timeBetweenSummon) {

            return false;

        } else {

            return true;

        }

    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            if (enemyType == 4)
            {

                if (CanShot())
                {

                    GameManager.instance.PlayerHealth.Damage(enemyDamage);

                    timeSinceLastShot = 0;
                }

            }
            else if (enemyType == 5)
            {

                if (CanShot())
                {

                    GameManager.instance.PlayerHealth.Damage(enemyDamage);

                    enemyHealth.Heal(enemyDamage);

                    timeSinceLastShot = 0;

                }

            }
            else if (enemyType == 6)
            {

                Explode();

            }

        }

    }

    private void Explode()
    {

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);

    }

}