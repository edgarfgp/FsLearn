namespace Net6iOSTemplate


open Net6iOSTemplate.Controllers
open UIKit
open Foundation

[<Register(nameof AppDelegate)>]
type AppDelegate() =
    inherit UIResponder()

    interface IUIApplicationDelegate

    [<Export("application:didFinishLaunchingWithOptions:")>]
    member this.FinishedLaunching(application: UIApplication, launchOptions: NSDictionary) : bool = true

    [<Export("application:configurationForConnectingSceneSession:options:")>]
    member this.GetConfiguration
        (
            application: UIApplication,
            sceneSession: UISceneSession,
            options: UISceneConnectionOptions
        ) =
        UISceneConfiguration.Create("Default Configuration", sceneSession.Role)
