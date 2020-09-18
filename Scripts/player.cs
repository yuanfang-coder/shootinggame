using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("MyGame/player")]
public class player: MonoBehaviour
{
    Transform m_transform;
    public float m_speed = 5;
    public float m_life = 3;
    public Transform m_rocket;      //关联玩家子弹Prefab
    public Transform m_explosionFX; //爆炸特效组件
    public AudioClip m_shootClip;   //声音文件
    protected AudioSource m_audio;  //声音源组件，用于播放声音
    protected Vector3 m_targetPos;  //目标位置
    public LayerMask m_inputMask;   //鼠标射线碰撞层
    float m_rocketTimer = 0;
    
    private void OnTriggerEnter(Collider other)//重载自MonoBehaviour的回调函数，碰撞时触发
    {
        if (other.tag != "PlayerRocket")
        {
            m_life -= 1;
            GameManager.Instance.ChangeLife(m_life);
            if (m_life <= 0)
            {
                //当生命清0时
                Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);//播放爆炸特效
                Destroy(this.gameObject);//自我销毁
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        m_audio = this.GetComponent<AudioSource>();//添加代码获取声音源组件
        m_targetPos = this.m_transform.position;   //初始化目标位置
    }
    void MoveTo()//鼠标移动函数
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 ms = Input.mousePosition;                                    //获得鼠标屏幕位置
            Ray ray = Camera.main.ScreenPointToRay(ms);                          //把屏幕位置转换为射线
            RaycastHit hitinfo;                                                  //记录射线碰撞信息
            bool iscast = Physics.Raycast(ray, out hitinfo, 1000, m_inputMask);  //使用Raycast函数射出射线，把碰撞结果返回到hitinfo
            if (iscast)
            {
                //如果射中目标，记录射线碰撞点
                m_targetPos = hitinfo.point;
            }
        }
        //使用Vector3提供的MoveTowards函数，获得朝目标移动的位置
        Vector3 pos = Vector3.MoveTowards(m_transform.position, m_targetPos, m_speed * Time.deltaTime);
        //更新当前位置
        this.m_transform.position = pos;
    }
    // Update is called once per frame
    void Update()
    {
        float move_v = 0;
        float move_h = 0;
        
        if (Input.GetKey(KeyCode.UpArrow))//Z轴递增
        {
            move_v += m_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) //Z轴递减
        {
            move_v -= m_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) //X轴递减
        {
            move_h -= m_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow)) //X轴递增
        {
            move_h += m_speed * Time.deltaTime;
        }
        m_transform.Translate(new Vector3(move_h, 0, move_v));
        //m_transform.position += new Vector3(move_h, 0, move_v);
        m_rocketTimer -= Time.deltaTime;
        if (m_rocketTimer <= 0)
        {
            m_rocketTimer = 0.3f;
            if (Input.GetKey(KeyCode.Space)||Input.GetMouseButton(0))
            {
                Instantiate(m_rocket, m_transform.position, m_transform.rotation);
                m_audio.PlayOneShot(m_shootClip);//每射击一次播放一次声音（m_shootClip）
            }
        }
    }
}
