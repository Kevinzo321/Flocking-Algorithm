﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBah
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {

        //if no neighbors, maintain current alignment  
        if (context.Count == 0)
            return agent.transform.up;

        // add all together and avg
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SqaureAvoidRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
                
        }

        if (nAvoid > 0)
            avoidanceMove /= nAvoid;
        
        return avoidanceMove;
    }


}