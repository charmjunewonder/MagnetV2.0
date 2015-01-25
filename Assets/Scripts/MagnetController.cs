using UnityEngine;
using System.Collections;

public class MagnetController : MonoBehaviour {
    public GameObject sign;

    public float quantityOfCharge = 1.0f;

    public float factor = 8.0f;

	public int score = 0;

	public TextMesh scoreText;

    private GameObject strongestMagnet = null;

    private Vector2 strongestForce = Vector2.zero;

    private AudioClip bouncing;

	// Use this for initialization
	void Start () {
        bouncing = Resources.Load("Sound/bounce",typeof(AudioClip)) as AudioClip;
		scoreText.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addScore(int s){
		score += s;
		scoreText.text = "" + score;
	}

    void FixedUpdate()
    {
        strongestMagnet = GetStrongestMagnet();
        if (strongestMagnet == null) {
            strongestForce = Vector2.zero;
            return;
        }
        DrawLineToStrongestMagnet();
		rigidbody2D.AddForce (strongestForce * 10);//, ForceMode2D.Impulse);
    }

    public void OnQuantityChanged() {
        if (UIProgressBar.current != null)
        {
			float nextQuantity =  1.0f - UIProgressBar.current.value * 2 ;
			sign.GetComponent<SignController>().ChangeSign(nextQuantity);
			quantityOfCharge = nextQuantity;

        }
    }


    GameObject GetStrongestMagnet() {
        GameObject[] magnets = GameObject.FindGameObjectsWithTag("Magnet");
        GameObject strongest = null;
        float max = float.MinValue;
        for (int i = 0; i < magnets.Length; i++) {
            Vector2 force = ComputeMagentForce(magnets[i].transform.position, magnets[i].GetComponent<MagnetController>().quantityOfCharge);
            if(!Mathf.Approximately(0.0f,force.magnitude) && force.magnitude > max){
                strongest = magnets[i];
                max = force.magnitude;
                strongestForce = force;
            }
        }
        return strongest;
    }

    
    void DrawLineToStrongestMagnet()
    {
        if (strongestMagnet == null) return;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, strongestMagnet.transform.position);
		float len = (transform.position - strongestMagnet.transform.position).magnitude;
		len = Mathf.Clamp (len,0.0f, 6.0f);
		len = len / 6.1f;
		lineRenderer.material.SetFloat ("_Intensity",len);
    }

    Vector2 ComputeMagentForce(Vector3 targetPositon, float targetQuantity) {

        Vector3 direction = transform.position - targetPositon;
        float distance = direction.magnitude;
        float force = targetQuantity * quantityOfCharge * factor / (distance * distance);
        direction.Normalize();
        direction *= force;
        return new Vector2(direction.x, direction.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        AudioSource.PlayClipAtPoint(bouncing,transform.position);
    }
    /*
     * 
     * 
    void DrawLineToNearestMagnet()
    {
        if (nearestMagnet == null) return;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, nearestMagnet.transform.position);
    }
     * 
     * 
     GameObject GetNearestMagnet()
    {
        GameObject[] magnets = GameObject.FindGameObjectsWithTag("Magnet");
        GameObject nearest = null;
        float min = float.MaxValue;
        for (int i = 0; i < magnets.Length; i++)
        {
            float distance = Vector3.Distance(magnets[i].transform.position, transform.position);
            if (distance < min && !Mathf.Approximately(distance, 0))
            {
                nearest = magnets[i];
                min = distance;
            }
        }
        return nearest;
    }
     * 
     * 
    void AddInstanceForce()
    {
        if (nearestMagnet == null) return;
        Vector3 direction = nearestMagnet.transform.position - transform.position;
        if (direction.magnitude < 1f) return;
        float distance = Vector3.Distance(nearestMagnet.transform.position, transform.position);
        float force = -1.0f * nearestMagnet.GetComponent<MagnetController>().quantityOfCharge * quantityOfCharge / (distance * distance);
        direction.Normalize();
        Vector2 direction2d = new Vector2(direction.x,direction.y);
        rigidbody2D.AddForce(direction2d * force * factor);
    }*/
}
