namespace Net6iOSTemplate

open System
open CoreFoundation
open CoreGraphics
open Foundation
open ObjCRuntime
open UIKit

[<AutoOpen>]
module Extensions =

    type UIImageView with

        member ui.RoundedCorners() =
            ui.Layer.CornerRadius <- nfloat 50.
            ui.ClipsToBounds <- true
            ui.Layer.BorderWidth <- nfloat 3.

            ui.Layer.BorderColor = UIColor.White.CGColor
            |> ignore

    type UIView with
        member uv.AddSubviews([<ParamArray>] views: UIView array) =
            views
            |> Array.map
                (fun view ->
                    view.TranslatesAutoresizingMaskIntoConstraints <- false
                    uv.AddSubview view)
            |> ignore

        member uv.ConstraintToParent
            (
                parentView: UIView,
                ?leading: float,
                ?top: float,
                ?trailing: float,
                ?bottom: float
            ) =
            let leading = defaultArg leading (float 0)
            let top = defaultArg top (float 0)
            let trailing = defaultArg trailing (float 0)
            let bottom = defaultArg bottom (float 0)

            NSLayoutConstraint.ActivateConstraints(
                [| uv.TopAnchor.ConstraintEqualTo(parentView.TopAnchor, nfloat top)
                   uv.LeadingAnchor.ConstraintEqualTo(parentView.LeadingAnchor, nfloat leading)
                   uv.TrailingAnchor.ConstraintEqualTo(parentView.TrailingAnchor, nfloat -trailing)
                   uv.BottomAnchor.ConstraintEqualTo(parentView.BottomAnchor, nfloat -bottom) |]
            )

        member uv.ConstraintToParent(parentView: UIView, all: nfloat) =
            NSLayoutConstraint.ActivateConstraints(
                [| uv.TopAnchor.ConstraintEqualTo(parentView.TopAnchor, all)
                   uv.LeadingAnchor.ConstraintEqualTo(parentView.LeadingAnchor, all)
                   uv.TrailingAnchor.ConstraintEqualTo(parentView.TrailingAnchor, -all)
                   uv.BottomAnchor.ConstraintEqualTo(parentView.BottomAnchor, -all) |]
            )

        member uv.BlurViewIfPossible() =
            let reduceBlur =
                UIAccessibility.IsReduceTransparencyEnabled

            let lowerPowerIsOn =
                NSProcessInfo.ProcessInfo.LowPowerModeEnabled

            if reduceBlur || lowerPowerIsOn then
                uv.BackgroundColor <- UIColor.SystemFillColor
            else
                let blur =
                    UIBlurEffect.FromStyle(UIBlurEffectStyle.SystemThinMaterial)

                let blurredEffectView = new UIVisualEffectView(blur)
                blurredEffectView.Frame <- uv.Bounds
                uv.AddSubviews(blurredEffectView)

                DispatchQueue.MainQueue.DispatchAfter(
                    DispatchTime(DispatchTime.Now, TimeSpan.FromSeconds(0.5)),
                    fun _ ->
                        UIView.AnimateAsync(500., (fun _ -> blurredEffectView.RemoveFromSuperview()))
                        |> ignore
                )
