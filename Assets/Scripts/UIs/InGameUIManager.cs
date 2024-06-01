using System;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class InGameUIManager : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        public Observable<Unit> StartButtonClicked
        {
            get { return startButton.OnClickAsObservable(); }
        }
    }
}