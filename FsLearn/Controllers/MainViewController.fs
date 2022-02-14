namespace FsLearn.Controllers



open Foundation
open SceneKit
open UIKit

type MainViewController() as self =
    inherit UIViewController()

    override this.ViewDidLoad() =
        base.ViewDidLoad()

        let scene =
            SCNScene.FromFile("woolly-mammoth-skeleton", "Models.scnassets", SCNSceneLoadingOptions())

        this.SetupCamera(scene)
        this.SetupLighting(scene)

        let sceneView = this.SetupSceneView(scene)
        self.View.AddSubview(sceneView)

    member this.SetupCamera(scene: SCNScene) =
        let cameraNode = new SCNNode()
        cameraNode.Camera <- new SCNCamera()
        cameraNode.Camera.UsesOrthographicProjection <- true
        cameraNode.Camera.AutomaticallyAdjustsZRange <- true
        scene.RootNode.AddChildNode(cameraNode)

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
        sceneView
