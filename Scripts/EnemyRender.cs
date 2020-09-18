using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/EnemyRender")]
public class EnemyRender : MonoBehaviour
{
    public Enemy m_enemy;
    // Start is called before the first frame update
    void Start()
    {
        m_enemy = this.GetComponentInParent<Enemy>();//由父物体获得Enemy的脚本
    }
    private void OnBecameVisible()//当模型进入屏幕
    {
        m_enemy.m_isActiv = true;                           //更新Enemy脚本状态
        m_enemy.m_renderer = this.GetComponent<Renderer>(); //使Enemy获得Render
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
