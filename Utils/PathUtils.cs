﻿using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace c1tr00z.AssistLib.Utils {
    public static class PathUtils {

        #region Class Implementation

        public static string Combine(params string[] paths) {
            if (paths == null || paths.Length == 0) {
                return "";
            }

            if (paths.Length == 1) {
                return paths[0];
            }

            if (paths.Length == 2) {
                return Path.Combine(paths[0], paths[1]);
            }

            var path = paths[0];
            for (var i = 1; i < paths.Length; i++) {
                path = Path.Combine(path, paths[i]);
            }

            return path;
        }


        public static void CreatePath(params string[] paths) {
            if (paths == null || paths.Length == 0) {
                return;
            }

            for (var i = 0; i < paths.Length; i++) {
                var newPath = Combine(paths.SubArray(0, i + 1).ToArray());
                var newDir = new DirectoryInfo(Combine(Application.dataPath, newPath));
                if (!newDir.Exists) {
                    newDir.Create();
                }
            }

#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }

        #endregion
    }
}
