namespace headlock.processes
{
    using model;

    public interface Process
    {
        void Go(ExecutionPoint point);
    }
}