namespace Net6iOSTemplate.Controllers

open System
open CoreGraphics
open Net6iOSTemplate.Views
open ObjCRuntime
open UIKit
open Net6iOSTemplate

type OnboardingController() as self =
    inherit UIViewController()

    let imageView =
        lazy
            (let imageview = new UIImageView()
             imageview.Image <- UIImage.FromBundle("image_onboarding")
             imageview.ContentMode <- UIViewContentMode.ScaleAspectFit
             imageview.TranslatesAutoresizingMaskIntoConstraints <- false
             imageview)

    let containerVew =
        lazy
            (let containerView = new UIView()
             containerView.TranslatesAutoresizingMaskIntoConstraints <- false
             containerView)

    let carrouselView =
        lazy
            (let carrouselView = new CarouselView()
             carrouselView.TranslatesAutoresizingMaskIntoConstraints <- false
             carrouselView)

    override this.ViewDidLoad() =
        base.ViewDidLoad()

        self.View.AddSubview(containerVew.Value)
        containerVew.Value.ConstraintToParent(self.View)
        containerVew.Value.AddSubview(imageView.Value)
        containerVew.Value.AddSubview(carrouselView.Value)

        self.View.BackgroundColor <- UIColor.SystemBackgroundColor

        NSLayoutConstraint.ActivateConstraints(
            [| imageView.Value.TopAnchor.ConstraintEqualTo(containerVew.Value.SafeAreaLayoutGuide.TopAnchor, nfloat 0.)
               imageView.Value.LeadingAnchor.ConstraintEqualTo(containerVew.Value.SafeAreaLayoutGuide.LeadingAnchor, nfloat 0.)
               imageView.Value.TrailingAnchor.ConstraintEqualTo(containerVew.Value.SafeAreaLayoutGuide.TrailingAnchor, nfloat 0.)
               imageView.Value.HeightAnchor.ConstraintEqualTo(nfloat 100.) |]
        )

        NSLayoutConstraint.ActivateConstraints(
            [| carrouselView.Value.TopAnchor.ConstraintEqualTo(imageView.Value.BottomAnchor, nfloat 16.)
               carrouselView.Value.LeadingAnchor.ConstraintEqualTo(containerVew.Value.SafeAreaLayoutGuide.LeadingAnchor, nfloat 0.)
               carrouselView.Value.TrailingAnchor.ConstraintEqualTo(containerVew.Value.SafeAreaLayoutGuide.TrailingAnchor, nfloat 0.)
               carrouselView.Value.BottomAnchor.ConstraintEqualTo(containerVew.Value.SafeAreaLayoutGuide.BottomAnchor, nfloat 0.)
            |]
        )

    override this.ViewDidAppear(animated)  =
        base.ViewDidAppear(animated)
