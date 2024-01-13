using System;
using System.Collections;
using Peg.AutoCreate;
using UnityEngine;

namespace Peg.Systems
{
    /// <summary>
    /// Universal object for hold references to coroutines so that other non-gameobjects can use them easily.
    /// </summary>
    [AutoCreate(CreationTime = RuntimeInitializeLoadType.AfterSceneLoad)]
    public class GlobalCoroutine
    {
        public static GlobalCoroutine Instance;

#pragma warning disable CS0618 // Type or member is obsolete
        GlobalCoroutineOwnerBehaviour Owner;
#pragma warning restore CS0618 // Type or member is obsolete

        void AutoAwake()
        {
            Instance = this;
            var go = GameObject.Find("Global Coroutine Holder");
            if (go == null)
				go = new GameObject("Global Coroutine Holder");
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                go.hideFlags = HideFlags.HideAndDontSave;
                GameObject.DontDestroyOnLoad(go);
            }
#endif
#pragma warning disable CS0618 // Type or member is obsolete
            Owner = go.AddComponent<GlobalCoroutineOwnerBehaviour>();
#pragma warning restore CS0618 // Type or member is obsolete
        }

        void AutoDestroy()
        {
            if (Owner != null && Owner.gameObject is not null)
            {
#if UNITY_EDITOR
                if(Application.isPlaying)
                    GameObject.Destroy(Owner.gameObject);
                else GameObject.DestroyImmediate(Owner.gameObject);
#else
                GameObject.Destroy(Owner.gameObject);
#endif
                Owner = null;
            }
        }

        public static Coroutine Start(IEnumerator action)
        {
            return Instance.Owner.StartCoroutine(action);
        }

        public static void Stop(Coroutine action)
        {
            Instance.Owner.StopCoroutine(action);
        }
    }


    /// <summary>
    /// Used internall by GlobalCoroutineOwner to attach coroutines to a hidden dummy gameobject.
    /// </summary>
    [Obsolete("Do not manually apply this behaviour. It is done automatically by GlobalCoroutine at creation.")]
    public class GlobalCoroutineOwnerBehaviour : MonoBehaviour { }
}
