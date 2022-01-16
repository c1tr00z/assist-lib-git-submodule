using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using c1tr00z.AssistLib.Common;
using c1tr00z.AssistLib.ResourceManagement.Editor;
using c1tr00z.AssistLib.Utils;
using UnityEditor;
using UnityEngine;

namespace c1tr00z.AssistLib.Common {
    public class CreateScriptableObjectWindow : EditorWindow {

        #region Private Fields

        private List<Type> _allTypes = new List<Type>();
        private List<Type> _filteredTypes = new List<Type>();
        private string[] _typesNames = new string[0];
        
        private Type _defaultType = typeof(DefaultScriptableObject);

        private Type _selectedType = typeof(DefaultScriptableObject);
        
        private Type _prevType = typeof(DefaultScriptableObject);

        private int _selectedTypeIndex = 0;

        private string _filterLine = "";
        private string _prevFilterLine = "";
        private string[] _filterLineSplitted = new string[0];

        private string _newSOName;

        #endregion

        #region Static Methods

        [MenuItem("Assets/Create/Scriptable Object (Window)")]
        public static void Open() {
            var window = GetWindow(typeof(CreateScriptableObjectWindow), true);
            window.minSize = new Vector2(500, 100);
            window.maxSize = new Vector2(500, 100);
            window.Show();
        }

        #endregion

        #region Unity Events

        private void OnEnable() {
            Init();
        }

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
            CheckName();
            _newSOName = EditorGUILayout.TextField("New asset name", _newSOName);
            
            GUILayout.Label($"Current path: {Path.Combine(SelectionUtils.GetSelectedPath(), _newSOName)}.asset");

            EditorGUILayout.BeginHorizontal(); 
            
            if (GUILayout.Button("Create")) {
                ScriptableObjectsEditorUtils.Create(_selectedType, _newSOName);
                Close();
            }
            
            if (GUILayout.Button("Cancel")) {
                Close();
            }
            
            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        #endregion

        #region Class Implementation

        private void Init() {
            LoadDefault();
            Filter();
        }

        private void LoadDefault() {
            _allTypes = ReflectionUtils.GetSubclassesOf(typeof(ScriptableObject));
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
                var defaultType = typeof(DefaultScriptableObject);
                _selectedType = _filteredTypes.Contains(defaultType) ? defaultType : _filteredTypes.FirstOrDefault();
            }

            _typesNames = Enumerable.ToArray(_filteredTypes.Select(t => t.FullName.Replace(".", "/")));
            _selectedTypeIndex = _filteredTypes.IndexOf(_selectedType);
            
            CheckName();
        }

        private void CheckName() {
            if (_selectedType == null) {
                _newSOName = null;
                return;
            }
            if (string.IsNullOrEmpty(_newSOName) || _prevType != _selectedType) {
                _prevType = _selectedType;
                _newSOName = $"New {_selectedType.Name}";
            }
        }

        #endregion
        
    }
}