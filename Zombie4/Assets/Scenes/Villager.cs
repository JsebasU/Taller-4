using System.Collections;
using System.Collections.Generic;
using zom = NPC.Enemy;
using UnityEngine;

namespace NPC
{
    namespace Ally
    {
        public class Villager : MonoBehaviour
        {
            public InfoAlde infoAlde = new InfoAlde();
            int age;
            public float runningSpeed;
            public bool runningState = false;
            bool mirar = false;
            public Vector3 direccion;
            public float distancia;
            public float tiempo;
            GameObject Target;
            GameObject[] zombies;
            public Nombres nombres;
            public Estado aldeanoEstado;

            //nombres
            public enum Nombres
            {
                Carlos, Sebastian, Eduardo, Daniel, Cata,
                Danilo, Felipe, Tatiana, Juan, Ronald,
                Geremias, Rene, Eugenia, Eulari, Gala,
                Gurtza, Gudula, Hebe, Fara, Fedora

            }
            // estados
            public enum Estado
            {
                Idle, Moving, Rotating, Running
            }
            /// <summary>
            /// Designar todo sobre el aldeano
            /// </summary>
            public void Start()
            {
                Rigidbody rb;
                this.gameObject.tag = "Villager";
                rb = this.gameObject.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
                rb.useGravity = false;
                nombres = (Nombres)Random.Range(0, 21);
                infoAlde.name = nombres.ToString();
                age = Random.Range(15, 101);
               infoAlde.edad = age;
                runningSpeed = 10 / age;
                this.gameObject.name = nombres.ToString();
                StartCoroutine(BuscaZombies());
            }
            /// <summary>
            ///busqueda de los zombies cercanos 
            /// </summary>
            /// <returns></returns>
            IEnumerator BuscaZombies()
            {
                zombies = GameObject.FindGameObjectsWithTag("Zombie");
                foreach (GameObject item in zombies)
                {
                    zom.Zombie componenteZombie = item.GetComponent<zom.Zombie>();
                    if (componenteZombie != null)
                    {
                        distancia = Mathf.Sqrt(Mathf.Pow((item.transform.position.x - transform.position.x), 2) + Mathf.Pow((item.transform.position.y - transform.position.y), 2) + Mathf.Pow((item.transform.position.z - transform.position.z), 2));
                        if (!runningState)
                        {
                            if (distancia < 5f)
                            {
                                aldeanoEstado = Estado.Running;
                                Target = item;
                                runningState = true;
                            }
                        }
                    }
                }

                if (runningState)
                {
                    if (distancia > 5f)
                    {
                        runningState = false;
                    }
                }

                yield return new WaitForSeconds(0.1f);
                StartCoroutine(BuscaZombies());
            }
            /// <summary>
            /// cambia de estado
            /// </summary>
            void Update()
            {
                tiempo += Time.deltaTime;
                if (!runningState)
                {
                    if (tiempo >= 3)
                    {
                        aldeanoEstado = (Estado)Random.Range(0, 3);
                        mirar = true;
                        tiempo = 0;
                    }
                }

                switch (aldeanoEstado)
                {
                    case Estado.Idle:
                        break;

                    case Estado.Moving:
                        if (mirar)
                        {
                            this.gameObject.transform.Rotate(0, Random.Range(0, 361), 0);
                        }
                        this.gameObject.transform.Translate(0, 0, 0.05f);
                        mirar = false;
                        break;

                    case Estado.Rotating:
                        this.gameObject.transform.Rotate(0, Random.Range(1, 50), 0);
                        break;

                    case Estado.Running:
                        direccion = Vector3.Normalize(Target.transform.position - transform.position);
                        transform.position -= direccion * runningSpeed;
                        break;
                }
            }
        }
    }
}

