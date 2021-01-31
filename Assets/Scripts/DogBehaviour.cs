using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : MonoBehaviour
{
    public Animator animator;
    private int transitionState = 0;

    public float walk_speed = 0.1F;
    private bool isInteracted = false;
    private bool isStartled = false;
    private float startleTime = -1.0F;
    private bool sawCinematic1 = false;
    private bool sawCinematic2 = false;
    private bool sawCinematic3 = false;
    public GameObject cinematicObject1;
    public GameObject cinematicObject2;
    public GameObject cinematicObject3;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(cinematicObject1.transform.position, player.transform.position) <= 5 && !sawCinematic1)
        {
            sawCinematic1 = true;
        }

        if (Vector3.Distance(cinematicObject2.transform.position, player.transform.position) <= 4 && !sawCinematic2)
        {
            sawCinematic2 = true;
        }

        if (Vector3.Distance(cinematicObject3.transform.position, player.transform.position) <= 5 && !sawCinematic3)
        {
            sawCinematic3 = true;
        }

        if (isStartled && !isInteracted)
        {
            transform.Translate(walk_speed, 0, 0);
            if (Time.time - startleTime > 3)
            {
                isInteracted = true;
                GameObject.FindGameObjectWithTag("game_won_music").GetComponent<AudioSource>().Play();
            }
        }

    }

    void doAnimationTransition(int calledTransitionState)
    {
        if (transitionState < calledTransitionState)
        {
            transitionState = calledTransitionState;
            animator.SetBool(Animator.StringToHash("isStartled"), isStartled);
        }
    }

    void OnTriggerEnter(Collider thecollision)
    {
        if (thecollision.gameObject.name == "Player" && !isStartled && sawCinematic1 && sawCinematic2 && sawCinematic3)
        {
            isStartled = true;
            startleTime = Time.time;
            doAnimationTransition(1);
        }
    }
}
