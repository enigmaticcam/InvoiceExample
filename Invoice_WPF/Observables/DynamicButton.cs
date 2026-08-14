using CommunityToolkit.Mvvm.Input;

namespace Invoice_WPF.Observables;

public class DynamicButton
{
    public DynamicButton(string content, IAsyncRelayCommand command)
    {
        Content = content;
        Command = command;
    }

    public string Content { get; }
    public IAsyncRelayCommand Command { get; }
}
