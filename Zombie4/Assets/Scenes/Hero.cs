using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using zom = NPC.Enemy;
using ald = NPC.Ally;
using TMPro;

public class Hero : MonoBehaviour
{
    float distancia;
    float distanciaZ;
    public float time;
    public TextMeshProUGUI textoZombie;
    public TextMeshProUGUI textoAldeano;
    GameObject[] aldeanos, zombie;
    InfoAlde infoAlde = new InfoAlde();
    InfoZombie infoZombie = new InfoZombie();
    Movement movement;
    Look look;

    /// <summary>
    /// asignamos heroe y su hub
    /// </summary>
    void Start()
    {
        Rigidbody hero = this.gameObject.AddComponent<Rigidbody>();
        this.gameObject.tag = "Hero";
        this.gameObject.name = "Hero";
        hero.constraints = RigidbodyConstraints.FreezeAll;
        hero.useGravity = false;
        StartCoroutine(BuscaEntidades());
        movement = gameObject.AddComponent<Movement>();
        look = gameObject.AddComponent<Look>();
        textoZombie = GameObject.FindGameObjectWithTag("TextZombie").GetComponent<TextMeshProUGUI>();
        textoAldeano = GameObject.FindGameObjectWithTag("TextAlde").GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// contamos un tiempo para desaparicion de hub
    /// </summary>
    public void Update()
    {
        time += Time.fixedDeltaTime;
        movement.Move();
        look.Arround();
    }

    /// <summary>
    /// la distancia para el activamiento del hub
    /// </summary>
    /// <returns></returns>
    IEnumerator BuscaEntidades()
    {
        zombie = GameObject.FindGameObjectsWithTag("Zombie");
        aldeanos = GameObject.FindGameObjectsWithTag("Villager");

        // Hub  para el aldeano
        foreach (GameObject item in aldeanos)
        {
            yield return new WaitForEndOfFrame();
           ald.Villager componenteAldeano = item.GetComponent<ald.Villager>();
            if (componenteAldeano != null)
            {
                distancia = Mathf.Sqrt(Mathf.Pow((item.transform.position.x - transform.position.x), 2) + Mathf.Pow((item.transform.position.y - transform.position.y), 2) + Mathf.Pow((item.transform.position.z - transform.position.z), 2));
                if (distancia < 5f)
                {
                    time = 0;
                    infoAlde = item.GetComponent<ald.Villager>().infoAlde;
                    textoAldeano.text = "Hola, soy " + infoAlde.name + " y tengo " + infoAlde.edad.ToString() + " años";
                }
                if (time > 3)
                {
                    textoAldeano.text = " ";
                }
            }
        }

        // Hub para el zombie
        foreach (GameObject itemZ in zombie)
        {
            yield return new WaitForEndOfFrame();
            zom.Zombie componenteZombie = itemZ.GetComponent<zom.Zombie>();
            if (componenteZombie != null)
            {
                distanciaZ = Mathf.Sqrt(Mathf.Pow((itemZ.transform.position.x - transform.position.x), 2) + Mathf.Pow((itemZ.transform.position.y - transform.position.y), 2) + Mathf.Pow((itemZ.transform.position.z - transform.position.z), 2));
                if (distanciaZ < 5f)
                {
                    time = 0;
                    infoZombie = itemZ.GetComponent<zom.Zombie>().infoZombie;
                    textoZombie.text = "Waaaarrrr quiero comer " + infoZombie.gusto;
                }
                if (time > 3)
                {
                    textoZombie.text = " ";
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(BuscaEntidades());
    }
   
}

public class Movement : MonoBehaviour
{
    /// <summary>
    /// Transform de hero
    /// </summary>
    Transform movableTransform;

    private void Awake()
    {
        movableTransform = transform;
    }
    /// <summary>
    /// Se optiene el moviento hacia adelante y hacia atras de forma aleatoria y su input
    /// </summary>
    /// <param name="speedChange"></param>
    public void Move()
    {
        Move move = new Move(Random.Range(0.09f, 0.19f));

        if (Input.GetKey(KeyCode.W))
        {
            movableTransform.Translate(0, 0, move.speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            movableTransform.Translate(0, 0, -move.speed);
        }

    }

}
public class Look : MonoBehaviour
{
    /// <summary>
    /// se toma el tranform del objeto que tiene el script
    /// </summary>
    Transform movableTransform;

    private void Awake()
    {
        movableTransform = transform;
    }

    /// <summary>
    /// se le asignan input  con los que se activara la rotarcion la mira del personaje 
    /// </summary>

    public void Arround()
    {
        if (Input.GetKey(KeyCode.A))
        {
            movableTransform.Rotate(0, -1, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            movableTransform.Rotate(0, 1, 0, 0);
        }

    }
}
public class Move
{
    public readonly float speed;
    public Move(float veloz)
    {
        speed = veloz;
    }

}



