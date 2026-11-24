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
        Debug.Log("쉐이크 호출됨");
        shake.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = shaketime;
        shake.GenerateImpulse(shakepower);
    }
}