using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("MyGame/TitleScreen")]
public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnButtonGameStart()
    {
        SceneManager.LoadScene("level-one");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
