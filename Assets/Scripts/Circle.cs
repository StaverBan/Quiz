using System;
using Common;
using DefaultNamespace;
using Quiz.Inerfaces;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _symbolSprite;
    [SerializeField] private GameObject _particle;
    [SerializeField] private float _duration;
    [SerializeField] private float _strenght;
    [SerializeField] private float _bounceScale;
    
    public event Action<SymbolSettings, IAnimated> OnTouch;
    private SymbolSettings _symbol;
    private TweenComponent _tweenComponent;

    public void Init (SymbolSettings symbolSettings)
    {
        _symbol = symbolSettings;
        _symbolSprite.sprite = _symbol.SymbolScene;
        _symbolSprite.transform.localEulerAngles = Vector3.back * symbolSettings.RotateAngle;
        _tweenComponent = new TweenComponent(transform, _symbolSprite.transform,_duration,_strenght, _bounceScale, _particle);
    }

    private void OnMouseDown()
    {
        OnTouch?.Invoke(_symbol, _tweenComponent);
    }

}


