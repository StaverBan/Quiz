using System;

namespace Quiz.Inerfaces
{
    public interface IAnimated
    {
        event Action CompleteAnimation;
        void Shake();
        void OnComplete();
    }
}