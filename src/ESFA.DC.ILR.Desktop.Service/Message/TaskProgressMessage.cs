namespace ESFA.DC.ILR.Desktop.Service.Message
{
    public class TaskProgressMessage
    {
        public TaskProgressMessage(string taskName, int currentTask, int taskCount)
        {
            TaskName = taskName;
            CurrentTask = currentTask;
            TaskCount = taskCount;
        }

        public string TaskName { get; }

        public int CurrentTask { get; }

        public int TaskCount { get; }
    }
}
