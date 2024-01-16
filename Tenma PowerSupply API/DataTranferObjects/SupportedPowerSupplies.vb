Namespace Tenma
    Public Class SupportedPowerSupplies
        Private Shared supported As New Lazy(Of Dictionary(Of String, Ranges))(Function() Init(), isThreadSafe:=False)

        Private Shared Function Init() As Dictionary(Of String, Ranges)
            Return New Dictionary(Of String, Ranges) From {
            {"72-2535", New Ranges With {
                            .VoltageRange = New Range With {.Min = 0, .Max = 30},
                            .CurrentRange = New Range With {.Min = 0, .Max = 5}
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
                            .VoltageRange = New Range With {.Min = 0, .Max = 60},
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
                            .VoltageRange = New Range With {.Min = 0, .Max = 30},
                            .CurrentRange = New Range With {.Min = 0, .Max = 3}
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
