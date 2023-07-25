using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;

public class WitAutoReactivation : MonoBehaviour
{
    WitService _wit;
    [SerializeField] AudioSource OutputAudio;
    public bool temporarilyIgnore;

    public void Ignore(bool ignore)
    {
        temporarilyIgnore = ignore;
    }

    private void Start()
    {
        _wit = GetComponent<WitService>();
        if(_wit != null)
            _wit.Activate();
    }

    void Update()
    {
        if(_wit == null)
            _wit = GetComponent<WitService>();

        if (!_wit.Active)
        {
            if (!OutputAudio.isPlaying && !temporarilyIgnore)
                _wit.ActivateImmediately();
        }
        else
        {
            if (OutputAudio.isPlaying || temporarilyIgnore)
                _wit.DeactivateAndAbortRequest();
        }
    }
}
