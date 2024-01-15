Namespace Tenma
    Namespace Commands
        Public Structure SwitchMemoryStateCommand
            Implements TenmaSerializable
            Public memoryNumber As MemoryNumber
            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"RCL{CInt(memoryNumber)}"
            End Function
        End Structure
    End Namespace
End Namespace