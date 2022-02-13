namespace FsLearn.Controllers



open Foundation
open SceneKit
open UIKit

type MainViewController() as self =
    inherit UIViewController()

    let mutable cameraNode: SCNNode = null

    override this.ViewDidLoad() =
        base.ViewDidLoad()

        let scene =
            SCNScene.FromFile(
                "woolly-mammoth-skeleton",
                "Models.scnassets",
                SCNSceneLoadingOptions(new NSDictionary())
            )

        cameraNode <- this.SetupCamera(scene)
        this.SetupLighting(scene)
        this.SetupSceneView(scene)

    member this.SetupCamera(scene: SCNScene) =
        let cameraNode = new SCNNode()
        cameraNode.Camera <- new SCNCamera()
        cameraNode.Position <- SCNVector3(float32 0, float32 0, float32 0)
        scene.RootNode.AddChildNode(cameraNode)
        cameraNode

    member this.SetupLighting(scene: SCNScene) =
        let lightNode = new SCNNode()
        lightNode.Light <- new SCNLight()
        lightNode.Light.LightType <- SCNLightType.Area
        lightNode.Position <- SCNVector3(float32 0, float32 0, float32 0)
        scene.RootNode.AddChildNode(lightNode)

        let ambientLightNode = new SCNNode()
        ambientLightNode.Light <- new SCNLight()
        ambientLightNode.Light.LightType <- SCNLightType.Ambient
        ambientLightNode.Light.Color <- UIColor.SystemBackgroundColor
        scene.RootNode.AddChildNode(ambientLightNode)

    member this.SetupSceneView(scene: SCNScene) =
        let sceneView = new SCNView(self.View.Bounds)
        sceneView.Scene <- scene
        sceneView.AllowsCameraControl <- true
        sceneView.ShowsStatistics <- true
        sceneView.BackgroundColor <- UIColor.Black

        let tapGesture = new UITapGestureRecognizer()
        sceneView.AddGestureRecognizer(tapGesture)
        self.View.AddSubview(sceneView)
