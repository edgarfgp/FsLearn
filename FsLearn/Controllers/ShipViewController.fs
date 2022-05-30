namespace FsLearn.Controllers

open Foundation
open SceneKit
open UIKit

type ShipViewController() as self =
    inherit UIViewController()

    let mutable sceneView = null

    override this.ViewDidLoad() =
        base.ViewDidLoad()

        // Create a scene
        let scene =
            SCNScene.FromFile("ship.scn", "Models.scnassets", SCNSceneLoadingOptions())

        // Create a camera and add it to the scene
        let cameraNode = new SCNNode()
        cameraNode.Camera <- new SCNCamera()
        scene.RootNode.AddChildNode(cameraNode)

        // place the camera
        cameraNode.Position <- SCNVector3(float32 0, float32 0, float32 15)

                // retrieve the ship node
        let _ =
            scene.RootNode.FindChildNode("ship", true)

        // animate the 3d object
        //ship.RunAction(SCNAction.RepeatActionForever(SCNAction.RotateBy(nfloat 0, nfloat 2, nfloat 0, 1.)))

        // create and add a light to the scene
        let lightNode = new SCNNode()
        lightNode.Light <- new SCNLight()
        lightNode.CastsShadow <- true
        lightNode.Light.LightType <- SCNLightType.Spot
        lightNode.Position <- SCNVector3(float32 4, float32 7, float32 6)
        scene.RootNode.AddChildNode(lightNode)
        // retrieve the SCNView
        sceneView <- new SCNView(self.View.Bounds)
        // set the scene to the view
        sceneView.Scene <- scene

        // allows the user to manipulate the camera
        sceneView.AllowsCameraControl <- true

        // show statistics such as fps and timing information
        sceneView.ShowsStatistics <- true

        // configure the view
        sceneView.BackgroundColor <- UIColor.Black

        // add a tap gesture recognizer
        let tapGesture =
            new UITapGestureRecognizer(fun ges -> self.HandleTap(ges))

        sceneView.AddGestureRecognizer(tapGesture)

        self.View.AddSubview(sceneView)

    member this.HandleTap(tapGesture: UIGestureRecognizer) =
        // Check what nodes are tapped
        let point = tapGesture.LocationInView(sceneView)
        let hitResults = sceneView.HitTest(point, new NSDictionary())

        // check that we clicked on at least one object
        if hitResults.Length > 0 then
            // retrieved the first clicked object
            let result = hitResults.[0]
            // get its material
            let material = result.Node.Geometry.FirstMaterial

            // highlight it
            SCNTransaction.Begin()
            SCNTransaction.AnimationDuration <- 0.5

            // on completion - unhighlight
            SCNTransaction.SetCompletionBlock(fun () ->
                SCNTransaction.Begin()
                SCNTransaction.AnimationDuration <- 0.5

                material.Emission.ContentColor <- UIColor.Black

                SCNTransaction.Commit()
            )

            // interpolate between emissive color values
            material.Emission.ContentColor <- UIColor.Red

            SCNTransaction.Commit()
