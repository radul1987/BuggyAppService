<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.Data" %>

<!-- Sample code copied from https://github.com/WadGraphEs/AzurePlot/blob/a2e028e18a113de231dcf064f916b66f25b97e14/AzurePlot/AzurePlot.Lib/ServicePointMonitor.cs -->


<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Detect Socket Leak (DSL)</title>
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css" rel="stylesheet" />
    <link href="//netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet" />

    <style>
        #grdCertificateDetails {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #grdCertificateDetails td, #customers th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #grdCertificateDetails tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #grdCertificateDetails tr:hover {
                background-color: #ddd;
            }

            #grdCertificateDetails th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: center;
                background-color: #6dbedd;
                color: white;
            }
        .auto-style1 {
            width: 190px;
        }
    </style>

    <script runat="server">


        string strConnection = string.Empty;
        string strConnectionServicePointCount = string.Empty;
        public static DataTable dtConnection;

        DataRow dRow;


        protected void Page_Load(object sender, EventArgs e)
        {
          

            //if (!IsPostBack)
            //{
            dtConnection = new DataTable();
            dtConnection.Columns.Add("Service_Point", typeof(string));
            dtConnection.Columns.Add("Connection_Limit", typeof(int));
            dtConnection.Columns.Add("Reported_Connections", typeof(int));
            dtConnection.Columns.Add("Connection_Group_Count", typeof(int));
            dtConnection.Columns.Add("Total_Connections", typeof(int));
            //}
              PrintConnections(HttpContext.Current.Response);
        }


        void PrintConnections(HttpResponse resp)
        {
            foreach (var serviceEndpoint in ListServicePoints(resp))
            {
                PrintServicePointConnections(serviceEndpoint, resp);
            }
        }
        IEnumerable<ServicePoint> ListServicePoints(HttpResponse response)
        {
            var tableField = typeof(ServicePointManager).GetField("s_ServicePointTable", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            var table = (Hashtable)tableField.GetValue(null);
            var keys = table.Keys.Cast<object>().ToList();
            strConnectionServicePointCount = string.Empty;

            strConnectionServicePointCount = strConnectionServicePointCount + string.Format("<BR/><BR/>ServicePoint count: {0}, DefaultConnectionLimit: {1}", keys.Count, ServicePointManager.DefaultConnectionLimit);
            ltrlConnectionDetails0.Text = strConnectionServicePointCount;

            foreach (var key in keys)
            {
                var val = ((WeakReference)table[key]);

                if (val == null)
                {
                    continue;
                }
                var target = val.Target;
                if (target == null)
                {
                    continue;
                }
                yield return target as ServicePoint;
            }


        }

        void PrintServicePointConnections(ServicePoint sp, HttpResponse resp)
        {
            var spType = sp.GetType();
            var privateOrPublicInstanceField = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            var connectionGroupField = spType.GetField("m_ConnectionGroupList", privateOrPublicInstanceField);
            var value = (Hashtable)connectionGroupField.GetValue(sp);
            var connectionGroups = value.Keys.Cast<object>().ToList();
            var totalConnections = 0;



            foreach (var key in connectionGroups)
            {
                var connectionGroup = value[key];
                var groupType = connectionGroup.GetType();
                var listField = groupType.GetField("m_ConnectionList", privateOrPublicInstanceField);
                var listValue = (ArrayList)listField.GetValue(connectionGroup);
                resp.Write(string.Format("{0}", key));
                totalConnections += listValue.Count;
            }

            dRow = dtConnection.NewRow();
            dRow["Service_Point"] = sp.Address;
            dRow["Connection_Limit"] = sp.ConnectionLimit;
            dRow["Reported_Connections"] = sp.CurrentConnections;
            dRow["Connection_Group_Count"] = connectionGroups.Count;
            dRow["Total_Connections"] = totalConnections;

            dtConnection.Rows.Add(dRow);
            gdView.DataSource = dtConnection;
            gdView.DataBind();

            //  strConnection = strConnection + string.Format("<BR/>ServicePoint: {0} (Connection Limit: {1}, Reported connections: {2})", sp.Address, sp.ConnectionLimit, sp.CurrentConnections);
            // strConnection = strConnection + string.Format("<BR/>ConnectionGroupCount: {0}, Total Connections: {1}<BR/><BR/><BR/>", connectionGroups.Count, totalConnections);
            //ltrlConnectionDetails.Text = strConnection;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar navbar-inverse navbar-fixed-top" style="border-bottom: 3px solid orange; background-color: #006699">
            <div class="container" style="padding: 0; margin-left: 20px; padding-right: 20px; width: 100%">
                <div class="navbar-header">
                    <a class="navbar-brand" style="color: white; float: left">Detect Socket Leak (DSL)</a>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav" style="float: right">
                        <li>
                            <a style="color: white" href="mailto:anilpras@microsoft.com">Contact</a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="container body-content" style="padding-top: 60px">
            <div class="container" style="width: 100%">
                <div class="row">
                    <div class="col-sm-12 tab-body tab-content">
                        <div class="tab-pane active" id="sites">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div style="width: 120px; background-color: #006699; color: white; padding-left: 10px">What CLI does:</div>
                                    <div style="width: 100%; border: 1px solid black; padding: 10px; margin-bottom: 30px; border-bottom-right-radius: 0px; border-bottom-left-radius: 0px; border-top-color: orange">
                                        <p>
                                            <b>Detect Socket Leak (DSL)</b> finds the connection(socket leak) if the web application has used SYSTEM.NET class to initiate the connections, ref. GitHub code <a href="https://github.com/WadGraphEs/AzurePlot/blob/a2e028e18a113de231dcf064f916b66f25b97e14/AzurePlot/AzurePlot.Lib/ServicePointMonitor.cs">code</a>, *DO NOT* use this page to troubleshoot database connection leak or issues, this is specific to the SYSTEM.NET. 
                                        </p>
                                    </div>


                                    <div style="width: 100px; background-color: #006699; color: white; padding-left: 10px">Prerequisites:</div>
                                    <div style="width: 100%; border: 1px solid black; padding: 10px; margin-bottom: 30px;">
                                        Step# 1: Place this page to your application root directory, and when you suspect 
                                        leak, browse it to check on connection leak.
                                            
                                            <br />
                                        Step# 2: Ensure you place this page in advance, *DO NOT* place the page into your site folder during the time of the issue.This may lead to application domain recycle.
                                            <br />
                                        <br />
                                        NOTE: Adding new file/content or modification in the site content folder may lead to the application domain recycle, and hence we do not suggest you do it during the production hours.
                                    </div>
                                    <div style="background-color: #006699; color: white; padding-left: 10px; font-size: small;" class="auto-style1">Connection Details:</div>

                                    <div style="width: 100%; border: 1px solid black; padding: 10px; margin-bottom: 30px; font-family: 'segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size: x-small;  color: #FFFFFF;">
                                        <asp:GridView ID="gdView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Height="138px" Width="1228px">
                                            <AlternatingRowStyle BackColor="White" Font-Size="Small" ForeColor="#284775" />
                                            <EditRowStyle BackColor="#999999" Font-Size="Small" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="False" Font-Size="Small" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" Font-Size="Small" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                        <br />
                                        <asp:Literal ID="ltrlConnectionDetails0" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="navbar navbar-inverse navbar-fixed-bottom" style="min-height: 30px; border-top: 3px solid orange; padding-top: 6px; color: white; background-color: #006699">
                <p>
                    &copy; Microsoft Azure does not own the third party code in any form, we have designed this page to help in troubleshooting, if this page does not help, please raise a ticket with Microsoft Azure respectively.
         .
                </p>
            </div>
        </div>
    </form>
</body>
</html>



