using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

    private Camera cam;
    private GameCamera gcam;
    public Transform targetLoc;
    private Vector3 targetPos;
    private Vector3 screenMiddle; 
    private float zoom;

    private float yFactor = 17.5f;
    private float scaleFactor = 1.3f;//30;

	// Use this for initialization
	void Start () {
        GameObject mcam = GameObject.Find("Main Camera");
	    cam = mcam.GetComponent<Camera>();
        gcam = mcam.GetComponent<GameCamera>();
        zoom = gcam.currentZoom;
        //this.transform.position = cam.transform.position;
        Vector3 pos = this.transform.position;
        Vector3 posScreen = cam.WorldToScreenPoint(pos);
        posScreen.y = (Screen.height) + 96;
        posScreen.x = (Screen.width / 2);
        this.transform.position = cam.ScreenToWorldPoint(posScreen);
	}
	
	// Update is called once per frame
    void Update()
    {
        Transform dest = GameObject.Find("SpaceStation").transform;
        Vector3 targetPos = new Vector3(dest.position.x, dest.position.y, 0);
        Vector3 cameraPos = new Vector3(cam.transform.position.x, cam.transform.position.y, 0);
        Vector3 toTarget = targetPos - cameraPos;
        //float angleDir = AngleDir(this.transform.up, toTarget, Vector3.forward);
        //float targetAngle = Vector3.Angle(Vector3.up, toTarget) * angleDir;
        float targetAngle = SignedAngle(Vector3.up, toTarget);
        this.transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        adjustOrtho();

        //this.transform.rotation = transform.rotation * Quaternion.Euler(90, 90, 0);
        //this.transform.rotation.SetAxisAngle(new Vector3(1, 0, 0), 0);
        //this.transform.rotation.SetAxisAngle(new Vector3(0, 1, 0), 0); 
    }

    protected float SignedAngle(Vector3 a, Vector3 b)
    {
        float angle = Vector3.Angle(a, b);
        return angle * Mathf.Sign(Vector3.Cross(a, b).y);
    }

    void adjustOrtho()
    {
        float percentZoom = (gcam.currentZoom - gcam.minZoom) / gcam.maxZoom;
        Vector3 pos = this.transform.position;
        pos.y += yFactor * (percentZoom - zoom);
        this.transform.position = pos;
        Vector3 scale = this.transform.localScale;
        scale.x += scaleFactor * (percentZoom - zoom);
        scale.y += scaleFactor * (percentZoom - zoom);
        this.transform.localScale = scale;
        zoom = percentZoom;
    }
}
