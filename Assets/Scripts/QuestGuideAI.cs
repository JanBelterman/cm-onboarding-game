using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class QuestGuideAI : MonoBehaviour
{
    public Transform[] navTargets;
    [Range(0, 10)] public int nearTargetCount = 3;
    public float targetDeadzoneDistance = 0.7f;
    public float targetWaitTime = 4f;

    private Coroutine currentRoutine = null;
    private NavMeshAgent _agent;
    private Animator _animator;
    private QuestManager _questManager;
    private SkinnedMeshRenderer _meshRenderer;
    private FloorVisibility _currentFloor;
    private List<Transform> selectedTargets = new List<Transform>();
    
    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _questManager = FindObjectOfType<QuestManager>();
    }
    
    private void Start() {
        currentRoutine = StartCoroutine(QuestSeekCycle());
        _questManager.OnQuestAccept += UpdateTargets;
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

    private IEnumerator QuestSeekCycle() {
        int currentTargetIndex = 0;

        while (selectedTargets.Count <= 0) yield return null;
        
        while (true) {

            var currentTarget = selectedTargets[currentTargetIndex].position;
            _agent.SetDestination(currentTarget);
            
            while (Vector3.Distance(transform.position, currentTarget) > targetDeadzoneDistance) {
                yield return null;
            }
            
            yield return new WaitForSeconds(targetWaitTime);

            currentTargetIndex++;
            if (currentTargetIndex >= selectedTargets.Count) currentTargetIndex = 0;
        }
    }

    private void UpdateTargets(GameObject questTarget) { 
        selectedTargets.Clear();

        for (int i = 0; i < nearTargetCount; i++) {
            float shortestDistance = Mathf.Infinity;
            Transform currentNearest = null;
            
            foreach (var target in navTargets) {
                var sqrDistance = (target.position - questTarget.transform.position).sqrMagnitude;

                if (sqrDistance < shortestDistance && !selectedTargets.Contains(target)) {
                    currentNearest = target;
                    shortestDistance = sqrDistance;
                }
            }
            
            selectedTargets.Add(currentNearest);
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
        _questManager.OnQuestAccept -= UpdateTargets;
    }
}
