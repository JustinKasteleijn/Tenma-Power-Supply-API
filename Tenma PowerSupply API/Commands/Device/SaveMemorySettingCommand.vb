Namespace Tenma
    Namespace Commands
        Public Structure SaveMemorySettingCommand
            Implements TenmaSerializable
            Public memoryNumber As MemoryNumber
            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"SAV{CInt(memoryNumber)}"
            End Function
        End Structure
    End Namespace
End Namespace