using System.ComponentModel;
namespace Tasks_System.Enumerators
{
    public enum TaskStatus
    {
        [Description("To do")]
        ToDo = 1,

        [Description("In Progress")]
        InProgress = 2,

        [Description("In Review")]
        InReview = 3,

        [Description("Done")]
        Done = 4
    }
}
