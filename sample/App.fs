module App

open Elmish
open Elmish.React
open Elmish.ReactNative
open Fable.ReactNative
open Fable.ReactNative.DeviceInfo

type Model = {
    LocationProviders   : AvailableLocationProviders option
    BuildId             : string option
    FontScale           : float option
    Manufacturer        : string option
    PowerState          : PowerState option
    UserAgent           : string option
    AirplaneMode        : bool option
    Emulator            : bool option
    Landscape           : bool option
}

type Message =
    | LP                of Result<AvailableLocationProviders, string>
    | BuildId           of Result<string, string>
    | FontScale         of Result<float, string>
    | Manufacturer      of Result<string, string>
    | Power             of Result<PowerState, string>
    | Agent             of Result<string, string>
    | InAirplaneMode    of Result<bool, string>
    | InEmulator        of Result<bool, string>
    | InLandscape       of Result<bool, string>

let init () = 
    let mdl = {
        LocationProviders   = None
        BuildId             = None
        FontScale           = None
        Manufacturer        = None
        PowerState          = None
        UserAgent           = None
        AirplaneMode        = None
        Emulator            = None
        Landscape           = None
    }

    let cmds = Cmd.batch [
        Cmd.OfPromise.perform getAvailableLocationProviders () LP
        Cmd.OfPromise.perform getBuildId () BuildId
        Cmd.OfPromise.perform getFontScale () FontScale
        Cmd.OfPromise.perform getManufacturer () Manufacturer
        Cmd.OfPromise.perform getPowerState () Power
        Cmd.OfPromise.perform getUserAgent () Agent
        Cmd.OfPromise.perform isAirplaneMode () InAirplaneMode
        Cmd.OfPromise.perform isEmulator () InEmulator
        Cmd.OfPromise.perform isLandscape () InLandscape
    ]
    
    mdl, cmds

let resultToOption ( res : Result<'a, string> ) : 'a option = 
    match res with 
    | Result.Ok x -> Some x
    | Result.Error _ -> None

let update msg model =
    match msg with
    | LP x              -> { model with LocationProviders = resultToOption x }, Cmd.none
    | BuildId x         -> { model with BuildId = resultToOption x }, Cmd.none
    | FontScale x       -> { model with FontScale = resultToOption x }, Cmd.none
    | Manufacturer x    -> { model with Manufacturer = resultToOption x }, Cmd.none
    | Power x           -> { model with PowerState = resultToOption x }, Cmd.none
    | Agent x           -> { model with UserAgent = resultToOption x }, Cmd.none
    | InAirplaneMode x  -> { model with AirplaneMode = resultToOption x }, Cmd.none
    | InEmulator x      -> { model with Emulator = resultToOption x }, Cmd.none
    | InLandscape x     -> { model with Landscape = resultToOption x }, Cmd.none

module R = Fable.ReactNative.Helpers
module P = Fable.ReactNative.Props
open Fable.ReactNative.Props

let entry txt = 
    R.text [
        P.TextProperties.Style [
            P.FontSize 14.
            P.FlexStyle.PaddingTop ( R.pct 2. )
        ]
    ] txt

let stringOption txt ( x : string option ) =
    match x with 
    | Some y -> entry ( sprintf "%s: %s" txt y )
    | None -> entry ( sprintf "%s: Unavailable" txt ) 

let objOption txt ( x : obj option ) =
    match x with 
    | Some y -> entry ( sprintf "%s: %s" txt ( string y ) )
    | None -> entry ( sprintf "%s: Unavailable" txt ) 

let floatOption txt ( x : float option ) = 
    match x with 
    | Some y -> entry ( sprintf "%s: %s" txt ( y.ToString "0.00" ) )
    | None -> entry ( sprintf "%s: Unavailable" txt ) 

let intOption txt ( x : int option ) = 
    match x with 
    | Some y -> entry ( sprintf "%s: %i" txt y )
    | None -> entry ( sprintf "%s: Unavailable" txt ) 

let boolOption txt ( x : bool option ) = 
    match x with 
    | Some y -> entry ( sprintf "%s: %b" txt y )
    | None -> entry ( sprintf "%s: Unavailable" txt ) 


let toLocationProviderType ( x : AvailableLocationProviders option ) = 
    match x with 
    | None -> "Unavailable"
    | Some y -> 
        match y with 
        | IOS _ -> "iOS"
        | _ -> "Android"

let printLocationProvider ( x : AvailableLocationProviders option ) = 
    match x with 
    | None -> "Unavailable"
    | Some y -> 
        match y with
        | IOS z -> sprintf "%A" z
        | Android z -> sprintf "%A" z

let view model dispatch =
    R.view [
        P.ViewProperties.Style [
            P.FlexStyle.Flex 1.
            P.FlexStyle.Padding ( R.pct 2. )

        ]
    ] [
        R.view [
            P.ViewProperties.Style [
                P.FlexStyle.Flex 0.1
            ]
        ] []

        R.scrollView [
            P.ViewProperties.Style [ 
                P.FlexStyle.Flex 0.9
            ]
        ] [
            
           
            R.text [
                P.TextProperties.Style [
                    P.FontSize 20.
                    P.FontWeight FontWeight.Bold
                ]
            ] "Fable.ReactNative.DeviceInfo Sample"

            entry ( sprintf "Location Provider type: %s" ( toLocationProviderType model.LocationProviders ))

            entry ( sprintf "Location Providers: %s" ( printLocationProvider model.LocationProviders ) )

            entry ( sprintf "App name: %s" ( getApplicationName() ) )

            stringOption "Build ID" model.BuildId
            
            entry ( sprintf "BuildNr: %s" ( getBuildNumber() ) )

            entry ( sprintf "Brand: %s" ( getBrand() ) )
        
            entry ( sprintf "BundleID: %s" ( getBundleId() ) )

            entry ( sprintf "Device ID: %s" ( getDeviceId() ) )
            
            entry ( sprintf "Device type: %s" ( getDeviceType() ) )


            floatOption "Font scale" model.FontScale
        
            entry ( sprintf "System name: %s" ( getSystemName () ) )

            entry ( sprintf "Version: %s" ( getVersion () ) )

            stringOption "Manufacturer" model.Manufacturer
        
            entry ( sprintf "Model: %s" ( getModel() ) )

            entry ( sprintf "Power state: %s" ( string model.PowerState ) )

            entry ( sprintf "Unique ID: %s" ( getUniqueId () ) )
        
            stringOption "User agent" model.UserAgent

            entry ( sprintf "Has notch: %b" ( hasNotch() ) )

            boolOption "Airplane mode" model.AirplaneMode

            boolOption "Emulator" model.Emulator

            boolOption "Landscape mode" model.Landscape

            entry ( sprintf "Tablet: %b" ( isTablet() ) )
            R.view [
                P.ViewProperties.Style [
                    P.FlexStyle.Padding ( R.pct 10. )
                ]
            ] []
        ]
    ]

Program.mkProgram init update view
|> Program.withConsoleTrace
|> Program.withReactNative "debug" // CHANGE ME
|> Program.run