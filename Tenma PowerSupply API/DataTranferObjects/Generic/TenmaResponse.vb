Namespace Tenma
    Public Structure TenmaResponse(Of T)
        Public tenmaPowerSupply As TenmaPowerSupply
        Public result As T
    End Structure
End Namespace