﻿namespace FsLearn

open UIKit

module Main = 
    [<EntryPoint>]
    let main args = 
        UIApplication.Main(args, null, typeof<AppDelegate>)
        0
