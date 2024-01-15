Namespace Tenma
    Namespace Commands
        Public Structure SwitchMemoryStateCommand
            Implements TenmaSerializable
            Public MemoryNumber As MemoryNumber
            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"RCL{CInt(MemoryNumber)}"
            End Function
        End Structure
    End Namespace
End Namespace