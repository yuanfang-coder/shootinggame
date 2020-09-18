using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/Enemy")]

public class Enemy : MonoBehaviour
{
    public float m_speed = 1;
    public float m_life = 10;
    public int m_point = 10;            //敌人的分数
    protected float m_rotSpeed = 30;    //旋转速度
    internal Renderer m_renderer;       //模型渲染组件
    internal bool m_isActiv = false;    //是否激活
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = this.GetComponent<Renderer>();
    }
    private void OnBecameInvisible()
    {
        m_isActiv = true;
    }

    // Update is called once per frame
     void Update()
    {
        UpdateMove();
        if (m_isActiv && !this.m_renderer.isVisible)
            Destroy(this.gameObject);
    }
    protected virtual void UpdateMove()//将来要拓展功能
    {
        float rx = (float)Math.Sin(Time.time) * Time.deltaTime;//左右移动
        transform.Translate(new Vector3(rx, 0, -m_speed * Time.deltaTime));//前进（向-Z方向）

    }
    protected virtual void  OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRocket")
        {
            Rocket rocket = other.GetComponent<Rocket>();
            if (rocket != null)
            {
                m_life -= rocket.m_power;
                if (m_life <= 0)
                {
                    GameManager.Instance.AddScore(m_point);
                    Destroy(this.gameObject);
                }
            }
        }
        else if (other.tag == "Player")
        {
            m_life = 0;
            Destroy(this.gameObject);
        }
    }
}
