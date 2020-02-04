using System;

namespace Attributes {

	/// <summary>
	/// Use this if you have AutoLinkField annotated fields in class.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class HasAutolinkFields : Attribute { }

	/// <summary>
	/// When using this your class should be annotated with HasAutoLinkFields!
	/// Annotated fields will be filled with components from gameObject.
	/// If you have more components of same type on gameObject, one will be assigned to this field.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class AutoLinkField : Attribute { }

	/// <summary>
	/// When using this your class should be annotated with HasAutoLinkFields!
	/// Annotated fields shuld be of type List and will be filled with all components of its list type from gameObject.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class AutoLinkListField : Attribute { }


	/// <summary>
	/// When using this your class should be annotated with HasAutoLinkFields!
	/// Annotated fields will be filled with component of the field type that is
	/// right after this component from gameObject. If inverse is set to true, component that is right before will
	/// be used. If there is no component before/after this one matching the type, field will be left unasgined.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class AutoLinkFieldSequential : Attribute {
		public bool IsInverse { get; } = false;

		public AutoLinkFieldSequential(bool inverse) => IsInverse = inverse;

		public AutoLinkFieldSequential() { }
	}

	/// <summary>
	/// Use this if you want to show a warning dialog on adding component to gameObject.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class WithWarningDialog : Attribute {
		public string WarningMessage { get; }

		public WithWarningDialog(string warningMessage) {
			WarningMessage = warningMessage;
		}
	}


	/// <summary>
	/// Annotate class to signal that only one instance of class can be added as a component on gameObject.
	/// Set <c>hasLinks</c> to true if you want your class to link following component on replace.
	/// This depends on order of components on gameObject.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class AllowOnlyOne : Attribute {
		public bool HasLinks { get; } = false;

		public AllowOnlyOne() { }

		public AllowOnlyOne(bool hasLinks) {
			HasLinks = hasLinks;
		}
	}

	/// <summary>
	/// Use this if you want to show a warning dialog that cancels adding component to gameObject on "No".
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class WithCancelAddingDialog : Attribute {
		public string WarningMessage { get; }

		public WithCancelAddingDialog(string warningMessage) {
			WarningMessage = warningMessage;
		}
	}

	/// <summary>
	/// Use this if you want to make user allow linking.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class WithLinkingCheckDialog : Attribute { }

	/// <summary>
	/// Annotate class with this to signal that it has fields that require components.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class RequiresComponents : Attribute { }

	/// <summary>
	/// Annotate class with this to singal it requires a component
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class ComponentRequiresComponent : Attribute {

		public Type requiredComponentType { get; }

		public ComponentRequiresComponent(Type requiredComponentType) {
			this.requiredComponentType = requiredComponentType;
		}
 }

	/// <summary>
	/// Annotate field with this to signal that field requires component.
	/// Dialog will be displayed for each field that has this attribute. If Yes is selected,
	/// required component will be added to gameObject and linked to this field.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class FieldRequiresComponent : Attribute { }

	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class FieldListRequiresComponent : Attribute {
		public int RequireAtLeast { get; } = 1;

		public FieldListRequiresComponent() { }

		public FieldListRequiresComponent(int requireAtLeast) {
			RequireAtLeast = requireAtLeast;
		}
	}
}
