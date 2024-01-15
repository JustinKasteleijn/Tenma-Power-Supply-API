Imports FunctionalExtensions.Functional

Namespace Tenma
    Partial Friend Module Utils
        Friend Function StringToInt(value As String) As Result(Of Integer, String)
            Dim intValue As Decimal

            If Integer.TryParse(value, intValue) Then
                Return Result(Of Integer, String).Ok(intValue)
            Else
                Return Result(Of Integer, String).Err($"Failed to parse '{value}' as Decimal.")
            End If
        End Function
    End Module
End Namespace