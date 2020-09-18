using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/Rocket")]
public class Rocket : MonoBehaviour
{
    public float m_speed = 10;
    public float m_power = 1.0f;
    protected Transform m_transform;
    private void OnBecameInvisible()
    {
        if (this.enabled)
            Destroy(this.gameObject);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
            return;
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.Translate(new Vector3(0, 0, m_speed*Time.deltaTime));
    }
}
