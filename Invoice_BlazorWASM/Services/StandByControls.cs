namespace Invoice_BlazorWASM.Services;

public class StandByControls
{
    private Dictionary<string, Register> _tokens = new();

    public bool Disabled(string control)
    {
        var register = GetRegister(control);
        if (register.SoftDisabled == null)
        {
            return register.IsRunning;
        }
        else
        {
            return register.IsRunning || register.SoftDisabled();
        }
    }

    private Register GetRegister(string control)
    {
        if (!_tokens.ContainsKey(control))
            throw new Exception($"{control} is not a registered Control");
        return _tokens[control];
    }

    private Task OnRunning(string control, bool isRunning)
    {
        _tokens[control].IsRunning = isRunning;
        return Task.CompletedTask;
    }

    public void RegisterControl(string control, BroadcastToken token)
    {
        _tokens.Add(control, new Register(token));
        token.OnRunning += x => OnRunning(control, x);
    }

    public void RegisterControl(string control, BroadcastToken token, Func<bool> softDisabled)
    {
        _tokens.Add(control, new Register(token, softDisabled));
        token.OnRunning += x => OnRunning(control, x);
    }

    private class Register
    {
        public Register(BroadcastToken token)
        {
            Token = token;
        }

        public Register(BroadcastToken token, Func<bool> softDisabled)
        {
            Token = token;
            SoftDisabled = softDisabled;
        }

        public BroadcastToken Token { get; set; }
        public bool IsRunning { get; set; }
        public Func<bool>? SoftDisabled { get; set; }
    }
}


