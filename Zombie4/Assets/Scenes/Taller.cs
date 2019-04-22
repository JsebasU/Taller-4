using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using zom = NPC.Enemy;
using ald = NPC.Ally;
using TMPro;

public class Taller : MonoBehaviour
{
    public TextMeshProUGUI numeroZombies;
    public  TextMeshProUGUI numeroAldeanos;
    public int numZombies;
    public int numAldeanos;
    public GameObject[] zom, ald;
    /// <summary>
    /// llama al el metodo de creacion de todos objects
    /// </summary>
    void Start()
    {
       new Creator();
    }

    /// <summary>
    /// Se asignan la cantidad objects aliados y enemigos si representan en hub
    /// </summary>
    void Update()
    {
        zom = GameObject.FindGameObjectsWithTag("Zombie");
        ald = GameObject.FindGameObjectsWithTag("Villager");
        foreach (GameObject item in zom)
        {
            numZombies = zom.Length;
        }
        foreach (GameObject item in ald)
        {
            numAldeanos = ald.Length;
        }

        if (ald.Length == 0)
        {
            numeroAldeanos.text = 0.ToString();
        }
        else
        {
            numeroAldeanos.text = numAldeanos.ToString();
        }

        numeroZombies.text = numZombies.ToString();
    }
}
/// <summary>
/// Se generan todos los objectos
/// </summary>
public class Creator
{
    public GameObject Omni;
    public readonly int minimo = Random.Range(5, 16);
    const int MAX = 26;
    public Creator()
    {
        Omni = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Omni.AddComponent<Camera>();
        Omni.AddComponent<Hero>();
       
          int total = (int)Random.Range(minimo, MAX);
          for (int i = 0; i < total; i++)
          {
                 Omni = GameObject.CreatePrimitive(PrimitiveType.Cube);
                 Omni.AddComponent<zom.Zombie>();
                 Omni.transform.position = new Vector3(Random.Range(-20, 21), 0, Random.Range(-20, 21));

          }
            int aTotal = MAX - total;
          for (int i = 0; i < aTotal; i++)
          {
                    Omni = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Omni.AddComponent<ald.Villager>();
                    Omni.transform.position = new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41));
          }  
    }
}
