using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : MonoBehaviour {

    public NavMeshAgent navMeshAgent;
    public float timeBetweenHit = 0.5f;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 4;
    public float speedRun = 6;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1.0f;
    public int edgeInteractions = 4;

    public int enemyDamage;
    float timeSinceLastHit;

    public Transform[] waypoints;
    int _currentWaypointIndex;

    Vector3 playerLastPos = Vector3.zero;
    Vector3 _playerPos;

    float _waitTime;
    float _timeToRotate;
    bool _playerInRange;
    bool _playerNear;
    bool _isPatrol;
    bool _caughtPlayer;


    void Start() {

        _playerPos = Vector3.zero;
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
        navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);

    }

    void Update() {

        timeSinceLastHit += Time.deltaTime;

        EnviromentView();

        if (!_isPatrol) {

            Chasing();

        } else {

            Patroling();

        }

    }

    void EnviromentView() {

        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++) {

            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle) {

                float distToPlayer = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, dirToPlayer, distToPlayer, obstacleMask)) {

                    _playerInRange = true;
                    _isPatrol = false;

                } else {

                    _playerInRange = false;

                }

            }

            if (Vector3.Distance(transform.position, player.position) > viewRadius) {

                _playerInRange = false;

            }

            if (_playerInRange) {

                _playerPos = player.transform.position;

            }

        }

    }

    private void Chasing() {

        _playerNear = false;
        playerLastPos = Vector3.zero;

        if (!_caughtPlayer)
        {

            Move(speedRun);
            navMeshAgent.SetDestination(_playerPos);

        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {

            if (_waitTime <= 0 && !_caughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {

                _isPatrol = true;
                _playerNear = false;
                Move(speedWalk);
                _timeToRotate = timeToRotate;
                _waitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);

            }
            else
            {

                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {

                    Stop();
                    _waitTime -= Time.deltaTime;

                }

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

    void Move(float speed) {

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;

    }

    void Stop() {

        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;

    }

    void LookingPlayer(Vector3 player) {

        navMeshAgent.SetDestination(player);

        if (Vector3.Distance(transform.position, player) <= 0.3) {

            if (_waitTime <= 0) {

                _playerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);
                _waitTime = startWaitTime;
                _timeToRotate = timeToRotate;

            } else {

                Stop();
                _waitTime -= Time.deltaTime;

            }

        }

    }

    public void NextPoint() {

        _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);

    }
    
    private void OnCollisionStay(Collision collision) {

        if (collision.gameObject.tag == "Player") {

            if (CanHit()) {

                //HealthInterface healthInterface = collision.transform.GetComponent<HealthInterface>();
                //healthInterface?.Damage(5);

                GameManager.instance.PlayerHealth.Damage(enemyDamage);

                timeSinceLastHit = 0;
            }

        }

    }

    private bool CanHit() {

        if (timeSinceLastHit < timeBetweenHit) {

            return false;

        } else {

            return true;

        }

    }

}