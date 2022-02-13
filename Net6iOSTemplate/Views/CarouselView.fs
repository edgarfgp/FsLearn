namespace Net6iOSTemplate.Views

open System
open CoreGraphics
open ObjCRuntime
open UIKit
open Net6iOSTemplate

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

        firstCurrencyValue.Lines <- nint 0

        firstCurrencyValue.Font <- UIFont.BoldSystemFontOfSize(nfloat 18.)

    static member val CellId = nameof CarouselCell

    member this.SetupLabel() =
        let label = new UILabel()
        label.Lines <- nint 0
        label.TranslatesAutoresizingMaskIntoConstraints <- false
        label.Font <- UIFont.BoldSystemFontOfSize(nfloat 18.)
        label

    member self.SetUp(text) = firstCurrencyValue.Text <- text


type CarouselView() as self =
    inherit UIView()

    let dataSource =
        lazy
            ({ new UICollectionViewDataSource() with

                override this.GetItemsCount(_, _) = nint 2

                override this.NumberOfSections _ = nint 1

                override this.GetCell(collectionView: UICollectionView, indexPath) =
                    let cell =
                        collectionView.DequeueReusableCell(CarouselCell.CellId, indexPath) :?> CarouselCell

                    cell.SetUp("aasdasdsadad")
                    upcast cell })

    let createColumnFlowLayout (frame: CGRect) =
        let cellPadding =
            (frame.Width - nfloat 300.) / nfloat 2.

        let flowLayout = new UICollectionViewFlowLayout()
        flowLayout.ScrollDirection <- UICollectionViewScrollDirection.Horizontal
        flowLayout.MinimumLineSpacing <- nfloat cellPadding * nfloat 2.
        flowLayout.SectionInset <- UIEdgeInsets(nfloat 0., cellPadding, nfloat 0., cellPadding)
        flowLayout.ItemSize <- CGSize(nfloat 300., nfloat 400.)
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
             pageControl.Pages <- nint 2
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
        collectionView.Value.HeightAnchor.ConstraintEqualTo(nfloat 450).Active <- true

        pageControl.Value.TopAnchor.ConstraintEqualTo(collectionView.Value.BottomAnchor, nfloat 16).Active <- true
        pageControl.Value.CenterXAnchor.ConstraintEqualTo(self.CenterXAnchor).Active <- true
        pageControl.Value.WidthAnchor.ConstraintEqualTo(nfloat 150).Active <- true
        pageControl.Value.HeightAnchor.ConstraintEqualTo(nfloat 50).Active <- true
