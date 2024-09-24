# Vapolia.StrokedLabel

[![NuGet][nuget-img]][nuget-link]  
![Nuget](https://img.shields.io/nuget/dt/Vapolia.StrokedLabel)  
[![Publish To Nuget](https://github.com/vapolia/StrokedLabel/actions/workflows/main.yaml/badge.svg)](https://github.com/vapolia/StrokedLabel/actions/workflows/main.yaml)

```cs
dotnet add package Vapolia.StrokedLabel

builder.UseStrokedLabelBehavior();
```

[nuget-link]: https://www.nuget.org/packages/Vapolia.StrokedLabel/
[nuget-img]: https://img.shields.io/nuget/v/Vapolia.StrokedLabel

Platforms:
- Android
- iOS

# Quick start

Add the above nuget package to your Maui project   
then add this line to your maui app builder:

```c#
using Vapolia.StrokedLabels;
...
builder.UseStrokedLabelBehavior();
```

# Examples

See the SampleApp in this repo.

Declare the namespace:
```xaml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
         ...
         xmlns:segmented="https://vapolia.eu/Vapolia.StrokedLabel">
```

Apply the behavior to a label:
```xaml
<Label Text="I do really like it!"
       FontSize="32"
       TextColor="LightBlue"
       stroked:StrokedLabel.StrokeColor="DarkBlue"
       stroked:StrokedLabel.StrokeWidth="4"  />
```

Apply the behavior through a style:
```xaml
<StackLayout>
    <StackLayout.Resources>
        <Style TargetType="Label">
            <Setter Property="TextColor" Value="LightBlue" />
            <Setter Property="stroked:StrokedLabel.StrokeColor" Value="DarkBlue" />
            <Setter Property="stroked:StrokedLabel.StrokeWidth" Value="4" />
        </Style>
    </StackLayout.Resources>

    <Label FontSize="32" Text="I like it so much!" />
</StackLayout>
```
