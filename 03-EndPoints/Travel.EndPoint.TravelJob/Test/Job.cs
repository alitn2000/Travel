using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.EndPoint.TravelJob.Test;

public class Job
{
    public Type Type { get; set; }
    public string Experssion { get; set; }

    public Job(Type type, string experssion)
    {
        Type = type;
        Experssion = experssion;
    }
}
