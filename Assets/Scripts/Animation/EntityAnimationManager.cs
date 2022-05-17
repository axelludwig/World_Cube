using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EntityAnimationManager : NetworkBehaviour
{
    

    public AudioClip LandingAudioClip;
    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    [ServerRpc]
    public void SetBoolServerRPC(int animId, bool value)
    {
        SetBoolClientRPC(animId, value);
    }

    [ServerRpc]
    public void SetFloatServerRPC(int animId, float value)
    {
        SetFloatClientRPC(animId, value);
    }

    [ClientRpc]
    public void SetBoolClientRPC(int animId, bool value)
    {
        _animator.SetBool(animId, value);
    }

    [ClientRpc]
    public void SetFloatClientRPC(int animId, float value)
    {
        _animator.SetFloat(animId, value);
    }

    public void SetBool(int animId, bool value)
    {
        bool currentValue = _animator.GetBool(animId);
        if (value != currentValue)
            SetBoolServerRPC(animId, value);

    }

    public void SetFloat(int animId, float value)
    {
        float currentValue = _animator.GetFloat(animId);
        if (value != currentValue)
            SetFloatServerRPC(animId, value);
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, FootstepAudioVolume);
            }
        }
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(LandingAudioClip, transform.position, FootstepAudioVolume);
        }
    }
}
