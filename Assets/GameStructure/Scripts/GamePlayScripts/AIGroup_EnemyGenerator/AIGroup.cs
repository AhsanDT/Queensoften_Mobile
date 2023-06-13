using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
//using UnityEngine.AI;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIGroup : MonoBehaviour
{
    //public enum SpawnMode { AtNight, AtDaytime, AllDay }
    public UnityEvent onAiGroupTaskCompletetion; 
    [SerializeField]
    private Color m_GroupColor = new Color(.2f, .2f, .2f, .5f);

    [SerializeField]
    [Space()]
    private bool m_EnableSpawning = true;

    [SerializeField]
    private bool m_MakeAgentsChildren = true;

    [SerializeField]
    private GameObject[] m_Prefabs;
    public int maxObjectsToInstantiate = 20;
    [SerializeField]
    [Space()]
    [Range(0f, 50f)]
    private float m_GroupRadius = 10f;

    [SerializeField]
    [Range(0, 30)]
    private int m_MaxCountAtATime = 3;

    //[SerializeField]
    //[Space()]
    //private SpawnMode m_SpawnMode;

    [SerializeField]
    [Range(1f, 120f)]
    private float m_SpawnInterval = 30f;

    private float m_LastUpdateTime;
    private List<Vector3> m_SpawnPoints = new List<Vector3>();
    private List<EnemyDeadAI> m_AliveAgents = new List<EnemyDeadAI>();
    private Transform m_Player;
    //[SerializeField]
    //WaypointGroup waypointGroup;

    [SerializeField]
    bool instantiateOnFacingPlayerFlag;
    public float initialWaitTimeToStart = 5f;
    float timeToStart;
    private void Start()
    {
        for (int sp = 0; sp < m_MaxCountAtATime; sp++)
        {
            var randomPos = transform.position + new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y) * m_GroupRadius;

            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(randomPos, out navMeshHit, 10f, NavMesh.AllAreas))
                randomPos = navMeshHit.position;

            m_SpawnPoints.Add(randomPos);

        }

        m_Player = PlayerDead.Instance.gameObject.transform;

    }

    private void Update()
    {
        timeToStart += Time.deltaTime;
        if (timeToStart < initialWaitTimeToStart)
            return;
        //Debug.Log("m_Player.forward  " + Vector3.Dot(m_Player.forward, transform.position - m_Player.position) + "And Distance is " + Vector3.Distance(transform.position, m_Player.position) * .6f);
        bool shouldSpawn;
        if (instantiateOnFacingPlayerFlag)
        {
            shouldSpawn =
              m_EnableSpawning &&
              Time.time > m_LastUpdateTime &&
              m_AliveAgents.Count < m_MaxCountAtATime &&
              maxObjectsToInstantiate > 0;
        }
        else
        {
            shouldSpawn =
              m_EnableSpawning &&
              Time.time > m_LastUpdateTime &&
              m_AliveAgents.Count < m_MaxCountAtATime &&
              (Vector3.Dot(m_Player.forward, transform.position - m_Player.position) < Vector3.Distance(transform.position, m_Player.position) * .6f) && maxObjectsToInstantiate > 0;
            //Vector3.Dot(m_Player.forward, transform.position - m_Player.position) < 0f && maxObjectsToInstantiate > 0;
        }
        if (shouldSpawn)
            TrySpawn();
    }

    private void TrySpawn()
    {

        bool canSpawn = true;
        if (!canSpawn)
            return;

        m_LastUpdateTime = Time.time + m_SpawnInterval;

        // Spawning logic.
        var randomPos = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Count)];
        var randomPrefab = m_Prefabs[Random.Range(0, m_Prefabs.Length)];

        var enemyInstantiated = Instantiate(randomPrefab, randomPos, Quaternion.Euler(Vector3.up * Random.Range(-360f, 360f)));
        //GameObjectsContainer.Instance.enemiesCarContainer.Add(enemyInstantiated);

        //Disabled  In this
        //if (waypointGroup != null)
        //{
        //    if (enemyInstantiated.GetComponent<AI>().waypointGroup == null)
        //    {
        //        enemyInstantiated.GetComponent<AI>().waypointGroup = waypointGroup;
        //        enemyInstantiated.GetComponent<AI>().SpawnNPC();
        //    }
        //}
        //else
        //{
        //    Debug.LogError("waypointGroup is Null");
        //}



        maxObjectsToInstantiate--;
        var agent = enemyInstantiated.GetComponent<EnemyDeadAI>();

        if (m_MakeAgentsChildren)
            enemyInstantiated.transform.SetParent(transform, true);

        if (agent != null)
        {
            m_AliveAgents.Add(agent);
            agent.OnDead.AddListener(() => On_AgentDeath(agent));

        }
    }

    private void On_AgentDeath(EnemyDeadAI agent)
    {  
        m_AliveAgents.Remove(agent); 

        if(maxObjectsToInstantiate == 0 && m_AliveAgents.Count == 0)
        {
           // Debug.LogError("Wave Finished");
            onAiGroupTaskCompletetion.Invoke();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Color prevCol = Gizmos.color;

        Gizmos.color = m_GroupColor;
        Gizmos.DrawSphere(transform.position, m_GroupRadius);

        Gizmos.color = prevCol;
    }
}
