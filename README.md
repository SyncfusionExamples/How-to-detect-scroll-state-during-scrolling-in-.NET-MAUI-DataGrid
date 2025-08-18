# How to detect scroll states during scrolling in .NET MAUI DataGrid?

This article demonstrates how to detect scroll states during scrolling in [.NET MAUI DataGrid](https://www.syncfusion.com/maui-controls/maui-datagrid).

The scroll state detection can be implemented by accessing the ScrollOwner from DataGrid’s visual container using dataGrid.GetVisualContainer(). A velocity threshold helps distinguish between drag and fling, while an idle state can be detected using a timer that resets on each scroll and triggers when no further scrolls occur.

## C#

```C#
public MainPage()
{

    InitializeComponent();

   var visualContainer =  dataGrid.GetVisualContainer();

    if (visualContainer != null && visualContainer.ScrollOwner != null)
    {
        visualContainer.ScrollOwner.Scrolled += ScrollOwner_Scrolled;
    }


    idleTimer = new System.Timers.Timer(idleTimeout);
    idleTimer.Elapsed += (s, e) =>
    {
        idleTimer.Stop();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Debug.WriteLine("Idle detected");
        });
    };

}

private void ScrollOwner_Scrolled(object? sender, ScrolledEventArgs e)
{
    var currentOffset = e.ScrollY;
    var currentTime = DateTime.Now;

    var deltaOffset = currentOffset - lastScrollOffset;
    var deltaTime = (currentTime - lastScrollTime).TotalMilliseconds;

    if (deltaTime > 0)
    {
        var velocity = deltaOffset / deltaTime;

        if (Math.Abs(velocity) > flingThreshold)
        {
            Debug.WriteLine("Fling detected");
        }
        else
        {
            Debug.WriteLine("Drag detected");
        }
    }

    idleTimer.Stop();
    idleTimer.Start();

    lastScrollOffset = currentOffset;
    lastScrollTime = currentTime;
}
```

You can download this example on [GitHub](https://github.com/SyncfusionExamples/How-to-detect-scroll-state-during-scrolling-in-.NET-MAUI-DataGrid).