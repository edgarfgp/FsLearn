namespace FsLearn.Views

open System
open CoreGraphics
open System.Runtime.InteropServices
open UIKit
open FsLearn

type CarouselCell(handle: IntPtr) as self =
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

    static member val CellId = nameof CarouselCell

    member this.SetupLabel() =
        let label = new UILabel()
        label.Lines <- 0
        label.TranslatesAutoresizingMaskIntoConstraints <- false
        label.Font <- UIFont.BoldSystemFontOfSize(NFloat 18.)
        label

    member self.SetUp(text) = firstCurrencyValue.Text <- text


type CarouselView() as self =
    inherit UIView()

    let dataSource =
        lazy
            { new UICollectionViewDataSource() with

               override this.GetItemsCount(_, _) = 2

               override this.NumberOfSections _ = 1

               override this.GetCell(collectionView: UICollectionView, indexPath) =
                   let cell =
                       collectionView.DequeueReusableCell(CarouselCell.CellId, indexPath) :?> CarouselCell

                   cell.SetUp("aasdasdsadad")
                   upcast cell }

    let createColumnFlowLayout (frame: CGRect) =
        let cellPadding =
            (frame.Width - NFloat 300.) / NFloat 2.

        let flowLayout = new UICollectionViewFlowLayout()
        flowLayout.ScrollDirection <- UICollectionViewScrollDirection.Horizontal
        flowLayout.MinimumLineSpacing <- NFloat cellPadding * NFloat 2.
        flowLayout.SectionInset <- UIEdgeInsets(NFloat 0., cellPadding, NFloat 0., cellPadding)
        flowLayout.ItemSize <- CGSize(NFloat 300., NFloat 400.)
        flowLayout

    let collectionView =
        lazy
            (let collectionView =
                new UICollectionView(self.Frame, createColumnFlowLayout(self.Frame))

             collectionView.ShowsHorizontalScrollIndicator <- false
             collectionView.PagingEnabled <- true
             collectionView.BackgroundColor <- UIColor.Green
             collectionView.TranslatesAutoresizingMaskIntoConstraints <- false
             collectionView)

    let pageControl =
        lazy
            (let pageControl = new UIPageControl()
             pageControl.PageIndicatorTintColor <- UIColor.Gray
             pageControl.CurrentPageIndicatorTintColor <- UIColor.White
             pageControl.TranslatesAutoresizingMaskIntoConstraints <- false
             pageControl.Pages <- 2
             pageControl)

    do self.SetupUI()

    member this.SetupUI() =
        self.BackgroundColor <- UIColor.Red
        self.AddSubview collectionView.Value
        self.AddSubview pageControl.Value

        collectionView.Value.DataSource <- dataSource.Value
        collectionView.Value.RegisterClassForCell(typeof<CarouselCell>, CarouselCell.CellId)

        collectionView.Value.TopAnchor.ConstraintEqualTo(self.TopAnchor).Active <- true
        collectionView.Value.LeadingAnchor.ConstraintEqualTo(self.LeadingAnchor).Active <- true
        collectionView.Value.TrailingAnchor.ConstraintEqualTo(self.TrailingAnchor).Active <- true
        collectionView.Value.HeightAnchor.ConstraintEqualTo(NFloat 450).Active <- true

        pageControl.Value.TopAnchor.ConstraintEqualTo(collectionView.Value.BottomAnchor, NFloat 16).Active <- true
        pageControl.Value.CenterXAnchor.ConstraintEqualTo(self.CenterXAnchor).Active <- true
        pageControl.Value.WidthAnchor.ConstraintEqualTo(NFloat 150).Active <- true
        pageControl.Value.HeightAnchor.ConstraintEqualTo(NFloat 50).Active <- true
