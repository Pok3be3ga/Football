using System.Collections;
using TMPro;
using UnityEngine;

namespace Layer_lab._3D_Casual_Character
{
    public class CharacterControl : MonoBehaviour
    {
        public static CharacterControl Instance;
        public CharacterBase CharacterBase { get; set; }
        private Animator animator; 
        private Coroutine _coroutine;
        void Awake()
        {
            Instance = this;
            animator = GetComponentInChildren<Animator>();
            CharacterBase = GetComponentInChildren<CharacterBase>();
        }

        public void PlayAnimation(AnimationClip clip)
        {
            animator.CrossFadeInFixedTime(clip.name, 0.25f);
            if(_coroutine != null) StopCoroutine(_coroutine);
        
            if (!clip.isLooping)
            {
                StartCoroutine(ChangeIdleText(clip.length));
            }
        
        }

        IEnumerator ChangeIdleText(float duration)
        {
            yield return new WaitForSeconds(duration);
        }
    }
}
