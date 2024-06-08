using System;
using UnityEngine;

namespace Players.HP.Gauge.GaugeView
{
    public class GaugeView3DSpace : MonoBehaviour, IGaugeView
    {
        [SerializeField] private GameObject GaugeObject;
        private MeshRenderer _meshRenderer;
        private MaterialPropertyBlock _gaugeMaterialPropertyBlock;

        private void Start()
        {
            _gaugeMaterialPropertyBlock = new MaterialPropertyBlock();
        }

        public void SetValue(float value)
        {
            if (_meshRenderer != null)
            {
                _meshRenderer = GaugeObject.GetComponent<MeshRenderer>();
            }

            _meshRenderer.GetPropertyBlock(this._gaugeMaterialPropertyBlock);
            _gaugeMaterialPropertyBlock.SetFloat("_GaugeValue", value);
            _meshRenderer.SetPropertyBlock(_gaugeMaterialPropertyBlock);
        }
    }
}