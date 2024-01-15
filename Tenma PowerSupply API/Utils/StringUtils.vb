Imports System.Globalization

Namespace Tenma
    Partial Friend Module Utils
        Public Function FormatDecimalAsString(number As Decimal, decimalNumbers As Integer) As String
            Return number.ToString($"F{decimalNumbers}", CultureInfo.InvariantCulture)
        End Function
    End Module
End Namespace