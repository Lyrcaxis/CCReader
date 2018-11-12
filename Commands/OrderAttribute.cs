using System;

namespace CCReader.Commands {
	[AttributeUsage(AttributeTargets.Class)]
	class ListOrderAttribute : Attribute {
		public int Order;

		/// <summary>
		/// Specify the order in which the command should appear in the Help list.
		/// Completely cosmetic.
		/// </summary>
		/// <param name="OrderInList">Order.</param>
		public ListOrderAttribute(int OrderInList) => Order = OrderInList;
	}
}
