﻿Namespace Tenma
    Partial Friend Module Commands
        Friend Structure SwitchMemoryStateCommand
            Implements ITenmaSerializable
            Public MemoryNumber As MemoryNumber
            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"RCL{CInt(MemoryNumber)}"
            End Function
        End Structure
    End Module
End Namespace