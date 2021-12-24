using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class CircleSpawner
    {
        private readonly Circle _circlePrefab;
        private readonly Transform _circlePanel;
        private readonly int _circlesInRow;
        private readonly float _spacing;
        private readonly float _duration;

        public CircleSpawner(Circle circlePrefab, Transform circlePanel, int circlesInRow, float spacing, float duration)
        {
            _circlePrefab = circlePrefab;
            _circlePanel = circlePanel;
            _circlesInRow = circlesInRow;
            _spacing = spacing;
            _duration = duration;
        }

        public List<Circle> CreateCircles(int difficult ,List<SymbolSettings> symbolSettings)
        {
            var circleList = new List<Circle>();
            
            for (var i = 0; i < _circlesInRow * (difficult+1); i++)
            {
                var circle = Object.Instantiate(_circlePrefab, _circlePanel);
                circle.Init(symbolSettings[i]);
                circle.transform.localScale = Vector3.zero;
                circle.transform.DOScale(Vector3.one, _duration);
                circle.transform.localPosition = SetPosition(difficult, i);
                circleList.Add(circle);
            }

            return circleList;
        }

        private Vector3 SetPosition(int difficult, int positionInList)
        {
            var rowWidth = (_spacing + 1) * (_circlesInRow-1);
            var columnHeight = (_spacing + 1) * (difficult);
            
            var circleRow = (positionInList) / _circlesInRow;
            var positionInRow = positionInList - circleRow * _circlesInRow;

            var rowOffset = (_spacing + 1) * circleRow;
            var columnOffset = (_spacing + 1) * positionInRow;

            return new Vector3(rowWidth/2 - columnOffset, columnHeight/2 -rowOffset);
        }
    }
}