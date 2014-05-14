<%@ Page Language="C#" AutoEventWireup="true" CodeFile="report.aspx.cs" Inherits="report" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge;chrome=1" />
    <title>Translation Request - Avaya</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="developer" content="William Ballesteros Blanco (wballesteros@avaya.com) - Avaya Inc. 2014">
    <link rel="icon" type="image/png" href="images/favicon.png" />
    <!-- Le styles -->
    <link href="css/bootstrap.css" rel="stylesheet">
    
    <link href="css/bootstrap-responsive.css" rel="stylesheet">
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet">
    <link href="css/docs.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    
    
    <link href="css/DT_bootstrap.css" rel="stylesheet" type="text/css"/>
    <link href="css/bootstrap-dialog.css" rel="stylesheet" type="text/css" />
   

    <style>
        .btn-file {
            position: relative;
            overflow: hidden;
        }

        .btn-file input[type=file] {
            position: absolute;
            top: 0;
            right: 0;
            min-width: 150%;
            min-height: 100%;
            font-size: 999px;
            text-align: right;
            filter: alpha(opacity=0);
            opacity: 0;
            background: red;
            cursor: inherit;
            display: block;
        }
    </style>

    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/bootstrap-dialog.js"></script>
    <script type="text/javascript" src="js/mystyle.js"></script>
    <script type="text/javascript" src="js/holder/holder.js"></script>
    <script type="text/javascript" src="js/respond.src.js"></script>
    <script type="text/javascript" src="js/prettyLoader.js"></script>
    <script type="text/javascript" src="js/ajaxfileupload.js"></script>
    <script type="text/javascript" src="js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="js/DT_bootstrap.js"></script>
    <script type="text/javascript" src="js/bootstrap-datetimepicker.js"></script>
    <script type="text/javascript" src="js/report.js"></script>

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="assets/ico/apple-touch-icon-57-precomposed.png">
    <link rel="shortcut icon" href="assets/ico/favicon.png">
    <style>
        /*@media all and (min-width:1200px)*/
        .container, .navbar-static-top .container, .navbar-fixed-top .container, .navbar-fixed-bottom .container {
            width: 1400px!important;
        }
    </style>
</head>

<body>



    <!-- NAVBAR
    ================================================== -->
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container">
                <a class="brand" href="http://avaya.com">
                    <img class="desktop" src="images/avaya-logo.jpg" alt="Avaya" /><img class="mobile" src="images/avaya-logo-mobile.jpg" alt="Avaya" /></a>

                <button type="button" class="btn btn-navbar visible-phone" data-toggle="collapse" data-target=".nav-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>

                <div class="nav-collapse collapse">
                    <ul class="nav">
                        <li><a href="#" id="new_solicit">Reports</a></li>
                        <li style="width:300px;"><a href="sol_details.aspx" id="userName"></a></li>
                        <li><a href="#" id="exit">Log Out</a></li>
                    </ul>
                    
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
    </div>


    <!-- Marketing messaging and featurettes
    ================================================== -->
    <!-- Wrap the rest of the page in another container to center all the content. -->

    

    <div class="container" id="dt_my_solicits">
        <div class="row-fluid">


            <div class="span9">

                <hr style="margin-top: 0;">
                
                

                <div class="row-fluid">
                    <div class="span12">
                        <h2>Reports</h2>
                        
        

        <table id="datatables" cellpadding="0" cellspacing="0" border="0" style="width: 100%; text-align: center; visibility: hidden" class="table table-striped table-bordered">
            <thead id="thead">
                <tr>
                    
                    
                    <th class="sorting" width="3%">ID</th>
                    <th class="sorting" width="15%">Translation Name</th>
                    <th class="sorting" width="6%">Requestor</th>
                    <th class="sorting" width="6%">Original Lang</th>
                    <th class="sorting" width="6%">Translate Lang</th>
                    <th class="sorting" width="9%">Translation State</th>
                    <th class="sorting" width="10%">Date of Requirement</th>
                    <th class="sorting" width="10%">Desired Date</th>
                    <th class="sorting" width="10%">Estimated Date</th>
                    <th class="sorting" width="6%">In Charge</th>
                    <th class="sorting" width="6%">Duration / Days</th>
                    

                    
                </tr>
            </thead>
            <tbody id="tbody">
            </tbody>
            <tfoot>
                <tr>
                    <td>
                       <div id="toExcel"><a href="#" id="btnDescargaExcel">
                           <img src="images/xls.png" alt="to Excel" /></a>
                       </div>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    
                    
                </tr>
            </tfoot>
        </table>
                         </div></div></div></div>
    </div>
    <!-- /.container -->

    <!-- Progress bar -->
    <div class="modal hide" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-header">
                <h1>Processing...</h1>
            </div>
            <div class="modal-body">
                <div class="progress progress-striped active">
                    <div class="bar progress-bar-danger" style="width: 100%;"></div>
                </div>
            </div>
    </div>

    <!-- Footer
    ================================================== -->

    <footer class="footer"><a href="http://www4.avaya.com/cala/SM_LATAM/Index.html" target="_blank"><img src="images/Translation-solcials_02.png"></a>
      

        <div class="bs-docs-social social-media">
            <div class="container">

                <div class="row  hidden-desktop">
                    <div class="span12">

                    </div>
                </div>


                <div class="row">

                    <div class="span12">
                        <ul class="footer-links">
                            <li><a href="https://thesource.avaya.com/avayaPortal/friendly/termsPage">Terms of Use</a></li>
                            <li class="muted">&middot;</li>
                            <li><a href="http://www.avaya.com/gcm/master-usa/en-us/includedcontent/privacy.htm">Privacy Statement</a></li>
                            <li class="muted">&middot;</li>
                            <li><a href="http://www.avaya.com/gcm/master-usa/en-us/includedcontent/cookiepolicy.htm">Cookies</a></li>
                            <li class="muted">&middot;</li>
                            <li class="muted">&copy; 2009-2014 Avaya Inc.</li>
                        </ul>
                    </div>
                </div>

            </div>
        </div>
</footer>




    <!-- Le javascript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    
    <!--[if lte IE 7]><script src="assets/js/lte-ie7.js"></script><![endif]-->
</body>
</html>
