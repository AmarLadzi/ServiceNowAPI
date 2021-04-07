namespace WindowsService.Triggers.EventRow
{
    public enum EventTypeDto
    {
        None = 0,
        Server = 100,
        Protocol = 200,
        User = 300,
        Variable = 400,
        VariableReaderGranted = 500,
        VariableReaderDenied =550,
        AlarmChanged = 600,
        AreaOrGroupChanged = 700
    }
}