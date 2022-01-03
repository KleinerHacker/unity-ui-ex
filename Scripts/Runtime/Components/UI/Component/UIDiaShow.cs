using System;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Types;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.ComponentMenu + "/Dia Show")]
    [DisallowMultipleComponent]
    public sealed class UIDiaShow : UIBehaviour
    {
        #region Inspector Data

        [SerializeField]
        private Sprite[] imagesToShow;

        [SerializeField]
        private bool random;

        [Header("Animation")]
        [SerializeField]
        private AnimationCurve fadingCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        [SerializeField]
        private float fadingSpeed = 1f;

        [SerializeField]
        private float showTime = 1f;

        [Header("References")]
        [SerializeField]
        private Image image1;

        [SerializeField]
        private CanvasGroup imageGroup1;

        [SerializeField]
        private Image image2;

        [SerializeField]
        private CanvasGroup imageGroup2;

        #endregion

        private Sprite[] _images;

        #region Properties

        public Sprite[] Images
        {
            get => _images;
            set
            {
                OnImagesUpdated();
                _images = value;
            }
        }

        #endregion

        private uint _imageIndex = 0;
        private float _timeCounter = 0f;
        private State _state = State.Fade;
        private ActiveImage _activeImage = ActiveImage.None;

        #region Builtin Methods

        protected override void Awake()
        {
            if (imagesToShow != null && imagesToShow.Length > 0)
            {
                Images = imagesToShow;
            }
        }

        protected override void OnEnable()
        {
            _activeImage = 0;
            image1.sprite = null;
            imageGroup1.alpha = 0f;
            image2.sprite = null;
            imageGroup2.alpha = 0f;

            OnImageChanged();
        }

        private void FixedUpdate()
        {
            if (_state == State.Show)
            {
                _timeCounter += Time.fixedUnscaledDeltaTime;
                if (_timeCounter >= showTime)
                {
                    _timeCounter = 0f;
                    OnImageChanged();
                }
            }
        }

        #endregion

        private void OnImageChanged()
        {
            if (Images == null || Images.Length <= 0)
                return;

            Debug.Log("Change Image");

            _state = State.Fade;

            UpdateIndex();
            RunFading(() => _state = State.Show);
        }

        private void UpdateIndex()
        {
            if (random)
            {
                uint newIndex;
                if (_images.Length <= 1)
                {
                    newIndex = 0;
                }
                else
                {
                    do
                    {
                        newIndex = (uint)Random.Range(0, _images.Length);
                    } while (newIndex == _imageIndex);
                }

                _imageIndex = newIndex;
            }
            else
            {
                _imageIndex++;
                if (_imageIndex >= _images.Length)
                {
                    _imageIndex = 0;
                }
            }
        }

        private void RunFading(Action onFinished)
        {
            switch (_activeImage)
            {
                case ActiveImage.None:
                case ActiveImage.Two:
                    image1.sprite = Images[_imageIndex];
                    break;
                case ActiveImage.One:
                    image2.sprite = Images[_imageIndex];
                    break;
                default:
                    throw new NotImplementedException();
            }

            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(fadingCurve, fadingSpeed, v =>
                {
                    switch (_activeImage)
                    {
                        case ActiveImage.None:
                            imageGroup1.alpha = v;
                            break;
                        case ActiveImage.One:
                            imageGroup1.alpha = v;
                            imageGroup2.alpha = 1f - v;
                            break;
                        case ActiveImage.Two:
                            imageGroup1.alpha = 1f - v;
                            imageGroup2.alpha = v;
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                })
                .WithFinisher(() =>
                {
                    _activeImage = _activeImage == ActiveImage.One ? ActiveImage.Two : ActiveImage.One;
                    onFinished?.Invoke();
                })
                .Start();
        }

        private void OnImagesUpdated()
        {
            _imageIndex = 0;
        }

        private enum State
        {
            Show,
            Fade
        }

        private enum ActiveImage
        {
            None,
            One,
            Two
        }
    }
}