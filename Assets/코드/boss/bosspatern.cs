using UnityEngine;

public abstract class bosspatern : MonoBehaviour //여러 패턴을 가지니 부모 스크립트를 두고 통제
{
    public bool running = false;

    public abstract void Startpattern();
}
