using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBehaviour : MonoBehaviour
{
    public Animator animator;
    private int transitionState = 0;

    public float walk_speed = -0.015F;
    private bool isInteracted = false;
    private bool isStartled = false;
    private float startleTime = -1.0F;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartled && !isInteracted) {
            transform.Translate(0, 0, walk_speed);
            if (Time.time - startleTime > 1.3)
            {
                isInteracted = true;
                doAnimationTransition(2);
                enable_hearing();
            }
        }

    }

    void doAnimationTransition(int calledTransitionState) {
        if (transitionState < calledTransitionState) {
            transitionState = calledTransitionState;
            animator.SetBool(Animator.StringToHash("isStartled"), isStartled);
            animator.SetBool(Animator.StringToHash("isInteracted"), isInteracted);
        }
    }

    void enable_hearing() {
        foreach (GameObject audio in GameObject.FindGameObjectsWithTag("main_audio"))
        {
            audio.GetComponent<AudioSource>().mute = false;
        }
    }

    void OnTriggerEnter(Collider thecollision)
    {
        if (thecollision.gameObject.name == "Player" && !isStartled)
        {
            isStartled = true;
            startleTime = Time.time;
            doAnimationTransition(1);
        }
    }

}
