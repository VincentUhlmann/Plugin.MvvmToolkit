# Plugin.MvvmToolkit.Maui

This is an implementation of the Plugin.MvvmToolkit library for .NET MAUI (Multi-platform App UI) applications.

## Features

- Base ContentPage class
- Implementation of NavigationService for .NET MAUI AppShell

## Installation

You can install the library via NuGet Package Manager:

Install-Package Plugin.MvvmToolkit.Maui

## Usage

### BaseContentPage

To create a ContentPage, simply extend the BaseContentPage<T> class, where T is the ViewModel type that extends the BaseViewModel<ILogger<T>> class.
The BaseContentPage class automatically sets the BindingContext to the ViewModel instance, and executes the ViewModel.AppearingCommand and ViewModel.DisappearingCommand methods when the page appears and disappears on the screen, respectively.
Additionally, you can use the On<iOS>().SetUseSafeArea(true) method to enable safe area insets on iOS devices.

### NavigationService

To use the NavigationService, create an instance of the service and use its methods to navigate between ViewModels.
The AddRoute method is used to register a route with a ViewModel, and the NavigateAsync and NavigateBackAsync methods are used to navigate between ViewModels.

## License

This library is licensed under the MIT License. See the LICENSE file for details.
