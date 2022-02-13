namespace FsLearn.Controllers


open System
open CoreFoundation
open CoreGraphics
open FSharp.Control
open FsLearn.Views
open ObjCRuntime
open UIKit

type MainViewController() as self =
    inherit UIViewController()

    let mutable getExchangesSub: IDisposable = null
    let mutable startSub: IDisposable = null

    let mutable stopSub: IDisposable = null

    let exchanges: ResizeArray<string> = ResizeArray<string>()

    let collectionView =
        lazy
            (let collectionView =
                new UICollectionView(self.View.Frame, new UICollectionViewFlowLayout())
             collectionView.TranslatesAutoresizingMaskIntoConstraints <- false
             collectionView)

    let startButton =
        lazy
            (let button = new UIButton(UIButtonType.System)
             button.SetTitle("Start", UIControlState.Normal)
             button.SetTitleColor(UIColor.Blue, UIControlState.Normal)
             button)

    let stopButton =
        lazy
            (let button = new UIButton(UIButtonType.System)
             button.SetTitle("Stop", UIControlState.Normal)
             button.SetTitleColor(UIColor.Blue, UIControlState.Normal)
             button)


    let container =
        lazy
            (let container = new UIStackView()
             container.Axis <- UILayoutConstraintAxis.Vertical
             container.Spacing <- 16
             container.TranslatesAutoresizingMaskIntoConstraints <- false
             container)


    let overViewDelegate =
        { new UICollectionViewDelegateFlowLayout() with
            override this.GetSizeForItem(collectionView, layout, indexPath) =
                CGSize(collectionView.Frame.Width - nfloat 16., nfloat 60.)

            member this.ItemSelected(collectionView, indexPath) = () }

    let dataSource =
        { new UICollectionViewDataSource() with

            override this.GetItemsCount(_, _) = nint exchanges.Count

            override this.GetCell(collectionView: UICollectionView, indexPath) =
                let cell =
                    collectionView.DequeueReusableCell(CustomCell.CellId, indexPath) :?> CustomCell

                let exchange = exchanges.[indexPath.Row]
                cell.SetUp(exchange)
                upcast cell }

    override _.ViewDidLoad() =
        base.ViewDidLoad()

        self.View.BackgroundColor <- UIColor.SystemBackgroundColor

        collectionView.Value.RegisterClassForCell(typeof<CustomCell>, CustomCell.CellId)
        container.Value.AddArrangedSubview collectionView.Value
        container.Value.AddArrangedSubview startButton.Value
        container.Value.AddArrangedSubview stopButton.Value

        self.View.AddSubview container.Value

        NSLayoutConstraint.ActivateConstraints(
            [| container.Value.TopAnchor.ConstraintEqualTo(self.View.SafeAreaLayoutGuide.TopAnchor, nfloat 0.)
               container.Value.LeadingAnchor.ConstraintEqualTo(self.View.SafeAreaLayoutGuide.LeadingAnchor, nfloat 0.)
               container.Value.TrailingAnchor.ConstraintEqualTo(self.View.SafeAreaLayoutGuide.TrailingAnchor, nfloat 0.)
               container.Value.BottomAnchor.ConstraintEqualTo(self.View.SafeAreaLayoutGuide.BottomAnchor, nfloat -16.) |]
        )

        collectionView.Value.DataSource <- dataSource
        collectionView.Value.Delegate <- overViewDelegate

        startSub <-
            startButton.Value.TouchUpInside.Subscribe
                (fun _ ->
                    getExchangesSub <-
                        ExchangeCore.getExchanges
                        |> Observable.subscribe
                            (fun x ->
                                exchanges.Add $"{x}"
                                DispatchQueue.MainQueue.DispatchAsync(fun _ -> collectionView.Value.ReloadData())))

        stopSub <- stopButton.Value.TouchUpInside.Subscribe(fun _ -> getExchangesSub.Dispose())


    interface IDisposable with
        member this.Dispose() =
            if startSub <> null then
                startSub.Dispose()

            if stopSub <> null then
                stopSub.Dispose()

            if getExchangesSub <> null then
                getExchangesSub.Dispose()
