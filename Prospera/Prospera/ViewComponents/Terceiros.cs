using Microsoft.AspNetCore.Mvc;

public class TerceirosViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        // Lógica para carregar a exibição da página "Terceiros"
        return View();
    }
}
