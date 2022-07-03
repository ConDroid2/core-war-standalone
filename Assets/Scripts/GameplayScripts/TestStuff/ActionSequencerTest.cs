using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSequencerTest : MonoBehaviour
{
    [SerializeField] private Wait10SecondsAction seconds;
    [SerializeField] private RunImmeately immediate;
    private ActionSequencer sequencer;

    // Start is called before the first frame update
    void Start()
    {
        sequencer = new ActionSequencer();
        sequencer.AddGameAction(seconds);
        StartCoroutine(sequencer.RunSequence());
        sequencer.AddGameAction(immediate);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            sequencer.AddGameAction(immediate);
        }
    }
}
