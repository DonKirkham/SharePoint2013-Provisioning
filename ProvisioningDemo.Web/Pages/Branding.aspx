<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Branding.aspx.cs" Inherits="TekFocus.ProvisioningDemoWeb.Pages.Branding" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Branding Demo Page</title>
        <script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>
        <script type="text/javascript" src="../Scripts/app.js"></script>
        <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
        <script type="text/javascript" src="../Scripts/customtabs.js"></script>

        <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    </head>
<body style="display:none; overflow: auto;">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True" AsyncPostBackTimeout="2000" />
            <div id="divSPChrome"></div>
            <asp:UpdateProgress ID="progress" runat="server" AssociatedUpdatePanelID="update" DynamicLayout="true">
                <ProgressTemplate>
                    <div id="divWaitingPanel" style="position: absolute; z-index: 3; background: rgb(255, 255, 255); width: 100%; bottom: 0px; top: 0px;">
                        <div style="top: 40%; position: absolute; left: 50%; margin-left: -150px;">
                            <img alt="Working on it" src="data:image/gif;base64,R0lGODlhEAAQAIAAAFLOQv///yH/C05FVFNDQVBFMi4wAwEAAAAh+QQFCgABACwJAAIAAgACAAACAoRRACH5BAUKAAEALAwABQACAAIAAAIChFEAIfkEBQoAAQAsDAAJAAIAAgAAAgKEUQAh+QQFCgABACwJAAwAAgACAAACAoRRACH5BAUKAAEALAUADAACAAIAAAIChFEAIfkEBQoAAQAsAgAJAAIAAgAAAgKEUQAh+QQFCgABACwCAAUAAgACAAACAoRRACH5BAkKAAEALAIAAgAMAAwAAAINjAFne8kPo5y02ouzLQAh+QQJCgABACwCAAIADAAMAAACF4wBphvID1uCyNEZM7Ov4v1p0hGOZlAAACH5BAkKAAEALAIAAgAMAAwAAAIUjAGmG8gPW4qS2rscRPp1rH3H1BUAIfkECQoAAQAsAgACAAkADAAAAhGMAaaX64peiLJa6rCVFHdQAAAh+QQJCgABACwCAAIABQAMAAACDYwBFqiX3mJjUM63QAEAIfkECQoAAQAsAgACAAUACQAAAgqMARaol95iY9AUACH5BAkKAAEALAIAAgAFAAUAAAIHjAEWqJeuCgAh+QQJCgABACwFAAIAAgACAAACAoRRADs=" style="width: 32px; height: 32px;" />
                            <span class="ms-accentText" style="font-size: 36px;">&nbsp;Working on it...</span>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="update" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="tab-content" style="padding: 0px 75px">
                            <h3>Deploy all the artifacts</h3>
                            <p>
                                Click the Deploy button to create folders, upload Master Pages, CSS, image and Display Template JavaScript files, create pages, create Site Columns, create a Content Type, create the Home Hero list and initialize the list with data.<br />
                            </p>
                            <asp:Button runat="server" ID="btnIniSiteContent" Text="Deploy" OnClick="btnDeployTemplates_Click" />
                            <br />
                            <br />
                            <h3>Delete all the artifacts (Optional)</h3>
                            <p>
                                To run the deployment process again, click the Delete Artifacts button below to remove all the artifacts created by the app. Then, click the Deploy button to create all the artifacts again.
                            </p>
                            <asp:Button runat="server" ID="btnDelete" Text="Delete Artifacts" OnClick="btnTemplatesDelete_Click" />
                            <br />
                            <br />
                            <asp:Label ID="lblInfo" runat="server"></asp:Label>
                    </div>

                <%--               <div class="panel-heading col-md-10 col-md-offset-1">
                <ul class="nav nav-tabs" id="myTab">
                <li class="active"><a href="#home">Home</a></li>
                <li><a href="#templates">Deploy/Apply Branding</a></li>
                <li><a href="#branding">Apply Branding</a></li>
                <li><a href="#themes">Custom Themes</a></li>
                <li><a href="#css">Custom CSS</a></li>
                <li><a href="#altcss">Alternate CSS</a></li>
                <li><a href="#client">Client Side</a></li>
                </ul>

                <div class="tab-content" style="padding-top: 15px;">
                <div class="tab-pane col-lg-8 col-md-8 active" id="home">
                Welcome to our Branding Demo Page. By clicking on the tabs above, you can see some of the different capabilities of the SharePoint Client App Model (CAM) using CSOM.
                </div>
                <div class="tab-pane" id="templates">
                <h2>Instructions</h2>
                <br />
                <h3>Deploy all the artifacts</h3>
                <p>
                Click the Deploy button to create folders, upload Master Pages, CSS, image and Display Template JavaScript files, create pages, create Site Columns, create a Content Type, create the Home Hero list and initialize the list with data.<br />
                </p>
                <asp:Button runat="server" ID="btnIniSiteContent" Text="Deploy" OnClick="btnDeployTemplates_Click" />
                <br />
                <br />
                <h3>Delete all the artifacts (Optional)</h3>
                <p>
                To run the deployment process again, click the Delete Artifacts button below to remove all the artifacts created by the app. Then, click the Deploy button to create all the artifacts again.
                </p>
                <asp:Button runat="server" ID="btnDelete" Text="Delete Artifacts" OnClick="btnTemplatesDelete_Click" />
                <br />
                <br />
                <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </div>
                <div class="tab-pane" id="themes">
                Custom Theme Demo here 
                </div>
                <div class="tab-pane" id="css">
                Custom CSS Demo here 
                </div>
                <div class="tab-pane" id="client">
                Cient Side Rendering Demo here 
                </div>
                <div class="tab-pane" id="branding">
                Apply Branding Demo here 
                </div>
                <div class="tab-pane" id="altcss">
                Alternate CSS Demo here 
                </div>
                </div>
                </div>--%>

                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </body>
</html>
