using System;
using UnityEngine;

namespace c1tr00z.AssistLib.PropertyReferences {
	public class ReferenceTypeAttribute : PropertyAttribute {

		#region Accessors

		public Type type { get; private set; }

		#endregion

		#region Constructors

		public ReferenceTypeAttribute (Type type) {
			this.type = type;
		}

		#endregion

		#region Attribute Implementation

		public override bool Match (object obj) {
			var other = obj as ReferenceTypeAttribute;
			if (other == null) {
				return false;
			}

			return other.type == this.type;
		}

		#endregion
	}
}
