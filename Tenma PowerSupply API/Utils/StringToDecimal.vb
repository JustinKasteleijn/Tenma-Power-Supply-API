Imports System.Globalization
Imports FunctionalExtensions.Functional

Namespace Tenma
    Partial Friend Module Utils
        Friend Function StringToDecimal(value As String) As Result(Of Decimal, String)
            Dim decimalValue As Decimal

            If Decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, decimalValue) Then
                Return Result(Of Decimal, String).Ok(decimalValue)
            Else
                Return Result(Of Decimal, String).Err($"Failed to parse '{value}' as Decimal.")
            End If
        End Function
    End Module
End Namespace