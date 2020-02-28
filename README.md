# Fable.ReactNative.DeviceInfo - Fable bindings for react-native-device-info

Provides Fable bindings for [react-native-device-info.](https://github.com/react-native-community/react-native-device-info) Some functions are only available for a specific OS, see the `react-native-device-info` documentation for further information. Promises and platform specific code is wrapped within promises returning a `Result<'a,string>`. You can also access all functions directly through `Globals.DeviceInfo` if you wish to do so.

## Setup

Install nuget package:
`paket add Fable.ReactNative.DeviceInfo`

Install npm module:
`yarn add react-native-device-info`

For ios:
`cd ios && pod install`

Note that some functions, like `getBatteryLevel` will require specific permissions and/or configurations. See the [react-native-device-info](https://github.com/react-native-community/react-native-device-info) documentation for further information.
