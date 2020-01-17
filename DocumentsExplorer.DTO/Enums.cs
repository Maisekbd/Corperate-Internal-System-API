using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public enum EnumMemberType
    {
        CouncilMember,

        Employee,

        External

    }


    public enum EnumAttachmentType
    {

        MeetingAgenda,

        MeetingAgendaDetail,

        ReferenceItem, 

        DecisionExecution,
    }


    public enum EnumNotificationType
    {
        [Description("تنفيذ قرار")]
        DecisionExecution,

        [Description("للعلم")]
        DecsionForInform,

        [Description("تم تنفيذ القرار")]
        DecsionExecuted,

        [Description("إجتماع")]
        MeetingRequest,

    }

    public enum EnumMailStatus
    {
        [Description("غير مرسل")]
        NotSend,
        [Description("مرسل")]
        Send

    }

    public enum EnumDecisionStatus
    {
        [Description("لم يبدأ التنفيذ")]
        NotStrated,
        [Description("تحت التنفيذ")]
        UnderExecution,
        [Description("تم التنفيذ")]
        Executed

    }



    public enum EnumActionType
    {
        [Description("noValue")]
        noValue,

        [Description("للاستشارة")]
        Advise,

        [Description("للتنفيذ")]
        Execute,

        [Description("حفظ")]
        Save,

        [Description("للعلم")]
        Inform,

        [Description("للمراجعة")]
        Review,

        [Description("تحتاج تواصل")]
        NeedContact
    }



    public static class EnumerationExtension
    {
        public static string Description(this Enum value)
        {
            // get attributes  
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(false);

            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(0);
            }

            // return description
            return displayAttribute?.Description ?? "Description Not Found";
        }
    }
}
