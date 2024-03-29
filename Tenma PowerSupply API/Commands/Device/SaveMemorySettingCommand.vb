﻿Namespace Tenma
    Partial Friend Module Commands
        Friend Structure SaveMemorySettingCommand
            Implements ITenmaSerializable
            Public MemoryNumber As MemoryNumber
            Public Function ToCommand() As String Implements ITenmaSerializable.ToCommand
                Return $"SAV{CInt(MemoryNumber)}"
            End Function
        End Structure
    End Module
End Namespace