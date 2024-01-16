# Tenma Power Supply API

## Introduction

Welcome to the Tenma Power Supply API, a library for interacting with Tenma power supplies using a functional rust alike approach. This library is built using Visual Basic and Functional Extensions. It utilizes the serial port to communicate with the power supply.  

## Supported Power Supplies

| Part Number | Voltage Range | Current Range |
|-------------|---------------|---------------|
| 72-2535     | 0-30V          | 0-5A          |
| 72-2540     | 0-30V          | 0-5A          |
| 72-2545     | 0-60V          | 0-2A          |
| 72-2550     | 0-30V          | 0-3A          |
| 72-2925     | 0-60V          | 0-10A         |
| 72-2930     | 0-30V          | 0-5A          |
| 72-2935     | 0-60V          | 0-5A          |
| 72-2940     | 0-30V          | 0-3A          |
| 72-10480    | 0-30V          | 0-3A          |



## Features

- **Functional Design**: The API follows a functional programming paradigm, providing a clean and expressive way to work with Tenma power supplies.
- **Result Monad**: Robust error handling using the Result Monad, ensuring clear handling of success and failure scenarios.
- **Immutable State**: The library leverages immutability to maintain a consistent and predictable flow of data through functional transformations.
- **Ease of Use**: The API is designed to be user-friendly, allowing developers to perform common operations with ease.
- **Railway oriented programming** Utilizes the principle of railway oriented programming in order to communicate safely with the hardware. 

## Installation

To use this library in your project, you can install it via NuGet Package Manager:

```bash
nuget install TenmaPowerSupply
```

## Usage 

This is the first iteration of this chapter. It will be expanded later. For now note that this is a simple solutions, there are many ways to achieve the same goal, feel free to try out the functional toolchain this supports!

```VB.NET
Imports System.IO.Ports
Imports Tenma.Tenma.PowerSupply

Module Main
    Sub Main()
        ' Create a new power supply instance
        Dim powerSupply = New PowerSupply("COM4", 9600)

        ' Set the output voltage and current
        Dim setVoltageResult = powerSupply.SetOutputVoltage(12.5, Channels.One)
        Dim setCurrentResult = powerSupply.SetOutputCurrent(2.0, Channels.One)

        ' Check if setting voltage and current was successful
        If setVoltageResult.IsSuccess AndAlso setCurrentResult.IsSuccess Then
            Console.WriteLine("Voltage and current set successfully.")
        Else
            Console.WriteLine($"Failed to set voltage or current. Error: {setVoltageResult.ErrOr()} {setCurrentResult.ErrOr()}")
        End If

        ' Turn on the power supply
        Dim turnOnResult = powerSupply.TurnOn()
        If turnOnResult.IsSuccess Then
            Console.WriteLine("Power supply turned on.")
        Else
            Console.WriteLine($"Failed to turn on the power supply. Error: {turnOnResult.ErrOr()}")
        End If

        ' Perform other operations...

        ' Turn off the power supply
        Dim turnOffResult = powerSupply.TurnOff()
        If turnOffResult.IsSuccess Then
            Console.WriteLine("Power supply turned off.")
        Else
            Console.WriteLine($"Failed to turn off the power supply. Error: {turnOffResult.ErrOr()}")
        End If
    End Sub
End Module

```

## License
This project is licensed under the MIT License

## Reader
https://www.farnell.com/datasheets/3217055.pdf 