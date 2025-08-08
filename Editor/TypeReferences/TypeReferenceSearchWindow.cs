using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AssistLib.TypeReferences.Editor {
    public class TypeReferenceSearchWindow : EditorWindow {

        #region Private Fields

        private Action<Type> _onFound = null;
        
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

        private void OnGUI() {
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
            
            if (GUILayout.Button("Create")) {
                FoundType(_selectedType);
                EditorGUILayout.EndHorizontal();
            }
            
            if (GUILayout.Button("Cancel")) {
                EditorGUILayout.EndHorizontal();
                _onFound = null;
                Close();
            }
            
            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        #endregion

        #region Class Implementation
        
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
            Close();
        }

        public static void ShowSearchWindow(Action<Type> onFound) {
            var window = EditorWindow.GetWindow<TypeReferenceSearchWindow>();
            window._onFound = onFound;
            window.ShowModal();
        }

        #endregion
    }
}