module Fable.ReactNative.DeviceInfo

open Fable.Core
open Fable.Core.JsInterop

type AvailableLocationProvidersAndroid = {
    GPS : bool 
    Network : bool 
    Passive : bool
}

type AvailableLocationProvidersIOS = {
    HeadingAvailable : bool 
    RangingAvailable : bool 
    LocationServicesEnabled : bool
    SignificantLocationChangeMonitoringAvailable : bool
}

type AvailableLocationProviders =
    | IOS of AvailableLocationProvidersIOS 
    | Android of AvailableLocationProvidersAndroid

type PowerState  = {
    BatteryLevel: float
    BatteryState: string
    LowPowerMode: bool
}

type DeviceInfo =
    abstract member getAndroidId : unit -> JS.Promise<string>
    abstract member getApiLevel : unit -> JS.Promise<float>
    abstract member getApplicationName : unit -> string
    abstract member getAvailableLocationProviders : unit -> JS.Promise<obj>
    abstract member getBaseOS : unit -> JS.Promise<string>
    abstract member getBatteryLevel : unit -> JS.Promise<float>
    abstract member getBootloader : unit -> JS.Promise<string>
    abstract member getBrand : unit -> string
    abstract member getBuildId : unit -> JS.Promise<string>
    abstract member getBuildNumber : unit -> string
    abstract member getBundleId : unit -> string
    abstract member getCarrier : unit -> JS.Promise<string>
    abstract member getCodename : unit -> JS.Promise<string>
    abstract member getDevice : unit -> JS.Promise<string>
    abstract member getDeviceId : unit -> string
    abstract member getDeviceType : unit -> string
    abstract member getDisplay : unit -> JS.Promise<string>
    abstract member getDeviceName : unit -> JS.Promise<string>
    abstract member getDeviceToken : unit -> JS.Promise<string>
    abstract member getFirstInstallTime : unit -> JS.Promise<int>
    abstract member getFingerprint : unit -> JS.Promise<string>
    abstract member getFontScale : unit -> JS.Promise<float>
    abstract member getFreeDiskStorage : unit -> JS.Promise<int>
    abstract member getHardware : unit -> JS.Promise<string>
    abstract member getHost : unit -> JS.Promise<string>
    abstract member getIpAddress : unit -> JS.Promise<string>
    abstract member getIncremental : unit -> JS.Promise<string>
    abstract member getInstallerPackageName : unit -> JS.Promise<string>
    abstract member getInstallReferrer : unit -> JS.Promise<string>
    abstract member getInstanceId : unit -> JS.Promise<string>
    abstract member getLastUpdateTime : unit -> JS.Promise<int>
    abstract member getMacAddress : unit -> JS.Promise<string>
    abstract member getManufacturer : unit -> JS.Promise<string>
    abstract member getMaxMemory : unit -> JS.Promise<int>
    abstract member getModel : unit -> string
    abstract member getPhoneNumber : unit -> JS.Promise<string>
    abstract member getPowerState : unit -> JS.Promise<obj>
    abstract member getProduct : unit -> JS.Promise<string>
    abstract member getPreviewSdkInt : unit -> JS.Promise<int>
    abstract member getReadableVersion : unit -> string
    abstract member getSerialNumber : unit -> JS.Promise<string>
    abstract member getSecurityPatch : unit -> JS.Promise<string>
    abstract member getSystemAvailableFeatures : unit -> JS.Promise<string []>
    abstract member getSystemName : unit -> string 
    abstract member getSystemVersion : unit -> string
    abstract member getTags : unit -> JS.Promise<string>
    abstract member getType : unit -> JS.Promise<string>
    abstract member getTotalDiskCapacity : unit -> JS.Promise<int>
    abstract member getTotalMemory : unit -> JS.Promise<int>
    abstract member getUniqueId : unit -> string
    abstract member getUsedMemory : unit -> JS.Promise<int>
    abstract member getUserAgent : unit -> JS.Promise<string>
    abstract member getVersion : unit -> string
    abstract member hasNotch : unit -> bool
    abstract member hasSystemFeature : string -> JS.Promise<bool>
    abstract member isAirplaneMode : unit -> JS.Promise<bool>
    abstract member isBatteryCharging : unit -> JS.Promise<bool>
    abstract member isCameraPresent : unit -> JS.Promise<bool>
    abstract member isEmulator : unit -> JS.Promise<bool>
    abstract member isLandscape : unit -> JS.Promise<bool>
    abstract member isLocationEnabled : unit -> JS.Promise<bool>
    abstract member isHeadphonesConnected : unit -> JS.Promise<bool>
    abstract member isPinOrFingerprintSet : unit -> JS.Promise<bool>
    abstract member isTablet : unit -> bool
    abstract member supported32BitAbis : unit -> JS.Promise<string []>
    abstract member supported64BitAbis : unit -> JS.Promise<string []>
    abstract member supportedAbis : unit -> JS.Promise<string []>




