// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open System.Windows.Forms
open System.Drawing




let imageform  =

    let x = new Form()
    x.Width <- 1600
    x.Height <-1000
    let p = new PictureBox()
    p.Width <- x.Width
    p.Height <-x.Height
    p.Location <- new Point (0,0)
    x.Controls.Add p

    x
    

[<EntryPoint>]
[<STAThread>]
let main argv = 

    let dialog = new System.Windows.Forms.FolderBrowserDialog();
    let result =dialog.ShowDialog();
    let mutable path= ""
    if result = DialogResult.OK then
        path <-  dialog.SelectedPath
        ()

    else
        failwith "word to your mother"
        ()
    let imagePaths = System.IO.Directory.GetFiles(path)
    let images =
        let array = 
            [| for i in imagePaths do
                 if ( i.Contains("Thumbs.db")) then
                   
                    () //do nothing
                 else  
                    yield Image.FromFile i
            |] 
        new System.Collections.Generic.List<Image>(array)
   // let bitmap = new Bitmap( Image.FromFile(""))
   // imageform.Controls.Add bitmap
    let imagechange (c:Control) =
           let p = c.Controls.[0] :?> PictureBox
           p.Image <- images.Item(0)
           images.Remove(images.Item(0)) |> ignore
           images.Add p.Image


    let timer = new Timer()
    timer.Interval <- 3000
    timer.Tick.Add (fun _ -> imagechange imageform) 
    timer.Start()
    timer.Enabled <- true;
   
    printfn "%A" argv
    Application.Run imageform
    Console.ReadLine() |> ignore
    0 // return an integer exit code
