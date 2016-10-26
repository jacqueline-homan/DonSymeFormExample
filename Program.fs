open System
open System.Drawing
open System.Windows.Forms

Application.EnableVisualStyles()
Application.SetCompatibleTextRenderingDefault(false)

let form = new Form(Text = "Curves")
let cpt = [|Point(20,60); Point(40,50); Point(130,60); Point(200,200)|]
let mutable movingPoint = -1

let newMenu (s:string) = new ToolStripMenuItem(s,Checked = true,CheckOnClick = true)
let menuBezier = newMenu "Show &Bezier"
let menuCanonical = newMenu "Show &Canonical spline"
let menuControlPoints = newMenu "Show control &points"

let scrollbar = new VScrollBar(Dock = DockStyle.Right, LargeChange = 2, Maximum = 10)

let drawPoint (g:Graphics) (p:Point) =
    g.DrawEllipse(Pens.Red, p.X - 2, p.Y - 2, 4, 4)

let paint (g:Graphics) = 
    if (menuBezier.Checked) then
        g.DrawLine(Pens.Red, cpt.[0], cpt.[1])
        g.DrawLine(Pens.Red, cpt.[2], cpt.[3])
        g.DrawBeziers(Pens.Black, cpt)
    if (menuCanonical.Checked) then
        g.DrawCurve(Pens.Blue, cpt, float32 scrollbar.Value)
    if (menuControlPoints.Checked) then
        for i = 0 to cpt.Length - 1 do
            drawPoint g cpt.[i] 

let isClose (p:Point) (n:Point) =
    let dx = p.X - 1
    let dy = p.Y - 1
    (dx * dx + dy * dy) < 6



[<STAThread>]
Application.Run(form)

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code

