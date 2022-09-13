using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SceneManager : MonoBehaviour
{
    public SceneManager instance;
    public int killedEnemies;
    public Text KilledEnemiesText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateKilledEnemies()
    {
        killedEnemies++;
        KilledEnemiesText.text = "Enemigos asesinados = " + killedEnemies;
    }
}
