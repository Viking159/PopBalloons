using Features.Gameplay;
using System;
using Zenject;

namespace Features.UI.GameScene
{


    public class LifesView : IInitializable, IDisposable
    {
        private readonly LifesController _lifesController = default;
        private readonly LifesViewData _viewData = default;

        public LifesView(LifesController lifesController, LifesViewData viewData)
        {
            _lifesController = lifesController;
            _viewData = viewData;
        }
        void IInitializable.Initialize()
        {
            SetView();
            _lifesController.onLifesCountChanged += SetView;
        }

        private void SetView()
        {
            for (int i = 0; i < _viewData.LifesImages.Count; i++)
            {
                _viewData.LifesImages[i].gameObject.SetActive(i < _lifesController.Lifes);
            }
        }

        void IDisposable.Dispose()
            => _lifesController.onLifesCountChanged -= SetView;
    }
}
