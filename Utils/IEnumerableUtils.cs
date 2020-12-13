using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using Random = UnityEngine.Random;

namespace c1tr00z.AssistLib.Utils {
    public static class IEnumerableUtils {

        #region Class Implementation

        public static T RandomItem<T>(this IEnumerable<T> items) {
            T[] array;
            if (items is T[]) {
                array = items as T[];
            } else {
                var list = new List<T>();
                list.AddRange(items);
                array = list.ToArray();
            }

            var randomized = Random.Range(0, array.Length);
            if (randomized >= array.Length && array.Length > 0) {
                randomized = array.Length - 1;
            }

            if (randomized < 0 || array.Length <= randomized) {
                return default(T);
            }

            return array[randomized];
        }

        public static void AddOrSet<T, P>(this Dictionary<T, P> dic, T key, P value) {
            if (key == null) {
                Debug.LogError(string.Format("Key cannot be null (value: {0})", value));
                return;
            }

            if (dic.ContainsKey(key)) {
                dic[key] = value;
            } else {
                dic.Add(key, value);
            }
        }

        public static void AddOrSetRange<T, P>(this Dictionary<T, P> dic, Dictionary<T, P> other) {
            if (other == null) {
                throw new InvalidOperationException("`Other' param cant be null");
            }

            other.ToList().ForEach(kvp => { dic.AddOrSet(kvp.Key, kvp.Value); });
        }

        public static List<T2> SelectNotNull<T1, T2>(this IEnumerable<T1> enumerable, Func<T1, T2> processor) {
            return enumerable.Select(processor).Where(item => item != null).ToList();
        }

        public static List<T> SelectNotNull<T>(this IEnumerable<T> enumerable) {
            return enumerable.Where(item => item != null).ToList();
        }

        // public static Dictionary<T2, T3> ToDictionary<T1, T2, T3>(this IEnumerable<T1> enumerable,
        //     Func<T1, T2> keySelector, Func<T1, T3> valueSelector) {
        //     var dic = new Dictionary<T2, T3>();
        //     enumerable.ForEach(i => { dic.Add(keySelector(i), valueSelector(i)); });
        //     return dic;
        // }

        public static List<T> ToUniqueList<T>(this List<T> enumerable) {
            var uniqueList = new List<T>();
            enumerable.ForEach(item => {
                if (!Enumerable.Contains(uniqueList, item)) {
                    uniqueList.Add(item);
                }
            });
            return uniqueList;
        }

        public static string ToPlainString(this Dictionary<object, object> dic) {
            var sb = new StringBuilder();

            Func<object, string> processor = (val) => {
                if (val is Dictionary<object, object>) {
                    return ((Dictionary<object, object>) val).ToPlainString();
                } else if (val is IEnumerable<object>) {
                    return ((IEnumerable<object>) val).ToPlainString();
                }

                return val.ToString();
            };

            sb.Append("[");
            foreach (KeyValuePair<object, object> kvp in dic) {
                sb.Append(string.Format("{0}:{1}", processor(kvp.Key), processor(kvp.Value)));
            }

            sb.Append("]");

            return sb.ToString();
        }

        public static string ToPlainString<T>(this IEnumerable<T> enumerable) {
            var sb = new StringBuilder();

            sb.Append(string.Format("[{0}][", enumerable.Count()));
            foreach (T item in enumerable) {
                sb.Append(item + ", ");
            }

            sb.Append("]");

            return sb.ToString();
        }

        public static int Min(this IEnumerable<int> enumerable) {
            var min = 0;
            foreach (var item in enumerable) {
                min = item < min ? item : min;
            }

            return min;
        }

        public static IEnumerable<T> SubArray<T>(this IEnumerable<T> enumerable, int index) {
            var length = enumerable.Count() - index;
            return SubArray(enumerable, index, length);
        }

        public static IEnumerable<T> SubArray<T>(this IEnumerable<T> enumerable, int index, int length) {
            T[] result = new T[length];
            Array.Copy(enumerable.ToArray(), index, result, 0, length);
            return result;
        }

