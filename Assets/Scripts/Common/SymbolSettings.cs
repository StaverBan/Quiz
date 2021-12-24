using System;
using UnityEngine;

namespace Common
{
    [Serializable]
    public struct SymbolSettings
    {
        [SerializeField] private Sprite _symbolScene;
        [SerializeField] private float _rotateAngle;
        
        public Sprite SymbolScene => _symbolScene;
        public float RotateAngle => _rotateAngle;
    }
}