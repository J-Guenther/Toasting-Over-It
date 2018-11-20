using UnityEngine;

public class ScaleObject : MonoBehaviour {

    public Vector3 originalScale = new Vector3(1,1,1);
    public Vector3 destinationScale = new Vector3(1,1,0.1f);

    public bool scale = false;

    public float speed1 = 2.0f;
    public float speed2 = 4.0f;

    private Overit3D overit3D;

	// Use this for initialization
	void Start () {
        overit3D = GameObject.FindGameObjectWithTag("Player").GetComponent<Overit3D>();
	}

    private void Update()
    {
        if (overit3D.jamRecharges)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, destinationScale, speed1 * Time.deltaTime);
        } else
        {   
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, speed2 * Time.deltaTime);
        }
    }
    
    
}
