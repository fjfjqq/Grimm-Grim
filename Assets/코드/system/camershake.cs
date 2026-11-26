using UnityEngine;
using Cinemachine;

public class camershake : MonoBehaviour
{
   public static camershake shakeing;
    public CinemachineImpulseSource shake;
    private void Awake()
    {
        shakeing = this;
    }
    public void Shake(float shaketime, float shakepower)
    {
        shake.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = shaketime;
        shake.GenerateImpulse(shakepower);
    }
}