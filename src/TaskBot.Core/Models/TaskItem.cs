namespace TaskBot.Core.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AssignedTo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemType TaskType { get; set; }
        public bool IsCompleted { get; set; }
    }
    public enum TaskItemType
    {
        Basic,
        Project,
        Other
    }
}
