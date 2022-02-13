namespace FsLearn

open FsLearn.Controllers
open UIKit
open Foundation

[<Register(nameof SceneDelegate)>]
type SceneDelegate() =
    inherit UIWindowSceneDelegate()

    override val Window = null with get, set

    override this.WillConnect(scene: UIScene, session: UISceneSession, connectionOptions: UISceneConnectionOptions) =

        let scene = scene :?> UIWindowScene

        let win =
            new UIWindow(scene.CoordinateSpace.Bounds)

        win.RootViewController <- new MainViewController()
        this.Window <- win
        win.WindowScene <- scene
        win.MakeKeyAndVisible()

    override _.DidDisconnect(scene: UIScene) = ()

    // Called as the scene is being released by the system.
    // This occurs shortly after the scene enters the background, or when its session is discarded.
    // Release any resources associated with this scene that can be re-created the next time the scene connects.
    // The scene may re-connect later, as its session was not necessarily discarded (see UIApplicationDelegate `DidDiscardSceneSessions` instead).
    override _.DidBecomeActive(scene: UIScene) = ()
    // Called when the scene has moved from an inactive state to an active state.
    // Use this method to restart any tasks that were paused (or not yet started) when the scene was inactive.

    override _.WillResignActive(scene: UIScene) = ()
    // Called when the scene will move from an active state to an inactive state.
    // This may occur due to temporary interruptions (ex. an incoming phone call).

    override _.WillEnterForeground(scene: UIScene) = ()
    // Called as the scene transitions from the background to the foreground.
    // Use this method to undo the changes made on entering the background.

    override _.DidEnterBackground(scene: UIScene) = ()
// Called as the scene transitions from the foreground to the background.
// Use this method to save data, release shared resources, and store enough scene-specific state information
// to restore the scene back to its current state.
