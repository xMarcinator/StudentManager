namespace StudentManager.Components;

public class filterData
{
    public string ValueName { get; }
    public string routeValueName { get; }
    public Dictionary<int, (string,bool)> options { get; }
    private filterData? dependentFilter { get; }
   
    public static filterData fromModels<T>(string routeValue,string valueName,int? selected, IEnumerable<T> models, Func<T, (int, string)> extractor,
        filterData? dependentFilter = null)
    {
        var options = models.Aggregate(new Dictionary<int, (string,bool)>(),
            (a, c) =>
            {
                var result = extractor.Invoke(c);

                a.Add(result.Item1, (result.Item2,Equals(result.Item1, selected)));

                return a;
            });
        return new filterData(routeValue,valueName,options,dependentFilter);
    }
    
    public  filterData(string routeValueName,string valueName,  Dictionary<int, (string,bool)> options, filterData? dependentFilter = null)
    {
        ValueName = valueName;
        this.routeValueName = routeValueName;
        this.options = options;
        this.dependentFilter = dependentFilter;
    }
}