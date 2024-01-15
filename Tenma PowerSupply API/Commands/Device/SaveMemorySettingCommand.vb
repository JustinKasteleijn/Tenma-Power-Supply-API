Namespace Tenma
    Namespace Commands
        Public Structure SaveMemorySettingCommand
            Implements TenmaSerializable
            Public MemoryNumber As MemoryNumber
            Public Function ToCommand() As String Implements TenmaSerializable.ToCommand
                Return $"SAV{CInt(MemoryNumber)}"
            End Function
        End Structure
    End Namespace
End Namespace