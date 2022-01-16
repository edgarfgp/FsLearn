namespace Net6iOSTemplate

open System

open UIKit
open Foundation

[<Register(nameof AppDelegate)>]
type AppDelegate() =
    inherit UIApplicationDelegate()
       
    override val Window = null with get, set

    override this.FinishedLaunching(_, _) =
        this.Window <- new UIWindow(UIScreen.MainScreen.Bounds)
        let vc = new UIViewController()
        vc.View.BackgroundColor <- UIColor.Red
        this.Window.RootViewController <- vc
        this.Window.MakeKeyAndVisible()
        true
        
       
