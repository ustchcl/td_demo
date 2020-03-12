using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

public class StopableInterval
{
    private UniRx.IObservable<long> ob;

    private StopableInterval() { }

    private bool stopped = false;


    public static StopableInterval Interval(TimeSpan t, int count)
    {
        var si = new StopableInterval();
        si.ob = Observable.Interval(t).Take(count);
        return si;
    }

    public IDisposable Subscribe(Action<long> onNext)
    {
        return ob.Where(_ => !stopped).Subscribe<long>(_ => {
            if (!stopped)
            {
                onNext(_);
            }
        });
    }



    public void Stop()
    {
        stopped = true;
    }
    
}