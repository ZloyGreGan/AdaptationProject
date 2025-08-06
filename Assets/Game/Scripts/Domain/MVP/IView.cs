using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Domain.MVP
{
    public interface IView
    {
        void Init(APresenter presenter);
        bool IsActive { get; }
        MonoBehaviour monoBehaviour { get; }
        UniTask SetActive(bool value);
    }
}