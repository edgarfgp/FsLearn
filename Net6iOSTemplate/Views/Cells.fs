namespace Net6iOSTemplate.Views

open System
open ObjCRuntime
open UIKit
open Net6iOSTemplate.Extensions

type ExchangeCell(handle: IntPtr) as self =
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

        firstCurrencyValue.Lines <- nint 0

        firstCurrencyValue.Font <- UIFont.BoldSystemFontOfSize(nfloat 18.)

    static member val CellId = nameof ExchangeCell

    member this.SetupLabel() =
        let label = new UILabel()
        label.Lines <- nint 0
        label.TranslatesAutoresizingMaskIntoConstraints <- false
        label.Font <- UIFont.BoldSystemFontOfSize(nfloat 18.)
        label

    member self.SetUp(text) = firstCurrencyValue.Text <- text
