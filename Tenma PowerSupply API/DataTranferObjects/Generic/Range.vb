Namespace Tenma
    Partial Module DataTransferObjects
        Friend Structure Range
            Public Max As Integer
            Public Min As Integer

            Public Overloads Function ToString() As String
                Return $"Min: {Min} Max: {Max}"
            End Function
        End Structure

    End Module
End Namespace