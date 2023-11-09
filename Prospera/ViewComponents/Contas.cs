using Microsoft.AspNetCore.Mvc;

public class ContasViewComponent :ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
