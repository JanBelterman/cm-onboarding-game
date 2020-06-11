using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamingNPC : MonoBehaviour {

    public Transform[] navigationTargets;
    public float targetDeadzoneDistance = 0.7f;
    public float targetWaitTime = 4f;

    private Coroutine currentRoutine = null;
    private NavMeshAgent _agent;
    private Animator _animator;
    private SkinnedMeshRenderer _meshRenderer;
    private FloorVisibility _currentFloor;

    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Start() {
        currentRoutine = StartCoroutine(WalkCycle());
    }

    private void Update() {
        _animator.SetFloat("speed", _agent.velocity.magnitude);

        if (_currentFloor != null) {
            if (_currentFloor.playerOnFloor) {
                _meshRenderer.enabled = true;
            } else {
                _meshRenderer.enabled = false;
            }
        }
    }

    private IEnumerator WalkCycle() {
        if (navigationTargets.Length <= 0) yield break;
        
        var currentTargetIndex = 0;

        while (true) {
            var currentTarget = navigationTargets[currentTargetIndex].position;
            _agent.SetDestination(currentTarget);

            while (Vector3.Distance(transform.position, currentTarget) > targetDeadzoneDistance) {
                yield return null;
            }
            
            yield return new WaitForSeconds(targetWaitTime);
            
            currentTargetIndex++;
            if (currentTargetIndex >= navigationTargets.Length) currentTargetIndex = 0;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Floor")) {
            _currentFloor = other.GetComponent<FloorVisibility>();
        }
    }

    private void OnDisable() {
        if (currentRoutine != null) {
            StopCoroutine(currentRoutine);
        }
    }
}
