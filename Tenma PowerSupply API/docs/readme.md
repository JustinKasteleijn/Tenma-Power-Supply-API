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

### Functional 

```VB.NET
    ' Constants
    Const outputCurrent As Decimal = 2.222
    Const outputVoltage As Decimal = 22.22
    Const channel As Channels = Channels.Channel1

    ' Create and initialize an instance of TenmaPowerSupply
    Dim tenma = TenmaPowerSupply.Create("72-2550", "COM4")

    ' Functional chain to set up and control the power supply
    Dim result = tenma.
        AndThen(Function(t) t.SetOutputCurrent(outputCurrent, channel)).
        AndThen(Function(t) t.SetOutputVoltage(outputVoltage, channel)).
        AndThen(Function(t) t.TurnOn()).
        Wait(2000).
        AndThen(Function(t) t.GetActualOutputVoltage(channel)).
        Assert(
            Function(res) res.result = outputVoltage,
            Function(res) $"Output voltage of {res.result} did not match actual voltage of {outputVoltage}"
        ).
        AndThen(Function(t) t.TurnOff())

    ' The 'result' variable now holds the final result of the functional chain

```

### Imperative

```VB.NET
    ' Constants
    Const outputCurrent As Decimal = 2.222
    Const outputVoltage As Decimal = 22.22
    Const channel As Channels = Channels.Channel1

    ' Create and initialize an instance of TenmaPowerSupply
    Dim tenma = TenmaPowerSupply.Create("72-2550", "COM4")

    ' Set output current
    Dim setCurrentResult = tenma.SetOutputCurrent(outputCurrent, channel)
    If setCurrentResult.IsErr Then
        Console.WriteLine($"Failed to set output current: {setCurrentResult.Err}")
        Return
    End If

    ' Set output voltage
    Dim setVoltageResult = setCurrentResult.Value.tenmaPowerSupply.SetOutputVoltage(outputVoltage, channel)
    If setVoltageResult.IsErr Then
        Console.WriteLine($"Failed to set output voltage: {setVoltageResult.Err}")
        Return
    End If

    ' Turn on power supply
    Dim turnOnResult = setVoltageResult.Value.tenmaPowerSupply.TurnOn()
    If turnOnResult.IsErr Then
        Console.WriteLine($"Failed to turn on power supply: {turnOnResult.Err}")
        Return
    End If

    ' Wait for 2 seconds
    Thread.Sleep(2000)

    ' Get actual output voltage
    Dim getVoltageResult = turnOnResult.Value.tenmaPowerSupply.GetActualOutputVoltage(channel)
    If getVoltageResult.IsErr Then
        Console.WriteLine($"Failed to get actual output voltage: {getVoltageResult.Err}")
        Return
    End If

    ' Check if actual voltage matches expected voltage
    If getVoltageResult.Value.result = outputVoltage Then
        Console.WriteLine($"Output voltage matched expected voltage of {outputVoltage}")
    Else
        Console.WriteLine($"Output voltage did not match expected voltage of {outputVoltage}")
    End If

    ' Turn off power supply
    Dim turnOffResult = getVoltageResult.Value.tenmaPowerSupply.TurnOff()
    If turnOffResult.IsErr Then
        Console.WriteLine($"Failed to turn off power supply: {turnOffResult.Err}")
        Return
    End If
```

## License
This project is licensed under the MIT License

## Reader
https://www.farnell.com/datasheets/3217055.pdf 
