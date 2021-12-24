using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Quiz.Container
{
    [CreateAssetMenu(fileName = nameof(SymbolContainer), menuName = "Container/LetterContainer")]
    public class SymbolContainer: ScriptableObject
    {
        [SerializeField] private List<SymbolSettings> _letters;
        [SerializeField] private List<SymbolSettings> _numbers;

        public List<SymbolSettings> Letters => _letters;
        public List<SymbolSettings> Numbers => _numbers;
    }
}