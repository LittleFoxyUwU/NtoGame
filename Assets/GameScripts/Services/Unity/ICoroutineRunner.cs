using System.Collections;
using UnityEngine;

namespace GameScripts.Services.Unity
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
        void StopCoroutine(IEnumerator enumerator);
    }
}