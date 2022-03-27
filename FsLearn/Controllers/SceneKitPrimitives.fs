namespace FsLearn.Controllers

open System.Runtime.InteropServices
open SceneKit
open UIKit

type PrimitivesScene() as self =
    inherit SCNScene()

    let plane =
        let plane = SCNPlane.Create(NFloat 1., NFloat 1.)
        plane.FirstMaterial.Diffuse.Contents <- UIColor.Blue
        let planeNode = SCNNode.FromGeometry(plane)
        planeNode

    let sphere =
        let sphere = SCNSphere.Create(NFloat 1.)
        sphere.FirstMaterial.Diffuse.Contents <- UIColor.Red
        let sphereNode = SCNNode.FromGeometry(sphere)
        sphereNode.Position <- SCNVector3(float32 0., float32 3., float32 0.)
        sphereNode

    let box =
        let box =
            SCNBox.Create(NFloat 1., NFloat 1., NFloat 1., NFloat 0.2)

        box.FirstMaterial.Diffuse.Contents <- UIColor.Green
        let cubeNode = SCNNode.FromGeometry(box)
        cubeNode.Position <- SCNVector3(float32 0., float32 -3., float32 0.)
        cubeNode

    let cylinder =
        let cylinder = SCNCylinder.Create(NFloat 1., NFloat 1.)
        cylinder.FirstMaterial.Diffuse.Contents <- UIColor.Yellow
        let cylinderNode = SCNNode.FromGeometry(cylinder)
        cylinderNode.Position <- SCNVector3(float32 -3., float32 3., float32 0.)
        cylinderNode

    let torus =
        let torus = SCNTorus.Create(NFloat 1., NFloat 0.3)
        torus.FirstMaterial.Diffuse.Contents <- UIColor.White
        let torusNode = SCNNode.FromGeometry(torus)
        torusNode.Position <- SCNVector3(float32 -3., float32 0., float32 0.)
        torusNode

    let capsule =
        let capsule = SCNCapsule.Create(NFloat 0.3, NFloat 1.)
        capsule.FirstMaterial.Diffuse.Contents <- UIColor.Gray
        let capsuleNode = SCNNode.FromGeometry(capsule)
        capsuleNode.Position <- SCNVector3(float32 -3., float32 -3., float32 0.)
        capsuleNode

    let cone =
        let cone =
            SCNCone.Create(NFloat 1., NFloat 2., NFloat 1.)

        cone.FirstMaterial.Diffuse.Contents <- UIColor.Magenta
        let coneNode = SCNNode.FromGeometry(cone)
        coneNode.Position <- SCNVector3(float32 3., float32 -2., float32 0.)
        coneNode

    let tube =
        let tube =
            SCNTube.Create(NFloat 1., NFloat 2., NFloat 1.)

        tube.FirstMaterial.Diffuse.Contents <- UIColor.Brown
        let tubeNode = SCNNode.FromGeometry(tube)
        tubeNode.Position <- SCNVector3(float32 3., float32 2., float32 0.)
        tubeNode

    do self.RootNode.AddChildNode(plane)
    do self.RootNode.AddChildNode(sphere)
    do self.RootNode.AddChildNode(box)
    do self.RootNode.AddChildNode(cylinder)
    do self.RootNode.AddChildNode(torus)
    do self.RootNode.AddChildNode(capsule)
    do self.RootNode.AddChildNode(cone)
    do self.RootNode.AddChildNode(tube)

type SceneKitPrimitives() as self =
    inherit UIViewController()

    override this.ViewDidLoad() =
        base.ViewDidLoad()
        let sceneView = new SCNView(self.View.Bounds)

        let scene = new PrimitivesScene()
        sceneView.Scene <- scene
        sceneView.AllowsCameraControl <- true
        sceneView.BackgroundColor <- UIColor.Black
        sceneView.AutoenablesDefaultLighting <- true
        self.View.AddSubview(sceneView)
        sceneView.TranslatesAutoresizingMaskIntoConstraints <- false

        NSLayoutConstraint.ActivateConstraints(
            [| sceneView.TopAnchor.ConstraintEqualTo(self.View.TopAnchor, NFloat 0.)
               sceneView.LeadingAnchor.ConstraintEqualTo(self.View.LeadingAnchor, NFloat 0.)
               sceneView.TrailingAnchor.ConstraintEqualTo(self.View.TrailingAnchor, NFloat 0.)
               sceneView.BottomAnchor.ConstraintEqualTo(self.View.BottomAnchor, NFloat 0.) |]
        )