        public static IEnumerable<T> MaxElements<T>(this IEnumerable<T> enumerable, Func<T, float> selector) {
            var maxElements = new List<T>();
            enumerable.ToList().ForEach(e => {
                if (maxElements.Count == 0) {
                    maxElements.Add(e);
                } else if (selector(maxElements.First()) == selector(e)) {
                    maxElements.Add(e);
                } else if (selector(maxElements.First()) < selector(e)) {
                    maxElements.Clear();
                    maxElements.Add(e);
                }
            });
            return maxElements;
        }

        public static T MaxElement<T>(this IEnumerable<T> enumerable, Func<T, float> selector) {
            return MaxElements(enumerable, selector).First();
        }

        public static IEnumerable<T> MinElements<T>(this IEnumerable<T> enumerable, Func<T, float> selector) {
            var minElements = new List<T>();
            enumerable.ToList().ForEach(e => {
                if (minElements.Count == 0) {
                    minElements.Add(e);
                } else if (selector(minElements.First()) == selector(e)) {
                    minElements.Add(e);
                } else if (selector(minElements.First()) > selector(e)) {
                    minElements.Clear();
                    minElements.Add(e);
                }
            });
            return minElements;
        }

        public static T MinElement<T>(this IEnumerable<T> enumerable, Func<T, float> selector) {
            return MinElements(enumerable, selector).First();
        }

        public static void Sort<T>(this List<T> list, Func<T, float> comparer) {
            Sort(list, comparer, false);
        }

        public static void Sort<T>(this List<T> list, Func<T, float> comparer, bool invert) {
            list.Sort((e1, e2) => {
                if (comparer(e1) > comparer(e2)) {
                    return !invert ? 1 : -1;
                } else if (comparer(e1) < comparer(e2)) {
                    return !invert ? -1 : 1;
                } else {
                    return 0;
                }
            });
        }

        public static void RemoveRange<T>(this List<T> list, IEnumerable<T> range) {
            range.ToList().ForEach(item => {
                if (list.Contains(item)) {
                    list.Remove(item);
                }
            });
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, T item) {
            return enumerable.ToList().IndexOf(item);
        }

        public static List<int> MakeList(this int lenght, Func<int, int> processor = null) {
            var list = new List<int>();
            for (var i = 0; i < lenght; i++) {
                list.Add(processor != null ? processor(i) : i);
            }

            return list;
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable) {
            var queue = new Queue<T>();
            foreach (T item in enumerable) {
                queue.Enqueue(item);
            }

            return queue;
        }

        public static int[] MakeIndexesFromTo(int from, int to) {
            var indexes = new List<int>();

            for (var i = from; i < to; i++) {
                indexes.Add(i);
            }

            return indexes.ToArray();
        }

        public static int[] MakeIndexesTo(int to) {
            return MakeIndexesFromTo(0, to);
        }

        public static void ForIndexesList(this int[] indexes, Action<int> indexProcessor) {
            for (var i = 0; i < indexes.Length; i++) {
                indexProcessor?.Invoke(indexes[i]);
            }
        }

        /**
         * * <summary>Iterate from fromTo[0] (Include) to fromTo[1] (Exclude)</summary>
         * */
        public static void Iterate(this int[] fromTo, Action<int> iterator) {
            if (fromTo.Length == 0 || iterator == null) {
                return;
            }

            var from = fromTo.Length > 1 ? fromTo[0] : 0;
            var to = fromTo.Length > 1 ? fromTo[1] : fromTo[0];

            for (int i = from; i < to; i++) {
                iterator(i);
            }
        }

        public static void Limit<T>(this List<T> list, int newSize) {
            if (list.Count <= newSize) {
                return;
            }

            var other = list.ToQueue();

            list.Clear();

            while (list.Count < newSize) {
                list.Add(other.Dequeue());
            }
        }

        #endregion
    }
}