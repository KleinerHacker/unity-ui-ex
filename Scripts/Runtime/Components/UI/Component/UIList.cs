using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component
{
    public abstract class UIList<TI, TM> : UIBehaviour where TI : UIListItem<TM>
    {
        #region Inspector Data

        [Header("Behavior")]
        [SerializeField]
        private ListUpdatePolicy updatePolicy = ListUpdatePolicy.FillAndClear;

        [Header("References")]
        [SerializeField]
        private RectTransform content;

        [SerializeField]
        private TI itemPrefab;

        #endregion

        #region Properties

        protected abstract TM[] ContentData { get; }

        #endregion

        protected readonly IList<TI> _listItems = new List<TI>();

        #region Builtin Methods

        protected override void Awake()
        {
            switch (updatePolicy)
            {
                case ListUpdatePolicy.FillAndClear:
                case ListUpdatePolicy.Refresh:
                case ListUpdatePolicy.None:
                    break;
                case ListUpdatePolicy.OnlyStartup:
                    FillList();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        protected override void OnEnable()
        {
            switch (updatePolicy)
            {
                case ListUpdatePolicy.FillAndClear:
                    FillList();
                    break;
                case ListUpdatePolicy.Refresh:
                    Refresh();
                    break;
                case ListUpdatePolicy.OnlyStartup:
                case ListUpdatePolicy.None:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        protected override void OnDisable()
        {
            switch (updatePolicy)
            {
                case ListUpdatePolicy.FillAndClear:
                    ClearList();
                    break;
                case ListUpdatePolicy.Refresh:
                case ListUpdatePolicy.OnlyStartup:
                case ListUpdatePolicy.None:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        public virtual void Refresh()
        {
            ClearList();
            FillList();
        }

        private void ClearList()
        {
            foreach (var listItem in _listItems)
            {
                RemoveItem(listItem);
            }

            _listItems.Clear();
        }

        private void FillList()
        {
            if (ContentData == null)
                return;
        
            foreach (var data in ContentData)
            {
                AddItem(data);
            }
        }

        protected void AddItem(TM data)
        {
            var go = InstantiateItem(itemPrefab.gameObject, Vector3.zero, Quaternion.identity, content);

            var listItem = go.GetComponent<TI>();
            listItem.Model = data;

            _listItems.Add(listItem);
            OnAddItem(listItem);
        }

        protected void RemoveItem(TM data)
        {
            var item = _listItems.First(x => Equals(x.Model, data));
            RemoveItem(item);
        }

        private void RemoveItem(TI listItem)
        {
            DestroyItem(listItem);
            OnRemoveItem(listItem);
        }

        protected virtual GameObject InstantiateItem(GameObject prefab, Vector3 pos, Quaternion rot, Transform target)
        {
            return Instantiate(prefab, pos, rot, target);
        }

        protected virtual void DestroyItem(TI listItem)
        {
            Destroy(listItem.gameObject);
        }

        protected virtual void OnAddItem(TI item)
        {
        }

        protected virtual void OnRemoveItem(TI item)
        {
        }
    }

    public abstract class UiSelectionList<TI, TM> : UIList<TI, TM> where TI : UiSelectableListItem<TM>
    {
        #region Properties

        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value < 0 || value >= ContentData.Length)
                    throw new IndexOutOfRangeException();

                _selectedIndex = value;
                OnSelectedItemChanged();
            }
        }

        public TM SelectedItem
        {
            get => SelectedIndex < 0 || SelectedIndex >= ContentData.Length ? default : ContentData[SelectedIndex];
            set
            {
                var index = -1;
                for (var i = 0; i < ContentData.Length; i++)
                {
                    var data = ContentData[i];
                    index++;

                    if (data.Equals(value))
                        index = i;
                }

                if (index < 0)
                    throw new IndexOutOfRangeException("Item not found");

                SelectedIndex = index;
            }
        }

        #endregion

        public bool SelectNext(bool rollOver = true)
        {
            if (SelectedIndex + 1 >= ContentData.Length)
            {
                if (rollOver)
                {
                    SelectedIndex = 0;
                }

                return false;
            }

            SelectedIndex++;
            return true;
        }

        public bool SelectPrev(bool rollOver = true)
        {
            if (SelectedIndex - 1 < 0)
            {
                if (rollOver)
                {
                    SelectedIndex = ContentData.Length - 1;
                }

                return false;
            }

            SelectedIndex--;
            return true;
        }

        protected virtual void OnSelectedItemChanged()
        {
            foreach (var listItem in _listItems)
            {
                listItem.IsSelected = false;
            }

            if (SelectedItem == null)
                return;

            foreach (var listItem in _listItems)
            {
                if (listItem.Model.Equals(SelectedItem))
                {
                    listItem.IsSelected = true;
                    break;
                }
            }
        }
    }

    public enum ListUpdatePolicy
    {
        FillAndClear,
        Refresh,
        OnlyStartup,
        None
    }
}