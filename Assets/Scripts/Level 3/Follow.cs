using UnityEngine;
using System.Collections;

public class Follow : HumanController {

    public Transform m_Player;
    private NavMeshAgent m_NavMeshAgent;
    HostageSight vision;
	private tk2dSpriteAnimator anim;
	private Vector3 previousPosition;

	// Use this for initialization
	void Awake () {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        vision = this.GetComponentInChildren<HostageSight>();
		anim = GetComponentInChildren<tk2dSpriteAnimator>();
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
			
			if(anim != null)
			{
				if(m_NavMeshAgent.remainingDistance < 2 || transform.position == previousPosition)
				{
					if(!anim.IsPlaying("Hostage1Idle"))
					{
						anim.Play("Hostage1Idle");	
					}
				}
				else
				{
					if(!anim.IsPlaying("Hostage1Walk"))
					{
						anim.Play("Hostage1Walk");
					}
				}
			}
			previousPosition = transform.position;
        }
	}

	public override void kill()
	{
        GameManager.hostageAlive = false;
		Destroy(this.gameObject);
	}
}
