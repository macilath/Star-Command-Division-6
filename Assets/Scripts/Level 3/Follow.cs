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
        if (m_Player != null)
        {
            Vector3 behindPlayerPosition = m_Player.position + (m_Player.forward.normalized * -5);

            if (vision.followPlayer)
            {
                m_NavMeshAgent.destination = behindPlayerPosition;
            }
            else
            {
                m_NavMeshAgent.Stop();
            }
        }
	}
}