type Globals =
    [<Import("default", from="react-native-device-info")>]
    static member DeviceInfo with get(): DeviceInfo = failwith "JS only" and set(v: DeviceInfo): unit = failwith "JS only"

// Android ID (Android)
let getAndroidId () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getAndroidId()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Api Level (Android)
let getApiLevel () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getApiLevel()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Application name
let getApplicationName () : string = 
    Globals.DeviceInfo.getApplicationName()

// Available location providers
let getAvailableLocationProviders () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getAvailableLocationProviders()
            let device = Globals.DeviceInfo.getSystemName()
            let alp = 
                match device.ToLower() with
                | "ios" ->
                    let lp = {
                        HeadingAvailable = x?headingAvailable
                        RangingAvailable = x?isRangingAvailable
                        LocationServicesEnabled = x?locationServicesEnabled
                        SignificantLocationChangeMonitoringAvailable = x?significantLocationChangeMonitoringAvailable
                    }
                    AvailableLocationProviders.IOS lp
                | _ ->
                    let lp = {
                        GPS = x?gps
                        Network = x?network
                        Passive = x?passive
                    }
                    AvailableLocationProviders.Android lp
            
            return Result.Ok alp
        with 
        | e -> return Result.Error e.Message
    }

// Base OS (Android)
let getBaseOS () = 
    promise {
        try
            let! x = Globals.DeviceInfo.getBaseOS()
            return Result.Ok x
        with 
        | e -> return Result.Error e.Message
    }

// Build ID
let getBuildId () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getBuildId()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Battery level
let getBatteryLevel () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getBatteryLevel()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Bootloader
let getBootloader () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getBootloader()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Brand 
let getBrand () = 
    Globals.DeviceInfo.getBrand()

// Build number
let getBuildNumber () : string = 
    Globals.DeviceInfo.getBuildNumber()

//  Bundle ID
let getBundleId () = 
    Globals.DeviceInfo.getBundleId ()

// Carrier
let getCarrier () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getCarrier()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Codename (Android)
let getCodename () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getCodename()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Device (Android)
let getDevice () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getDevice()
            return Result.Ok x
        with | e -> return Result.Error e.Message 
    }

// Device Id 
let getDeviceId () = 
    Globals.DeviceInfo.getDeviceId()

// Device type
let getDeviceType () = 
    Globals.DeviceInfo.getDeviceType()
    
// Display (Android)
let getDisplay () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getDisplay()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Device name
let getDeviceName () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getDeviceName()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Device token (iOS)
let getDeviceToken () = 
    promise {
        try
            let! x = Globals.DeviceInfo.getDeviceToken()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// First install time (Android)
let getFirstInstallTime () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getFirstInstallTime()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Fingerprint (Android)
let getFingerprint () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getFingerprint()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Font scale
let getFontScale () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getFontScale()
            return Result.Ok x
        with 
        | e -> return Result.Error e.Message
    }

