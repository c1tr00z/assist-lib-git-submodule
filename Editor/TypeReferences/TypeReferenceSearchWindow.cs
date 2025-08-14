using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace AssistLib.TypeReferences.Editor {
    public class TypeReferenceSearchWindow : EditorWindow {

        #region Private Fields

        private Action<Type> _onFound = null;
        private Type _baseType;
        
        private List<Type> _allTypes = new List<Type>();
        private List<Type> _filteredTypes = new List<Type>();
        private string[] _typesNames = new string[0];
        
        private Type _defaultType = typeof(object);
        
        private Type _selectedType = typeof(object);
        
        private Type _prevType = typeof(object);

        private int _selectedTypeIndex = 0;

        private string _filterLine = "";
        private string _prevFilterLine = "";
        private string[] _filterLineSplitted = new string[0];

        #endregion
        
        #region EditorWindow Implementation

        private void OnEnable() {
            Init();
        }

        private void OnGUI() {
            var found = false;
            var closeAtTheEnd = false;
            _filterLine = EditorGUILayout.TextField("Search", _filterLine);
            if (!_filterLine.Equals(_prevFilterLine)) {
                Filter();
                _prevFilterLine = _filterLine;
            }
            var noTypes = _filteredTypes.Count == 0;
            if (noTypes) {
                GUI.enabled = false;
                // EditorGUILayout.Popup("Type", 0, _typesNames.ToArray());
            } else {
                GUI.enabled = true;
            }
            
            _selectedTypeIndex = EditorGUILayout.Popup("Type", _selectedTypeIndex, Enumerable.ToArray(_typesNames));
            _selectedTypeIndex =  _selectedTypeIndex < _filteredTypes.Count ? _selectedTypeIndex : 0;
            _selectedType = _selectedTypeIndex > -1 && _selectedTypeIndex < _filteredTypes.Count ? _filteredTypes[_selectedTypeIndex] : _defaultType;
            
            EditorGUILayout.BeginHorizontal(); 
            
            if (GUILayout.Button("Select")) {
                closeAtTheEnd = true;
            }

            GUI.enabled = true;
            
            if (GUILayout.Button("Cancel")) {
                _onFound = null;
                _baseType = null;
                _selectedType = null;
                closeAtTheEnd = true;
            }
            
            EditorGUILayout.EndHorizontal();

            if (!closeAtTheEnd) {
                return;
            }

            if (_selectedType != null) {
                FoundType(_selectedType);
            } else {
                Close();
            }
        }

        #endregion

        #region Class Implementation

        private void Init() {
            LoadDefault();
            Filter();
        }

        private void LoadDefault() {
            _allTypes = ReflectionUtils.GetSubclassesOf(_baseType);
        }
        
        private void Filter() {
            if (string.IsNullOrEmpty(_filterLine)) {
                _filteredTypes = _allTypes.ToList();
            } else {
                _filterLineSplitted = Enumerable.ToArray(_filterLine.ToLower().Split(' ').Where(s => !string.IsNullOrEmpty(s)));
                _filteredTypes = _allTypes.Where(t => _filterLineSplitted
                    .Any(s => t.FullName.ToLower().Contains(s))).ToList();
            }

            if (!_filteredTypes.Contains(_selectedType)) {
                var defaultType = typeof(object);
                _selectedType = _filteredTypes.Contains(defaultType) ? defaultType : _filteredTypes.FirstOrDefault();
            }

            _typesNames = Enumerable.ToArray(_filteredTypes.Select(t => t.FullName.Replace(".", "/")));
            _selectedTypeIndex = _filteredTypes.IndexOf(_selectedType);
            
            // CheckName();
        }

        private void FoundType(Type type) {
            _onFound?.Invoke(type);
            _onFound = null;
            _baseType = null;
            Close();
        }

        public static void ShowSearchWindow(Action<Type> onFound, Type baseType = null) {
            if (baseType == null) {
                baseType = typeof(object);
            }
            var window = EditorWindow.GetWindow<TypeReferenceSearchWindow>();
            window.minSize = new Vector2(500, 64);
            window.maxSize = new Vector2(500, 64);
            window._onFound = onFound;
            window._baseType = baseType;
            window.ShowModal();
        }

        #endregion
    }
}