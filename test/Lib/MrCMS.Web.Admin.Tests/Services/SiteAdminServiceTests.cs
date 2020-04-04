using MrCMS.TestSupport;

namespace MrCMS.Web.Apps.Admin.Tests.Services
{
    public class SiteAdminServiceTests : MrCMSTest
    {
        // todo - rewrite tests and refactor
        
        //private readonly ICloneSiteService _cloneSiteService;
        //private readonly SiteAdminService _siteService;
        //private readonly IMapper _mapper;

        //public SiteAdminServiceTests()
        //{
        //    _cloneSiteService = A.Fake<ICloneSiteService>();
        //    _mapper = A.Fake<IMapper>();
        //    _siteService = new SiteAdminService(Context, _cloneSiteService, _mapper);
        //}

        //[Fact]
        //public void SiteAdminService_GetAllSites_ReturnsPersistedSites()
        //{
        //    List<Site> sites = Enumerable.Range(1, 10).Select(i => new Site { Name = "Site " + i }).ToList();
        //    sites.ForEach(site => Context.Transact(session => session.Save(site)));

        //    List<Site> allSites = _siteService.GetAllSites();

        //    sites.ForEach(site => allSites.Should().Contain(site));
        //}

        //[Fact]
        //public void SiteAdminService_AddSite_ShouldPersistSiteToSession()
        //{
        //    var user = new User();
        //    Context.Transact(session => session.Save(user));
        //    //CurrentRequestData.CurrentUser = user;
        //    AddSiteModel model = new AddSiteModel();
        //    var site = new Site();
        //    A.CallTo(() => _mapper.Map<Site>(model)).Returns(site);
        //    var options = new List<SiteCopyOption>();

        //    _siteService.AddSite(model, options);

        //    // Including CurrentSite from the base class
        //    Context.QueryOver<Site>().RowCount().Should().Be(2);
        //}

        //[Fact]
        //public void SiteAdminService_AddSite_SavesPassedSiteToSession()
        //{
        //    AddSiteModel model = new AddSiteModel();
        //    var site = new Site();
        //    A.CallTo(() => _mapper.Map<Site>(model)).Returns(site);
        //    var options = new List<SiteCopyOption>();

        //    _siteService.AddSite(model, options);

        //    Context.QueryOver<Site>().List().Should().Contain(site);
        //}


        //[Fact]
        //public void SiteAdminService_SaveSite_UpdatesPassedSite()
        //{
        //    var site = new Site();
        //    Context.Transact(session => session.Save(site));
        //    site.Name = "updated";
        //    var updateSiteModel = new UpdateSiteModel{Id=site.Id};

        //    _siteService.SaveSite(updateSiteModel);

        //    A.CallTo(() => _mapper.Map(updateSiteModel, site)).MustHaveHappened();
        //    Context.Evict(site);
        //    Context.QueryOver<Site>().Where(s => s.Name == "updated").RowCount().Should().Be(1);
        //}

        //[Fact]
        //public void SiteAdminService_DeleteSite_ShouldDeleteSiteFromSession()
        //{
        //    var site = new Site();
        //    Context.Transact(session => session.Save(site));

        //    _siteService.DeleteSite(site.Id);

        //    Context.QueryOver<Site>().List().Should().NotContain(site);
        //}

        //[Fact]
        //public void SiteAdminService_DeleteSite_ShouldRemoveSiteFromSession()
        //{
        //    _siteService.DeleteSite(CurrentSite.Id);

        //    // Including CurrentSite from the base class
        //    Context.QueryOver<Site>().RowCount().Should().Be(0);
        //}

        //[Fact]
        //public void SiteAdminService_GetSite_ReturnsResultFromSessionGetAsResult()
        //{
        //    var site = new Site();


        //    _siteService.GetSite(site.Id).Should().Be(site);
        //}
    }
}