// Free disk storage
let getFreeDiskStorage () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getFreeDiskStorage()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Hardware (Android)
let getHardware () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getHardware()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Host (Android)
let getHost () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getHost()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// IP
let getIpAddress () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getIpAddress()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Incremental (Android)
let getIncremental () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getIncremental()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Installer Package Name(Android)
let getInstallerPackageName () =  
    promise {
        try 
            let! x = Globals.DeviceInfo.getInstallerPackageName()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Install Referrer (Android)
let getInstallReferrer () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getInstallReferrer()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Instance Id (Android)
let getInstanceId () = 
    promise {
        try
            let! x = Globals.DeviceInfo.getInstanceId()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Last update time (Android)
let getLastUpdateTime () = 
    promise {
        try
            let! x = Globals.DeviceInfo.getLastUpdateTime()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// MAC address
let getMacAddress () = 
    promise {
        try
            let! x = Globals.DeviceInfo.getMacAddress()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Manufacturer
let getManufacturer () =
    promise {
        try
            let! x = Globals.DeviceInfo.getManufacturer()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Max memory (Android)
let getMaxMemory () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getMaxMemory()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Model 
let getModel () = 
    Globals.DeviceInfo.getModel()

// Phonenumber (Android)
let getPhoneNumber() = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getPhoneNumber()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Powerstate
let getPowerState () =
    promise {
        try 
            let! x = Globals.DeviceInfo.getPowerState()
            let powerState = {
                BatteryState = x?batteryState
                BatteryLevel = float x?batteryLevel
                LowPowerMode = x?lowPowerMode
            }
            return Result.Ok powerState
        with | e -> return Result.Error e.Message
    }

// Product (Android)
let getProduct () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getProduct()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Preview SDK (Android)
let getPreviewSdkInt() = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getPreviewSdkInt()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Readable version
let getReadableVersion () = 
    Globals.DeviceInfo.getReadableVersion()


// Serialnumber (Android)
let getSerialNumber () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getSerialNumber()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Security patch (Android)
let getSecurityPatch () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getSecurityPatch()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// System available features (Android)
let getSystemAvailableFeatures () = 
    promise {
        try
            let! x = Globals.DeviceInfo.getSystemAvailableFeatures()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// System name
let getSystemName () : string = 
    Globals.DeviceInfo.getSystemName()

// System verison
let getSystemVersion () = 
    Globals.DeviceInfo.getSystemVersion()

// Tags (Android)
let getTags () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getTags()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Type (Android)
let getType () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getType()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Total disk capacity
let getTotalDiskCapacity () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getTotalDiskCapacity()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Total memory
let getTotalMemory () = 
    promise {
        try
            let! x = Globals.DeviceInfo.getTotalMemory()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Used memory
let getUsedMemory () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getUsedMemory()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// User agent
let getUserAgent () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.getUserAgent()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Unique Id
let getUniqueId () = 
    Globals.DeviceInfo.getUniqueId()

// version
let getVersion () : string = 
    Globals.DeviceInfo.getVersion()

// Notch
let hasNotch () = 
    Globals.DeviceInfo.hasNotch()

// Check for system feature (Android)
let hasSystemFeature ( feature : string ) = 
    promise {
        try 
            let! x = Globals.DeviceInfo.hasSystemFeature feature
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// in airplane mode
let isAirplaneMode () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.isAirplaneMode()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Is battery charging
let isBatteryCharging () = 
    promise {
        try
            let! x = Globals.DeviceInfo.isBatteryCharging()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Is running in emulator
let isEmulator () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.isEmulator()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// Is device in landscape mode
let isLandscape () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.isLandscape()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// location services enabled
let isLocationEnabled () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.isLocationEnabled()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// headphones connected
let isHeadphonesConnected () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.isHeadphonesConnected()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }


// pin or fingerprint set
let isPinOrFingerprintSet () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.isPinOrFingerprintSet()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

// tablet
let isTablet () = 
    Globals.DeviceInfo.isTablet()

// supported 32 bit abis (Android)
let supported32BitAbis () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.supported32BitAbis()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

let supported64BitAbis () = 
    promise {
        try 
            let! x = Globals.DeviceInfo.supported64BitAbis()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }

let supportedAbis () = 
    promise {
        try
            let! x = Globals.DeviceInfo.supportedAbis()
            return Result.Ok x
        with | e -> return Result.Error e.Message
    }