namespace Web.Repository.Admin.WebSiteSettings
{
    public class WebSiteSettingsRepository(WebDbContext context) :
        GenericRepository<WebSiteSettingsModel>(context), IWebSiteSettingsRepository
    {

    }
}