namespace Triggerable.Editor {
	public static class ComponentPath {
		private const string MODIFIER_PATH = "Triggerable/Modifiers/";
		private const string TRIGGER_PATH = "Triggerable/Triggers/";

		#region Modifiers

		public const string DELAY = MODIFIER_PATH + "Delay";
		public const string MULTIPLE_AND_SOURCES = MODIFIER_PATH + "Multiple sources AND";
		public const string MULTIPLE_DESTINATIONS = MODIFIER_PATH + "Multiple destinations";
		public const string REPEATER = MODIFIER_PATH + "Repeater";

		#endregion

		#region Triggers

		public const string ON_ENTER = TRIGGER_PATH + "On enter";
		public const string ON_EXIT = TRIGGER_PATH + "On exit";
		public const string ON_TIMED_STAY = TRIGGER_PATH + "On timed stay";
		public const string ON_PLAYER_INTERACT = TRIGGER_PATH + "On Player Interact";

		#endregion
	}

	public static class Headers {
		public const string TRIGGERABLE_SETTINGS = "Triggerable settings";
		public const string TRIGGER_SETTINGS = "Trigger settings";
		public const string MODIFIER_SETTINGS = "Modifier settings";
	}

	public static class Tooltips {
		public const string TRIGGERABLE_INITIAL_STATE_TOOLTIP = "Initial state";
		public const string DELAY_TIME_TOOLTIP =
			"How long will destination triggering be delayed after recieving a trigger";
		public const string TIME_TO_TRIGGER_TOOLTIP = "How long object has to stay in trigger to trigger";
		public const string REPEAT_COUNT_TOOLTIP = "How many times will destination be triggered";
		public const string REPEAT_INTEVAL_TOOLTIP = "Time in seconds between each destination triggering";
		public const string MULTIPLE_DESTINATIONS_TOOLTIP
			= "Set trigger destinations that will be triggered when this is triggered";
		public const string ONLY_PLAYER_TRIGGER = "True if only player can trigger this";
		public const string ON_PLAYER_INTERACT_DEBOUNCE = "Interaction debounce in seconds";
	}
}
