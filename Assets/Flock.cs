using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    //Drive factor: arrows move faster
    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float MaxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidRadiusMultiplier = 0.5f;

    float sqaureMaxSpeed;
    float squareNeighborRadius;
    float sqaureAvoidanceRadius;
    public float SqaureAvoidRadius {get {return sqaureAvoidanceRadius; }}

    // Start is called before the first frame update
    void Start()
    {
        sqaureMaxSpeed = MaxSpeed * MaxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        sqaureAvoidanceRadius = squareNeighborRadius * avoidRadiusMultiplier * avoidRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)

        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform
                );

            newAgent.name = "Agent" + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);

        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            // cal agent and near by objects

            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;

            //check maxspeed
            if (move.sqrMagnitude > sqaureMaxSpeed)
            {
                move = move.normalized * MaxSpeed;
            }
            agent.Move(move);

        }

    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position,
            neighborRadius);

        // Check For each colllider not ourself
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;

    }



}
