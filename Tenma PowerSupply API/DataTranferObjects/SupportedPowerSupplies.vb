Imports FunctionalExtensions.Functional

Namespace Tenma
    Friend Class SupportedPowerSupplies
        Private Shared ReadOnly SupportedRanges As New Lazy(Of Dictionary(Of String, Ranges))(Function() Init(), isThreadSafe:=False)
        Friend Shared Function GetCurrentRange(partNumber As String) As Result(Of Range, String)
            Try
                Dim voltageRange As Range? = SupportedRanges.Value.
                    Where(Function(pair) pair.Key = partNumber).
                    Select(Function(pair) pair.Value.CurrentRange).
                    FirstOrDefault()

                If voltageRange IsNot Nothing Then
                    Return Result(Of Range, String).Ok(voltageRange)
                Else
                    Return Result(Of Range, String).Err($"Part number '{partNumber}' not found.")
                End If
            Catch ex As Exception
                Return Result(Of Range, String).Err($"Error retrieving current range: {ex.Message}")
            End Try
        End Function

        Friend Shared Function GetVoltageRange(partNumber As String) As Result(Of Range, String)
            Try
                Dim voltageRange As Range? = SupportedRanges.Value.
                    Where(Function(pair) pair.Key = partNumber).
                    Select(Function(pair) pair.Value.VoltageRange).
                    FirstOrDefault()

                If voltageRange IsNot Nothing Then
                    Return Result(Of Range, String).Ok(voltageRange)
                Else
                    Return Result(Of Range, String).Err($"Part number '{partNumber}' not found.")
                End If
            Catch ex As Exception
                Return Result(Of Range, String).Err($"Error retrieving voltage range: {ex.Message}")
            End Try
        End Function


        Friend Shared Function IsSupported(partNumber As String) As Boolean
            If partNumber Is Nothing Then
                Return False
            End If

            Return SupportedRanges.Value.ContainsKey(partNumber)
        End Function


        Private Shared Function Init() As Dictionary(Of String, Ranges)
            Return New Dictionary(Of String, Ranges) From {
            {"72-2535", New Ranges With {
                            .VoltageRange = New Range With {.Min = 0, .Max = 30},
                            .CurrentRange = New Range With {.Min = 0, .Max = 3}
                         }
            },
            {"72-2540", New Ranges With {
                            .VoltageRange = New Range With {.Min = 0, .Max = 30},
                            .CurrentRange = New Range With {.Min = 0, .Max = 5}
                         }
            },
            {"72-2545", New Ranges With {
                            .VoltageRange = New Range With {.Min = 0, .Max = 60},
                            .CurrentRange = New Range With {.Min = 0, .Max = 2}
                         }
            },
            {"72-2550", New Ranges With {
                            .VoltageRange = New Range With {.Min = 0, .Max = 30},
                            .CurrentRange = New Range With {.Min = 0, .Max = 3}
                         }
            },
            {"72-2925", New Ranges With {
                            .VoltageRange = New Range With {.Min = 0, .Max = 30},
                            .CurrentRange = New Range With {.Min = 0, .Max = 10}
                         }
            },
            {"72-2930", New Ranges With {
                            .VoltageRange = New Range With {.Min = 0, .Max = 30},
                            .CurrentRange = New Range With {.Min = 0, .Max = 5}
                         }
            },
            {"72-2935", New Ranges With {
                            .VoltageRange = New Range With {.Min = 0, .Max = 60},
                            .CurrentRange = New Range With {.Min = 0, .Max = 5}
                         }
            },
            {"72-2940", New Ranges With {
                            .VoltageRange = New Range With {.Min = 0, .Max = 60},
                            .CurrentRange = New Range With {.Min = 0, .Max = 5}
                         }
            },
            {"72-10480", New Ranges With {
                            .VoltageRange = New Range With {.Min = 0, .Max = 30},
                            .CurrentRange = New Range With {.Min = 0, .Max = 3}
                         }
            }
        }
        End Function
    End Class

End Namespace
