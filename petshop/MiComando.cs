using System.Windows.Input;

namespace petshop;

public sealed class MiComando : ICommand
{
    private readonly Action _action;
    private readonly Func<bool> _canExecute;

    public MiComando(Action action, Func<bool> canExecute)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => _canExecute();

    public void Execute(object? parameter) => _action();

    public void RaiseCanExecuteChanged() =>
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
