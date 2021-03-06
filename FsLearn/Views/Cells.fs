namespace FsLearn.Views

open System
open ObjCRuntime
open System.Runtime.InteropServices

open UIKit
open FsLearn.Extensions

type CustomCell(handle: IntPtr) as self =
    inherit UICollectionViewCell(handle)
    let overallContainer = new UIStackView()
    let firstCurrencyValue = new UILabel()

    do
        overallContainer.TranslatesAutoresizingMaskIntoConstraints <- false
        self.ContentView.AddSubviews(overallContainer)
        overallContainer.ConstraintToParent self.ContentView

        overallContainer.AddArrangedSubview firstCurrencyValue
        overallContainer.Axis <- UILayoutConstraintAxis.Horizontal
        overallContainer.Alignment <- UIStackViewAlignment.Center
        overallContainer.Distribution <- UIStackViewDistribution.FillProportionally
        firstCurrencyValue.TranslatesAutoresizingMaskIntoConstraints <- false

        firstCurrencyValue.Lines <- 0

        firstCurrencyValue.Font <- UIFont.BoldSystemFontOfSize(NFloat 18.)

    static member val CellId = nameof CustomCell

    member this.SetupLabel() =
        let label = new UILabel()
        label.Lines <- 0
        label.TranslatesAutoresizingMaskIntoConstraints <- false
        label.Font <- UIFont.BoldSystemFontOfSize(NFloat 18.)
        label

    member self.SetUp(text) = firstCurrencyValue.Text <- text
