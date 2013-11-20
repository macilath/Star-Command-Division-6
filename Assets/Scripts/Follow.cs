using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public Transform m_Player;
    private NavMeshAgent m_NavMeshAgent;
    HostageSight vision;

	// Use this for initialization
	void Awake () {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        vision = this.GetComponentInChildren<HostageSight>();
	}
	
	// Update is called once per frame
	void Update () {
        //m_NavMeshAgent.destination = m_Player.position;
        if (vision.followPlayer)
            m_NavMeshAgent.destination = m_Player.position;
        else
            m_NavMeshAgent.Stop();
	}
}
