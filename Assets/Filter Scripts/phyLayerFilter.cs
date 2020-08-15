using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/ Phy Layer")]
public class phyLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> orginal)
    {
        List<Transform> filtered = new List<Transform>();

        foreach (Transform item in orginal)
        {
            if(mask == (mask | (1<< item.gameObject.layer)))
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
