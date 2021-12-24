using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;

namespace DefaultNamespace
{
    public class SymbolCollection
    {
        private readonly List<List<SymbolSettings>> _collection;
        private List<List<SymbolSettings>> _currentCollection = new List<List<SymbolSettings>>();
        private readonly int _rowCount;
        private SymbolSettings answerSymbol;

        public SymbolCollection(List<List<SymbolSettings>> collection, int rowCount)
        {
            _collection = collection;
            _rowCount = rowCount;

            var currentLists = _collection.Select(settings => settings.ToList()).ToList();
            
            _currentCollection.AddRange(currentLists);
        }

        public List<SymbolSettings> GetRandomQuiz(int difficult)
        {
            var list = new List<SymbolSettings>();
            
            var randomCount = Random.Range(0, _collection.Count);

            var randomCollection = _collection[randomCount];
            var currentRandomCollection = _currentCollection[randomCount];
            
            answerSymbol = currentRandomCollection[Random.Range(0, currentRandomCollection.Count)];

            currentRandomCollection.Remove(answerSymbol);
            
            list.Add(answerSymbol);

            for (var i = 0; i < _rowCount * difficult - 1; i++)
            {
                var symbol = randomCollection[Random.Range(0, randomCollection.Count)];
                if (list.Contains(symbol))
                    i--;
                else
                    list.Add(symbol);
            }

            Randomize(list, Random.Range(1,10));
            
            return list;
        }

        public SymbolSettings GetAnswer()
        {
            return answerSymbol;
        }

        private void Randomize (List<SymbolSettings> list, int step)
        {
            var i = step;
            while (i > 1)
            {
                for (var j = 0; j < list.Count - 1; j++)
                {
                    (list[j + 1], list[j]) = (list[j], list[j + 1]);
                }

                i--;
            }
        }
        
    }
}