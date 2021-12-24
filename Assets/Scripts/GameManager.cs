using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Quiz.Common;
using Quiz.Inerfaces;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class GameManager
    {
        private CircleSpawner _circleSpawner;
        private SymbolCollection _symbolCollection;
        private Levels nowDifficult;
        private List<Circle> _circles;
        private SymbolSettings answer;

        public event Action EndGame;
        public event Action<string> ShowQuiz;

        public GameManager(CircleSpawner circleSpawner, SymbolCollection symbolCollection)
        {
            _circleSpawner = circleSpawner;
            _symbolCollection = symbolCollection;
            
            StartGame();
        }

        private void StartGame()
        {
            nowDifficult = 0;
            CreateLevel();
        }

        private void CreateLevel()
        {
            var listOfSymbol = _symbolCollection.GetRandomQuiz((int)nowDifficult + 1);
            
            answer = _symbolCollection.GetAnswer();

            ShareSignal();
            
            _circles = _circleSpawner.CreateCircles((int) nowDifficult,listOfSymbol);

            SubscribeCircle();
        }

        private async void ShareSignal()
        {
            await Task.Delay(1);
            ShowQuiz?.Invoke(answer.SymbolScene.name);
        }

        private void NextRound()
        {
            ClearCircle();
            
            if (nowDifficult == Levels.high)
            {
                EndGame?.Invoke();
                return;                
            }
            
            nowDifficult++;

            CreateLevel();
        }

        private void SubscribeCircle()
        {
            foreach (var circle in _circles)
            {
                circle.OnTouch += OnCircleClick;
            }
        }

        private void UnSubscribeCircle()
        {
            foreach (var circle in _circles)
            {
                circle.OnTouch -= OnCircleClick;
            }
        }

        private void ClearCircle()
        {
            foreach (var circle in _circles)
            {
                Object.Destroy(circle.gameObject);
            }
            _circles.Clear();
        }

        private void OnCircleClick(SymbolSettings symbol, IAnimated animated)
        {
            if (symbol.SymbolScene == answer.SymbolScene)
            {
                animated.OnComplete();
                animated.CompleteAnimation += OnAnimationComplete;
                UnSubscribeCircle();
            }
            else
            {
                animated.Shake();
            }
        }

        private void OnAnimationComplete()
        {
            NextRound();
        }

    }
}