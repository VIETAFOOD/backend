using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extentions
{
	public static class EnumExtensions
	{
		public static string GetEnumDescription(this Enum value)
		{
			var field = value.GetType().GetField(value.ToString());

			var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

			return attribute == null ? value.ToString() : attribute.Description;
		}
	}
}
