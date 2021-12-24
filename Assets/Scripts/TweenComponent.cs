using System;
using DG.Tweening;
using Quiz.Inerfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class TweenComponent: IAnimated
    {
        private Transform _circle;
        private Transform _symbol;
        private GameObject _particle;
        private float _duration;
        private float _strenght;
        private float _bounceScale;

        public event Action CompleteAnimation;

        public TweenComponent(Transform circle, Transform symbol,float duration, float strenght, float bounceScale, GameObject particle)
        {
            _circle = circle;
            _symbol = symbol;
            _duration = duration;
            _strenght = strenght;
            _bounceScale = bounceScale;
            _particle = particle;
        }
        
        public void Shake()
        {
            _circle.transform.DOShakePosition(_duration, _strenght);
        }

        public void OnComplete()
        {
            var sequence = DOTween.Sequence();
            
            _particle.SetActive(true);
            sequence.Append(_symbol.transform.DOScale(Vector3.one * _bounceScale, _duration));
            sequence.Append(_symbol.transform.DOScale(Vector3.zero, _duration * 0.5f))
                .OnComplete(() => CompleteAnimation?.Invoke());
        }
    }
}