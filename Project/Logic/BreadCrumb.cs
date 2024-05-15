public class BreadCrumb
{
    // Class-level list to hold breadcrumb items
    private List<string> breadcrumbs = new List<string>();

    // Method to add a new breadcrumb item
    public string AddBreadCrumb(string breadcrumbName)
    {
        breadcrumbs.Add(breadcrumbName);
        return string.Join(" -> ", breadcrumbs);
    }

    // Method to generate the breadcrumb string
    public string GetBreadCrumbMenu()
    {   
        if (breadcrumbs.Count == 0)
        {
            return "TEST123\n";
        }
        else
        {
            return string.Join(" -> ", breadcrumbs);
        }
        
    }

    public void GoBack()
{
    if (breadcrumbs.Count > 0)
    {
        breadcrumbs.RemoveAt(breadcrumbs.Count - 1);
    }
    else
    {
        Console.WriteLine("No more pages to go back.");
    }
}


    // Method to clear the breadcrumb list (if needed)
    public void ClearBreadCrumbs()
    {
        breadcrumbs.Clear();
    }

}