# Plugin.MvvmToolkit

This is a .NET library that provides a set of base classes and services to facilitate building MVVM (Model-View-ViewModel) based applications.

## Features

- BaseViewModel class with logging capabilities
- INavigationService to navigate between ViewModels

## Installation

You can install the library via NuGet Package Manager:

Install-Package Plugin.MvvmToolkit

## Usage

### BaseViewModel

To create a ViewModel, simply extend the BaseViewModel<TLogger> class, where TLogger is a logger instance that implements the ILogger interface.
The BaseViewModel class provides two virtual methods, OnAppearing and OnDisappearing, that are called when the ViewModel appears or disappears on the screen, respectively.

### INavigationService

To use the INavigationService, create an instance of the service and use its methods to navigate between ViewModels.
The AddRoute method is used to register a route with a ViewModel, and the NavigateAsync and NavigateBackAsync methods are used to navigate between ViewModels.

## License

This library is licensed under the MIT License. See the LICENSE file for details.
