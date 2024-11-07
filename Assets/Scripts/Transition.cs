using UnityEngine;

public class Transition : MonoBehaviour
{
    private Animator transition;
    void Start()
    {
        transition = GetComponent<Animator>();
        transition.SetTrigger("StartTransition");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
