using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohensionBehavior : FilteredFlockBah
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {

        //if no neighbors, return no adjustment


        if (context.Count == 0)
            return Vector2.zero;

        // add all together and avg
        Vector2 conhesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            conhesionMove += (Vector2)item.position;
        }
        conhesionMove /= context.Count;

        //create offset from agent position
        conhesionMove -= (Vector2)agent.transform.position;
        return conhesionMove;
    }


}
