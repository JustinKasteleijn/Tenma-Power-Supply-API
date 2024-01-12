Imports System.Globalization

Namespace Tenma
    Partial Public Class Utils
        Public Shared Function FormatDecimalAsString(number As Decimal, decimalNumbers As Integer) As String
            Return number.ToString($"F{decimalNumbers}", CultureInfo.InvariantCulture)
        End Function
    End Class
End Namespace