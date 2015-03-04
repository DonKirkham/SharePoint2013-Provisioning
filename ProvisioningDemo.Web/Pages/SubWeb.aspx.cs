using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Client;
using TekFocus.ProvisioningDemoWeb;

namespace TekFocus.ProvisioningDemoWeb.Pages
{
    public partial class SubWeb : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Uri redirectUrl;
            switch (SharePointContextProvider.CheckRedirectionStatus(Context, out redirectUrl))
            {
                case RedirectionStatus.Ok:
                    return;
                case RedirectionStatus.ShouldRedirect:
                    Response.Redirect(redirectUrl.AbsoluteUri, endResponse: true);
                    break;
                case RedirectionStatus.CanNotRedirect:
                    Response.Write("This page must be run as part of a SharePoint App.");
                    Response.End();
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // define initial script, needed to render the chrome control
            string script = @"
            function chromeLoaded() {
                $('#chromeControl_topheader_apptitle').text('Provisioning Demo');
                $('body').show();
            }

            //function callback to render chrome after SP.UI.Controls.js loads
            function renderSPChrome() {
                //Set the chrome options for launching Help, Account, and Contact pages
                var options = {
                    'appTitle': document.title,
                    'onCssLoaded': 'chromeLoaded()'
                };

                //Load the Chrome Control in the divSPChrome element of the page
                var chromeNavigation = new SP.UI.Controls.Navigation('divSPChrome', options);
                chromeNavigation.setVisible(true);
            }";

            //register script in page
            Page.ClientScript.RegisterClientScriptBlock(typeof(SubWeb), "BasePageScript", script, true);

            string currUrl = HttpContext.Current.Request.Url.OriginalString;
            currUrl = currUrl.Substring(0, currUrl.IndexOf("?"));

            SharePointContext spContext = SharePointContextProvider.Current.GetSharePointContext(Context);
            using (ClientContext ctx = spContext.CreateUserClientContextForSPHost())
            {
                try
                {
                    Web rootWeb = ctx.Site.RootWeb;
                    ctx.Load(rootWeb);
                    if (!rootWeb.AllProperties.IsPropertyAvailable("SubsiteOverrideUrl") || rootWeb.AllProperties["SubsiteOverrideUrl"] != currUrl)
                    {
                        rootWeb.AllProperties["SubsiteOverrideUrl"] = currUrl;
                        rootWeb.Update();
                        ctx.Load(rootWeb);
                        ctx.ExecuteQuery();
                        //new SubWeb().AddJsLink(ctx, rootWeb, this.Request);
                    }
                }
                catch (Exception ex)
                {
                }

                //string[] approved = { "STS#0", "ENTERWIKI#0" };
                //Web web = ctx.Web;
                //WebTemplateCollection templates = web.GetAvailableWebTemplates(1033, false);
                //ctx.Load(web);
                //ctx.Load(templates);
                //ctx.ExecuteQuery();
                //foreach (var template in templates)
                //{
                //    if (Array.IndexOf(approved, template.Name) > -1){
                //        listSites.Items.Add(new System.Web.UI.WebControls.ListItem(template.Title, template.Name));
                //    }
                //}
                //listSites.SelectedIndex = 0;
            }

            lblBasePath.Text = Request["SPHostUrl"] + "/";
            Dictionary<string, string> templates = new Dictionary<string, string>();
            templates.Add("Team Site", "STS#0");
            templates.Add("Custom Team Site", "STS#0");
            //templates.Add("Discovery Center", "EDISC#0");
            //templates.Add("Discovery Case", "EDISC#1");
            //templates.Add("Records Center", "OFFILE#1");
            //templates.Add("Shared Services Administration Site", "OSRV#0");
            //templates.Add("PerformancePoint", "PPSMASite#0");
            //templates.Add("Business Intelligence Center", "BICenterSite#0");
            //templates.Add("SharePoint Portal Server Site", "SPS#0");
            //templates.Add("Contents area Template", "SPSTOC#0");
            //templates.Add("Topic area template", "SPSTOPIC#0");
            templates.Add("News Site", "SPSNEWS#0");
            //templates.Add("CMS Publishing Site", "CMSPUBLISHING#0");
            //templates.Add("Publishing Site ", "BLANKINTERNET#0");
            //templates.Add("Press Releases Site", "BLANKINTERNET#1");
            //templates.Add("Publishing Site with Workflow", "BLANKINTERNET#2");
            //templates.Add("News Site2", "SPSNHOME#0");
            //templates.Add("Site Directory", "SPSSITES#0");
            //templates.Add("Community area template", "SPSCOMMU#0");
            //templates.Add("Report Center", "SPSREPORTCENTER#0");
            //templates.Add("Collaboration Portal", "SPSPORTAL#0");
            //templates.Add("Enterprise Search Center", "SRCHCEN#0");
            //templates.Add("Profiles", "PROFILES#0");
            //templates.Add("Publishing Portal", "BLANKINTERNETCONT");
            //templates.Add("My Site Host", "SPSMSITEHOST#0");
            //templates.Add("Enterprise Wiki", "ENTERWIKI#0");
            //templates.Add("Project Site", "PROJECTSITE#0");
            //templates.Add("Product Catalog ", "PRODUCTCATALOG#0");
            templates.Add("Community Site", "COMMUNITY#0");
            //templates.Add("Community Portal", "COMMUNITYPORTAL#0");
            //templates.Add("Basic Search Center", "SRCHCENTERLITE#0");
            //templates.Add("Basic Search Center2", "SRCHCENTERLITE#1");
            //templates.Add("Visio Process Repository", "visprus#0");
            foreach (var template in templates)
            {
                listSites.Items.Add(new System.Web.UI.WebControls.ListItem(template.Key, template.Value));
            }
            listSites.SelectedIndex = 0;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(Context);

            using (var ctx = spContext.CreateUserClientContextForSPHost())
            {
                Web newWeb = CreateSubWeb(ctx, ctx.Web, txtUrl.Text, listSites.SelectedValue, txtTitle.Text, txtDescription.Text);
                // Redirect to just created site
                Response.Redirect(newWeb.Url);
            }
        }

        public Web CreateSubWeb(Microsoft.SharePoint.Client.ClientContext ctx, Web hostWeb, string txtUrl,
            string template, string title, string description)
        {
            // Create web creation configuration
            WebCreationInformation information = new WebCreationInformation();
            information.WebTemplate = template;
            information.Description = description;
            information.Title = title;
            information.Url = txtUrl;
            // Currently all English, could be extended to be configurable based on language pack usage
            information.Language = 1033;

            Microsoft.SharePoint.Client.Web newWeb = null;
            newWeb = hostWeb.Webs.Add(information);
            ctx.ExecuteQuery();

            ctx.Load(newWeb);
            ctx.ExecuteQuery();

            // Add sub site link override
            //new SubWeb().AddJsLink(ctx, newWeb, this.Request);

            // Set oob theme to the just created site
            //new BrandingHelper().SetThemeBasedOnName(ctx, newWeb, hostWeb, "Orange");

            // All done, let's return the newly created site
            return newWeb;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.Request["SPHostUrl"]);
        }
    }
}