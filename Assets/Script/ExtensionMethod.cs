using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class ExtensionMethod{
    public static float GetPathRemainingDistance(this NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.path.corners.Length == 0)
        {
            Debug.LogError("Path.corner equal 0");
            return -1f;
        }
        if (navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            Debug.LogError("Path.statut = invalid");
            return -1f;
        }
        if (navMeshAgent.pathPending)
        {
            Debug.LogError("Path.pending");
            return -1f;
        }
        float distance = 0.0f;
        for (int i = 0; i < navMeshAgent.path.corners.Length - 1; i++)
        {
            distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
        }
        return distance;
    }
}
