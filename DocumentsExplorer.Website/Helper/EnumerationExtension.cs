using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DocumentsExplorer.Website.Helper
{
    public static class EnumHelper<T>
    {
        static EnumHelper()
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum) { throw new ArgumentException("Type '" + enumType.Name + "' is not an enum"); }
        }

        public static string GetEnumDescription(T value)
        {
            var fi = typeof(T).GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static IEnumerable<SelectListItem> GetSelectList()
        {
            var groupDictionary = new Dictionary<string, SelectListGroup>();

            var enumType = typeof(T);
            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;

            foreach (var field in fields)
            {
                var display = field.GetCustomAttribute<DisplayAttribute>(false);
                var description = field.GetCustomAttribute<DescriptionAttribute>(false);
                var group = field.GetCustomAttribute<CategoryAttribute>(false);

                var text = display?.GetName() ?? display?.GetShortName() ?? display?.GetDescription() ?? display?.GetPrompt() ?? description?.Description ?? field.Name;
                var value = field.Name;
                var groupName = display?.GetGroupName() ?? group?.Category ?? string.Empty;
                if (!groupDictionary.ContainsKey(groupName)) { groupDictionary.Add(groupName, new SelectListGroup { Name = groupName }); }

                yield return new SelectListItem
                {
                    Text = text,
                    Value = value,
                    Group = groupDictionary[groupName],
                };
            }
        }
    }

    //public static class EnumerationExtension
    //{
    //    public static string Description(this Enum value)
    //    {
    //        // get attributes  
    //        var field = value.GetType().GetField(value.ToString());
    //        var attributes = field.GetCustomAttributes(false);

    //        // Description is in a hidden Attribute class called DisplayAttribute
    //        // Not to be confused with DisplayNameAttribute
    //        dynamic displayAttribute = null;

    //        if (attributes.Any())
    //        {
    //            displayAttribute = attributes.ElementAt(0);
    //        }

    //        // return description
    //        return displayAttribute?.Description ?? "Description Not Found";
    //    }
    //}
}